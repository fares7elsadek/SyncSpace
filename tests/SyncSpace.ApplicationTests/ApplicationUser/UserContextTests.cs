using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;
using FluentAssertions;

namespace SyncSpace.Application.ApplicationUser.Tests;

public class UserContextTests
{
    [Fact]
    public void GetCurrentUser_ShouldReturnCurrentUser_WhenUserIsAuthenticated()
    {
        // Arrange
        var userId = "123";
        var email = "test@example.com";
        var roles = new List<string> { "Admin", "User" };

        var claims = new List<Claim>
        {
            new Claim("uid", userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "User")
        };

        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);

        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(context => context.User).Returns(principal);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);

        var userContext = new UserContext(mockHttpContextAccessor.Object);

        // Act
        var result = userContext.GetCurrentUser();

        // Assert
        result.Should().NotBeNull();
        result.userId.Should().Be(userId);
        result.Email.Should().Be(email);
        result.IsInRole("Admin").Should().BeTrue();
        result.IsInRole("User").Should().BeTrue();
    }

    [Fact]
    public void GetCurrentUser_ShouldThrowInvalidOperationException_WhenHttpContextIsNull()
    {
        // Arrange
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(accessor => accessor.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(mockHttpContextAccessor.Object);

        // Act
        var act = () => userContext.GetCurrentUser();

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
    
}
