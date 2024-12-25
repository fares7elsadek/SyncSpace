using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Moq;
using SyncSpace.Application.Services;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using Xunit;

namespace SyncSpace.Application.ApplicationUser.Commands.RegisterUser.Tests;

public class RegisterUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAuthResponse_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
        );

        var mapperMock = new Mock<IMapper>();
        var authServiceMock = new Mock<IAuthService>();
        var environmentMock = new Mock<IWebHostEnvironment>();

        var user = new User
        {
            Id = "1",
            UserName = "testuser",
            Email = "test@example.com",
            Avatar = "path/to/avatar"
        };

        var command = new RegisterUserCommand
        {
            Email = "test@example.com",
            UserName = "testuser",
            Password = "Password123!"
        };

        userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        mapperMock.Setup(m => m.Map<User>(It.IsAny<RegisterUserCommand>())).Returns(user);

        authServiceMock.Setup(auth => auth.CreateJwtToken(It.IsAny<User>()))
            .ReturnsAsync(new System.IdentityModel.Tokens.Jwt.JwtSecurityToken());

        environmentMock.Setup(env => env.ContentRootPath).Returns("/app");

        var handler = new RegisterUserCommandHandler(userManagerMock.Object, mapperMock.Object, authServiceMock.Object, environmentMock.Object);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Message.Should().Be("User registered successfully");
        response.Email.Should().Be("test@example.com");
        response.Username.Should().Be("testuser");
        response.IsAuthenticated.Should().BeTrue();
        response.Roles.Should().Contain(UserRoles.User);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailAlreadyExists()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
        );

        var mapperMock = new Mock<IMapper>();
        var authServiceMock = new Mock<IAuthService>();
        var environmentMock = new Mock<IWebHostEnvironment>();

        userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

        var command = new RegisterUserCommand
        {
            Email = "test@example.com",
            UserName = "testuser",
            Password = "Password123!"
        };

        var handler = new RegisterUserCommandHandler(userManagerMock.Object, mapperMock.Object, authServiceMock.Object, environmentMock.Object);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Message.Should().Be("User email already exsit");
    }

    
}
