namespace RentAMovie.Test.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Areas.Admin.Controllers;
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Services.Movie;
    using RentAMovie.Test.Mocks;

    public class MovieControllerTest
    {
        [Fact]
        public void IndexShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectViewModel()
        {
            using var data = DatabaseMock.Instance;
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<MovieViewModel>(viewResult.Model);
        }
    }
}
