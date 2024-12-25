using AutoMapper;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.RoomParticipant.Dtos;

public class RoomParticipantProfile:Profile
{
    public RoomParticipantProfile()
    {
        CreateMap<RoomParticipants,RoomParticipantDto>()
            .ReverseMap();
    }
}
