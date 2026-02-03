using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnagramSolver.WebApp.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly Mock<IAnagramSolver> _mockAnagramSolver;
        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockAnagramSolver = new Mock<IAnagramSolver>();
            _homeController = new HomeController(_mockLogger.Object, _mockAnagramSolver.Object);
        }

        [Theory]
        [InlineData("test", "sett")]
        [InlineData("testing", null)]
        public void Index_VariousWords_ReturnsPossibleAnagrams(string id, string expectedAnagram)
        {
            //arrange
            var expectedResult = expectedAnagram == null 
                ? new List<string>() 
                : new List<string> { expectedAnagram };

            _mockAnagramSolver.Setup(s => s.GetAnagrams(id)).Returns(expectedResult);

            //act
            var result = _homeController.Index(id);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<AnagramViewModel>(viewResult.Model);

            model.AnagramLines.Should().BeEquivalentTo(expectedResult);
            
        }

        [Fact]
        public void Index_EmptyId_DoesNotCallService()
        {
            //arrange

            //act
            var result = _homeController.Index("");

            //assert
            _mockAnagramSolver.Verify(s => s.GetAnagrams(It.IsAny<string>()), Times.Never);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<AnagramViewModel>(viewResult.Model);

            model.AnagramLines.Should().BeNullOrEmpty();
        }
    }
}