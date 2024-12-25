using Xunit;
using FluentAssertions;
using FluentValidation.TestHelper;
namespace SyncSpace.Application.ApplicationUser.Commands.RegisterUser.Tests;

public class RegisterUserCommandValidatorTests
{
    [Fact()]
    public void RegisterUserCommandValidatorTest_ForInvalidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange
        var command = new RegisterUserCommand()
        {
            UserName = "Test",
            Email = "fares@gmail.com",
            Password = "Hllo566%$##@"
        };
        var validator = new RegisterUserCommandValidator();
        // act
        
        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void RegisterUserCommandValidatorTest_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // arrange
        var command = new RegisterUserCommand()
        {
            UserName = "Test",
            Email = "gmail.com",
            Password = "Hllo5"
        };
        var validator = new RegisterUserCommandValidator();
        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldHaveValidationErrorFor(c => c.Password);
    }
}