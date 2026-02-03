using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using FluentAssertions;

namespace AnagramSolver.BusinessLogic.Tests;

public class WordProcessorTests
{
    private readonly IWordProcessor _wordProcessor;

    public WordProcessorTests()
    {
        _wordProcessor = new WordProcessor();
    }

    [Theory]
    [InlineData ("test")]
    [InlineData ("TEST")]
    [InlineData ("TeSt")]
    [InlineData (" T eS t  ")]
    public void CreateCharCount_VariousInputs_ReturnsCharCountDictionary(string stringToProcess)
    {
        //arrange
        var expectedResult = new Dictionary<char, int>
        {
            ['t'] = 2,
            ['e'] = 1,
            ['s'] = 1
        };

        //act
        var result = _wordProcessor.CreateCharCount(stringToProcess);

        //assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData ("test", "estt")]
    [InlineData (" te  s t", "estt")]
    [InlineData ("", "")]
    [InlineData (null, "")]
    public void SortString_VariousInputs_ReturnsSortedString(string stringToProcess, string expectedResult)
    {
        //act
        var result = _wordProcessor.SortString(stringToProcess);

        //assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData (" te st  ", "test")]
    [InlineData ("", "")]
    [InlineData ("te\tst", "test")]
    [InlineData ("te\nst", "test")]
    [InlineData (null, "")]
    public void RemoveWhitespace_VariousInputs_OutputsStringWithoutWhitespaces(string stringToProcess, string expectedResult)
    {
        //act
        var result = _wordProcessor.RemoveWhitespace(stringToProcess);

        //assert
        result.Should().Be(expectedResult);
    }
}
