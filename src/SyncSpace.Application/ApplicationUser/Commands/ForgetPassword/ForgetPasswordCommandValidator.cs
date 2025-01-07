using FluentValidation;

namespace SyncSpace.Application.ApplicationUser.Commands.ForgetPassword;

public class ForgetPasswordCommandValidator:AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordCommandValidator()
    {
        RuleFor(d => d.Email)
            .EmailAddress().WithMessage("Email address is not valid.");
    }
}
