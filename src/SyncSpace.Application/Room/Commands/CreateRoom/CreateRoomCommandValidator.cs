using FluentValidation;

namespace SyncSpace.Application.Room.Commands.CreateRoom;

public class CreateRoomCommandValidator:AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.RoomName)
            .Length(3, 50).WithMessage("Room name must be between 3 and 50 characters.");
    }
}
