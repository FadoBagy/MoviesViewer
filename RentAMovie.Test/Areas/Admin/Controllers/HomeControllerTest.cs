namespace RentAMovie.Test.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Areas.Admin.Controllers;
    using RentAMovie.Areas.Admin.Models.Home;
    using RentAMovie.Services.Home;
    using RentAMovie.Test.Mocks;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new HomeService(data);
            var homeController = new HomeController(service);

            var result = homeController.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            var service = new HomeService(data);
            var homeController = new HomeController(service);

            var result = homeController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectViewModel()
        {
            using var data = DatabaseMock.Instance;
            var service = new HomeService(data);
            var homeController = new HomeController(service);

            var result = homeController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewHomeModel>(viewResult.Model);
        }
    }
}
