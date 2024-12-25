using MediatR;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Application.Services;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;

namespace SyncSpace.Application.ApplicationUser.Commands.LoginUser;

public class LoginUserCommandHandler(UserManager<User> userManager,
    IAuthService authService) : IRequestHandler<LoginUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var password = request.Password;
        var user = await userManager.FindByEmailAsync(email);
        if (user == null || !await userManager.CheckPasswordAsync(user, password))
            throw new CustomeException("Email or password is incorrect");
        var roles = await userManager.GetRolesAsync(user);
        var authResponse = await authService.GetJwtToken(user, roles.ToList());
        return authResponse;
    }
}
