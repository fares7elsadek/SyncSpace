﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SyncSpace.Application.Services;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SyncSpace.Application.ApplicationUser.Commands.CreateRefreshToken;

public class CreateRefreshTokenCommandHandler(UserManager<User> userManager,
    IAuthService authService) : IRequestHandler<CreateRefreshTokenCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var authModel = new AuthResponse();
        var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == request.Token));
        if (user == null)
            throw new CustomeException("Invalid token");
        var token = user.RefreshTokens.Single(t => t.Token == request.Token);
        if (!token.IsActive)
            throw new CustomeException("Inactive token");
        token.RevokedOn = DateTime.UtcNow;
        var newRefreshToken = authService.GenerateRefreshToken();
        user.RefreshTokens.Add(newRefreshToken);
        await userManager.UpdateAsync(user);

        var jwtToken = await authService.CreateJwtToken(user);
        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authModel.Email = user.Email;
        authModel.Username = user.UserName;
        var roles = await userManager.GetRolesAsync(user);
        authModel.Roles = roles.ToList();
        authModel.Message = "success";
        authModel.RefreshToken = newRefreshToken.Token;
        authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

        return authModel;
    }
}
