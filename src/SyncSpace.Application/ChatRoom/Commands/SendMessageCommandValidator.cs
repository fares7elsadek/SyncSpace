using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SyncSpace.Application.ChatRoom.Commands;

public class SendMessageCommandValidator:AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(d => d.Content)
            .Length(1, 5000)
            .WithMessage("The message must be between 1 and 5000 characters.");
    }
}
