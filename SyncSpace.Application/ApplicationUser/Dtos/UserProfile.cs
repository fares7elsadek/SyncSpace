using AutoMapper;
using SyncSpace.Application.ApplicationUser.Commands.RegisterUser;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.ApplicationUser.Dtos;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User,UserDto>()
            .ReverseMap();
        CreateMap<RegisterUserCommand, User>()
            .ReverseMap();
    }
}
