using AutoMapper;
using FluentValidation;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.ChatRoom.Dtos;

public class MessageProfile:Profile
{
    public MessageProfile()
    {
        CreateMap<Messages,MessageDto>().ForMember(d => d.User,options =>
        {
            options.MapFrom(d => d.User);
        })
            .ReverseMap();
    }
}
