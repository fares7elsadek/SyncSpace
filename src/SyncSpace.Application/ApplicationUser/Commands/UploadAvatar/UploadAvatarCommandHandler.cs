using MediatR;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Application.Services;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;

namespace SyncSpace.Application.ApplicationUser.Commands.UploadAvatar;

public class UploadAvatarCommandHandler(IUserContext userContext,
    IFileService fileService,UserManager<User> userManager) : IRequestHandler<UploadAvatarCommand>
{
    public async Task Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var Id = userContext.GetCurrentUser().userId;
        if (string.IsNullOrEmpty(Id))
            throw new CustomeException("User not authorized");

        var user = await userManager.FindByIdAsync(Id);
        if (user == null)
            throw new CustomeException("User not authorized");

        string[] allowedImageExtensions = new string[]
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"
        };
        var filename = await fileService.SaveFileAsync(request.Avatar, allowedImageExtensions);
        if (string.IsNullOrEmpty(filename))
            throw new CustomeException("Something wrong has happened");
        user.Avatar = filename;
        await userManager.UpdateAsync(user);
    }
}
