using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SyncSpace.Application.Services;
using SyncSpace.Application.Services.EmailService;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace SyncSpace.Application.ApplicationUser.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<User> userManager,
    IMapper mapper, IAuthService authService, IWebHostEnvironment environment,IUrlHelper urlHelper,
    EmailService emailService) : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userManager.FindByEmailAsync(request.Email) is not null)
            return new AuthResponse { Message = "User email already exsit" };

        if (await userManager.FindByNameAsync(request.UserName) is not null)
            return new AuthResponse { Message = "Username already exsit" };

        var user = mapper.Map<User>(request);
        var webroot = environment.WebRootPath;
        var path = Path.Combine(webroot, "favatars");
        Random rand = new Random();
        var number = rand.Next(1, 13);
        user.Avatar = Path.Combine(path, $"{number}.jpg");
        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            var CreatedUser = await userManager.FindByEmailAsync(request.Email);
            if (CreatedUser == null)
                throw new CustomeException("Something wrong has happened");

            var token = await userManager.GenerateEmailConfirmationTokenAsync(CreatedUser);
            var confirmationUrl = request.UrlHelper.Action("ConfirmEmail", "Auth", new { userId = CreatedUser.Id, token },
                request.UrlHelper.ActionContext.HttpContext.Request.Scheme);
            if (confirmationUrl == null)
                throw new CustomeException("Something wrong has happened");

            var template = EmailTemplate.ConfirmEmail(confirmationUrl);
            await emailService.SendEmailAsync(request.Email, "Confirm your email", template);
        }
        else
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            throw new CustomeException(string.Join(',', errors));
        }
        
        return new AuthResponse
        {
            Id = user.Id,
            Message = "User registered successfully. Please check your email to confirm your account.",
            Email = request.Email,
        };
    }
}
