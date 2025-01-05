using MediatR;
using Microsoft.AspNetCore.Mvc;
using SyncSpace.Domain.Helpers;
using System.Text.Json.Serialization;

namespace SyncSpace.Application.ApplicationUser.Commands.RegisterUser;

public class RegisterUserCommand:IRequest<AuthResponse>
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    [JsonIgnore]
    public IUrlHelper? UrlHelper { get; set; } = default!;
}
