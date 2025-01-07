using MediatR;

namespace SyncSpace.Application.ApplicationUser.Commands.ForgetPassword;

public class ForgetPasswordCommand:IRequest<string>
{
    public string Email { get; set; } = default!;
}
