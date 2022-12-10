namespace RentAMovie.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using RentAMovie.Controllers;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;
    using RentAMovie.Services.Person;
    using RentAMovie.Test.Mocks;

    using static TestConstants;

    public class PersonControllerTest
    {
        [Fact]
        public void PersonTmdbShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(validActorId);

            Assert.NotNull(result);
        }

        [Fact]
        public void PersonTmdbShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(validActorId);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PersonTmdbShouldReturnCorrectViewModel()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(validActorId);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewPersonModel>(viewResult.Model);
        }

        [Fact]
        public void PersonTmdbShouldReturnViewForActor()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(validActorId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var viewPersonModel = Assert.IsType<ViewPersonModel>(model);
            Assert.True(viewPersonModel.PersonData.Name == "Tom Holland");
            Assert.True(viewPersonModel.Movies.Count() == 0);
        }

        [Fact]
        public void PersonTmdbShouldReturnViewForDirector()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(validDirectorId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var viewPersonModel = Assert.IsType<ViewPersonModel>(model);
            Assert.True(viewPersonModel.PersonData.Name == "Christopher Nolan");
            Assert.True(viewPersonModel.Movies.Count() == 0);
        }

        [Fact]
        public void PersonTmdbShouldReturnErrorView()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(invalidId);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Error");
        }

        [Fact]
        public void ListShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            personController.TempData = mockTempData.Object;

            var result = personController.List(validActorId);

            Assert.NotNull(result);
        }

        [Fact]
        public void ListShouldReturnError()
        {
            using var data = DatabaseMock.Instance;
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            personController.TempData = mockTempData.Object;

            var result = personController.List(invalidId);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void ListShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.PersonTmdb(validMovieTmdbId);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ListShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.List(validMovieTmdbId);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewAllCastModel>(viewResult.Model);
        }

        [Fact]
        public void ListShouldReturnViewWithMovies()
        {
            using var data = PrepareDb();
            var service = new PersonService(data, MapperMock.Instance);
            var personController = new PersonController(service);

            var result = personController.List(validMovieTmdbId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var viewAllCastModel = Assert.IsType<ViewAllCastModel>(model);
            Assert.True(viewAllCastModel.Movie.Title == "test");
            Assert.True(viewAllCastModel.Actors.FirstOrDefault().Name == "Christian Bale");
        }

        private ViewMoviesDbContext PrepareDb()
        {
            var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                TmdbId = validMovieTmdbId,
                Title = "test",
                Description = "test"
            });
            data.SaveChanges();

            return data;
        }
    }
}
