using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Application.Services;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace SyncSpace.Application.ApplicationUser.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<User> userManager,
    IMapper mapper, IAuthService authService, IWebHostEnvironment environment) : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userManager.FindByEmailAsync(request.Email) is not null)
            return new AuthResponse { Message = "User email already exsit" };

        if (await userManager.FindByNameAsync(request.UserName) is not null)
            return new AuthResponse { Message = "Username already exsit" };

        var user = mapper.Map<User>(request);
        var ContentPath = environment.ContentRootPath;
        var path = Path.Combine(ContentPath, "Uploads");
        user.Avatar = Path.Combine(path, "Default.jpg");
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            throw new CustomeException(string.Join(',', errors));
        }
        await userManager.AddToRoleAsync(user, UserRoles.User);
        var JwtSecurityToken = await authService.CreateJwtToken(user);
        if (JwtSecurityToken == null)
            throw new CustomeException("Somthing wrong has happened");
        return new AuthResponse
        {
            Id = user.Id,
            Message = "User registered successfully",
            Email = user.Email,
            IsAuthenticated = true,
            Roles = new List<string> { UserRoles.User },
            Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
            Username = user.UserName
        };
    }
}
