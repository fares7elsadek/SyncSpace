﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Application.Services;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;

namespace SyncSpace.Application.ApplicationUser.Commands.LoginUser;

public class LoginUserCommandHandler(UserManager<User> userManager,
    IAuthService authService,SignInManager<User> signInManager) : IRequestHandler<LoginUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var password = request.Password;
        var user = await userManager.FindByEmailAsync(email);

        if (user == null || user.UserName == null)
            throw new CustomeException("Invalid credentials");

        var result = await signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: true);
        if (result.IsLockedOut)
        {
            throw new CustomeException("Account is locked out due to multiple failed attempts. Please try again later.");
        }
        if (!result.Succeeded)
        {
            throw new CustomeException("Invalid credentials");
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
            throw new CustomeException("Please confirm your email first");

        var roles = await userManager.GetRolesAsync(user);
        var authResponse = await authService.GetJwtToken(user, roles.ToList());
        authResponse.avatar = user.Avatar;
        return authResponse;
    }
}
