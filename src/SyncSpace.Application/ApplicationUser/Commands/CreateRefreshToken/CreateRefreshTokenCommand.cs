using MediatR;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Helpers;

namespace SyncSpace.Application.ApplicationUser.Commands.CreateRefreshToken;

public class CreateRefreshTokenCommand(string token):IRequest<AuthResponse>
{
    public string Token { get; set; } = token;
}
