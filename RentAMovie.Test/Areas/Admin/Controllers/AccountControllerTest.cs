namespace RentAMovie.Test.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Areas.Admin.Controllers;
    using RentAMovie.Areas.Admin.Models.Account;
    using RentAMovie.Data.Models;
    using RentAMovie.Services.Account;
    using RentAMovie.Test.Mocks;

    public class AccountControllerTest
    {
        [Fact]
        public void IndexShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new AccountService(data, MapperMock.Instance);
            var accountController = new AccountController(service);

            var result = accountController.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            var service = new AccountService(data, MapperMock.Instance);
            var accountController = new AccountController(service);

            var result = accountController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectViewModel()
        {
            using var data = DatabaseMock.Instance;
            var service = new AccountService(data, MapperMock.Instance);
            var accountController = new AccountController(service);

            var result = accountController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<ViewUserModel>>(viewResult.Model);
        }

        [Fact]
        public void DeleteShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = "1"
            });
            data.SaveChanges();
            var service = new AccountService(data, MapperMock.Instance);
            var accountController = new AccountController(service);

            var result = accountController.Delete("1");

            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = "1"
            });
            data.SaveChanges();
            var service = new AccountService(data, MapperMock.Instance);
            var accountController = new AccountController(service);

            var result = accountController.Delete("1");

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
