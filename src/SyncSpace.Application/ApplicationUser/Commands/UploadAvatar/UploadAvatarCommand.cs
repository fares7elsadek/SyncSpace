using MediatR;
using Microsoft.AspNetCore.Http;

namespace SyncSpace.Application.ApplicationUser.Commands.UploadAvatar;

public class UploadAvatarCommand:IRequest
{
    public IFormFile Avatar { get; set; } = default!;
}
