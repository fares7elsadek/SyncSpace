using Xunit;
using AutoMapper;
using SyncSpace.Domain.Entities;
using FluentAssertions;
using SyncSpace.Application.ApplicationUser.Commands.RegisterUser;

namespace SyncSpace.Application.ApplicationUser.Dtos.Tests;

public class UserProfileTests
{
    private IMapper _mapper;
    public UserProfileTests()
    {
        var configurations = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserProfile>();
        });
        _mapper = configurations.CreateMapper();
    }

    [Fact()]
    public void UserProfileTest_ForUserToUserDto_MapCorrectly()
    {
        // arrange
        var user = new User
        {
            Id = "1234",
            UserName = "Test",
            Email = "fares@gmail.com",
            Avatar = "fares.jpg",
            NormalizedUserName = "Test",
            NormalizedEmail = "Test",
        };

        // act 
        var userDto = _mapper.Map<UserDto>(user);

        // assert
        userDto.UserName.Should().Be(user.UserName);
        userDto.Id.Should().Be(user.Id);
        userDto.Email.Should().Be(user.Email);
        userDto.Avatar.Should().Be(user.Avatar);
    }

    [Fact()]
    public void UserProfileTest_ForRegisterUserCommandToUser_MapCorrectly()
    {
        // arrange
        var registerUser = new RegisterUserCommand
        {
            UserName = "Test",
            Email = "fares@gmail.com",
            Password = "Test",
        };

        // act 
        var user = _mapper.Map<User>(registerUser);

        // assert
        user.UserName.Should().Be(user.UserName);
        user.Email.Should().Be(user.Email);
    }
}