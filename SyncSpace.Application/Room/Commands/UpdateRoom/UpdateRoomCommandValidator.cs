using FluentValidation;

namespace SyncSpace.Application.Room.Commands.UpdateRoom;

public class UpdateRoomCommandValidator:AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(d => d.RoomName)
            .Length(5, 50)
            .When(d => !string.IsNullOrWhiteSpace(d.RoomName))
            .WithMessage("The title must be between 5 and 50 characters.");


        RuleFor(d => d)
            .Must(d => d.IsActive != null || !string.IsNullOrWhiteSpace(d.RoomName))
            .WithMessage("Either the RoomName or the IsActive must be provided.");
    }
}
