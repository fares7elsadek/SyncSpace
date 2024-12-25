using AutoMapper;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.Stream.Dtos;

public class StreamDetailsProfile:Profile
{
    public StreamDetailsProfile()
    {
        CreateMap<Rooms, StreamDetails>()
            .ReverseMap();
    }
}
