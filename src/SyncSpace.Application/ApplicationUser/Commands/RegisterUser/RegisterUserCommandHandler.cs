using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;
using SyncSpace.Application.Services;
using SyncSpace.Application.Services.EmailService;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SyncSpace.Application.ApplicationUser.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<User> userManager,
    IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, LinkGenerator linkGenerator,
    IEmailSender<User> emailSender) : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userManager.FindByEmailAsync(request.Email) is not null)
            throw new CustomeException("User email already exsit");


        if (await userManager.FindByNameAsync(request.UserName) is not null)
            throw new CustomeException("Username already exsit");

        var user = mapper.Map<User>(request);
        var webroot = environment.WebRootPath;
        var path = Path.Combine(webroot, "favatars");
        Random rand = new Random();
        var number = rand.Next(1, 13);
        user.Avatar = Path.Combine(path, $"{number}.jpg");
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

        if (httpContextAccessor.HttpContext == null)
            throw new CustomeException("Something wrong has happened");

        await userManager.AddToRoleAsync(user, UserRoles.User);

        try
        {
            await SendConfirmationEmailAsync(user, httpContextAccessor.HttpContext);
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to send confirmation email for user {user.Email}: {ex.Message}");
            throw new CustomeException($"Failed to send confirmation email for user {user.Email}: {ex.Message}");
        }



        var authResponse = new AuthResponse
        {
            Email = request.Email,
            Username = request.UserName,
            IsAuthenticated = true,
            Message = "User registered successfully. Please check your email to confirm your account."
        };

        return authResponse;
    }

    private async Task SendConfirmationEmailAsync(User user, HttpContext httpContext)
    {
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        user.LastEmailConfirmationToken = code;
        await userManager.UpdateAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var userId = await userManager.GetUserIdAsync(user);
        var routeValues = new RouteValueDictionary()
        {
            ["userId"] = userId,
            ["code"] = code
        };
        //var confirmEmailEndpointName = "ConfirmEmail";
        var confirmEmailUrl = $"https://sync-space-frontend-sigma.vercel.app/confirm-email?userId={user.Id}&code={code}";

        if (user.Email == null)
            throw new CustomeException("User email is not defined");

        await emailSender.SendConfirmationLinkAsync(user, user.Email, confirmEmailUrl);
    }
}
