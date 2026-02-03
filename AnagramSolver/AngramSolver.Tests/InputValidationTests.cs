using AnagramSolver.BusinessLogic.Services;
using FluentAssertions;

namespace AnagramSolver.BusinessLogic.Tests;

public class InputValidationTests
{
    private readonly InputValidation _inputValidation;

    public InputValidationTests()
    {
        _inputValidation = new InputValidation();
    }


    [Theory]
    [InlineData (3, "test", true)]
    [InlineData(4, "test", true)]
    [InlineData(5, "test", false)]
    [InlineData(2, "test  testing   ", true)]
    [InlineData(5, "test   testing ", false)]
    [InlineData(0, "", false)]
    [InlineData(0, "  ", false)]
    [InlineData(0, null, false)]
    [InlineData(2, "test \t testing", true)]
    [InlineData(2, "test \n testing", true)]
    public void IsValidInput_VariousInputs_ReturnsExpectedOutput(int minWordLength, string input, bool expectedOutput)
    {
        //arrange

        //act
        var result = _inputValidation.IsValidUserInput(input, minWordLength);

        //assert
        result.Should().Be(expectedOutput);

    }
}
