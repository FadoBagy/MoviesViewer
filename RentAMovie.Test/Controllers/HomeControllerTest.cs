namespace RentAMovie.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Controllers;
    using RentAMovie.Models.Home;
    using RentAMovie.Models.MovieModuls;
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
            var model = viewResult.Model;

            var viewIndexModel = Assert.IsType<ViewIndexModel>(model);
            Assert.Equal(15, viewIndexModel.TotalReviews);
            Assert.Equal(10, viewIndexModel.TotalMovies);
            Assert.Equal(1, viewIndexModel.TotalUsers);

            Assert.NotNull(viewIndexModel.TopActionMovies);
            Assert.NotEmpty(viewIndexModel.TopActionMovies);
        }
    }
}
