

using AutoMapper;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.Room.Dtos;

public class RoomProfile:Profile
{
    public RoomProfile()
    {
        CreateMap<Rooms, RoomDto>()
            .ForMember(d => d.HostUser, opt =>
        {
            opt.MapFrom(src => src.HostUser);
        }).ForMember(d => d.RoomParticipants, opt =>
        {
            opt.MapFrom(src => src.Participants);
        })
        .ForMember(d => d.Messages, opt =>
        {
            opt.MapFrom(src => src.Messages);
        })
            .ReverseMap();
    }
}
