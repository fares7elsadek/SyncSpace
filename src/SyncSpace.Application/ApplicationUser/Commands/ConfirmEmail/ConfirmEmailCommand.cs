using MediatR;
using SyncSpace.Domain.Helpers;

namespace SyncSpace.Application.ApplicationUser.Commands.ConfirmEmail;

public class ConfirmEmailCommand(string userId,string token):IRequest<AuthResponse>
{
    public string Token { get; set; } = token;
    public string UserId { get; set; } = userId;
}
