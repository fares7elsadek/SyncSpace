using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace SyncSpace.Application.ApplicationUser.Tests;

public class CurrentUserTests
{
    [Fact]
    public void IsInRole_ShouldReturnTrue_WhenRoleExists()
    {
        // Arrange
        var userId = "123";
        var email = "test@example.com";
        var roles = new List<string> { "Admin", "User" };
        var currentUser = new CurrentUser(userId, email, roles);

        // Act
        var result = currentUser.IsInRole("Admin");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_ShouldReturnFalse_WhenRoleDoesNotExist()
    {
        // Arrange
        var userId = "123";
        var email = "test@example.com";
        var roles = new List<string> { "Admin", "User" };
        var currentUser = new CurrentUser(userId, email, roles);

        // Act
        var result = currentUser.IsInRole("Guest");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_ShouldReturnFalse_WhenRolesListIsEmpty()
    {
        // Arrange
        var userId = "123";
        var email = "test@example.com";
        var roles = new List<string>();
        var currentUser = new CurrentUser(userId, email, roles);

        // Act
        var result = currentUser.IsInRole("Admin");

        // Assert
        result.Should().BeFalse();
    }
}
