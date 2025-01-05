using MediatR;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;

namespace SyncSpace.Application.ApplicationUser.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(UserManager<User> userManager) : IRequestHandler<ConfirmEmailCommand,AuthResponse>
{
    public async Task<AuthResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.UserId))
        {
            throw new CustomeException("Invalid email confirmation request");
        }
        var user = await userManager.FindByIdAsync(request.UserId);
        if(user == null)
            throw new NotFoundException(nameof(User),request.UserId);

        var result = await userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            throw new CustomeException("Invalid email confirmation request");

        await userManager.AddToRoleAsync(user, UserRoles.User);

        return new AuthResponse
        {
            Id = user.Id,
            Message = "Email confirmed successfully.",
            IsAuthenticated = true,
            Username = user.UserName,
            Email = user.Email,
            Roles = new List<string>() { UserRoles.User },
        };
    }
}
