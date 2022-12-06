namespace RentAMovie.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Controllers;
    using RentAMovie.Models.Home;
    using RentAMovie.Test.Mocks;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView() 
        {
            // Arrange
            var homeController = new HomeController(null);

            // Act 
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SourceShouldReturnView()
        {
            var homeController = new HomeController(null);

            var result = homeController.Source();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithModel() 
        {
            var homeController = new HomeController(HomeServiceMock.Instance);

            var result = homeController.Index();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ViewIndexModel>(viewResult.ViewData.Model);
            Assert.Equal(15, model.TotalReviews);
            Assert.Equal(10, model.TotalMovies);
            Assert.Equal(1, model.TotalUsers);
            Assert.NotNull(model.TopActionMovies);
            Assert.NotEmpty(model.TopActionMovies);
        }
    }
}
