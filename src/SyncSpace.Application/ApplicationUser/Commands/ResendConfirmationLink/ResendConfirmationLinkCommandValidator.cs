using FluentValidation;

namespace SyncSpace.Application.ApplicationUser.Commands.ResendConfirmationLink;

public class ResendConfirmationLinkCommandValidator:AbstractValidator<ResendConfirmationLinkCommand>
{
    public ResendConfirmationLinkCommandValidator()
    {
        RuleFor(d => d.Email)
            .EmailAddress().WithMessage("Email address is not valid.");
    }
}
