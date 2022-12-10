namespace RentAMovie.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using RentAMovie.Controllers;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Services.Movie;
    using RentAMovie.Test.Mocks;

    using static TestConstants;

    public class MovieControllerTest
    {
        [Fact]
        public void CreateShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Create();

            Assert.NotNull(result);
        }

        [Fact]
        public void CreateShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreatePostShouldReturnCorrectViewWhenModelStateIsNotValid()
        {
            using var data = DatabaseMock.Instance;
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            movieController.ModelState.AddModelError("Content", "Content is mandatory");

            var result = movieController.Create(new FormMovieModule());

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<FormMovieModule>(viewResult.Model);
        }

        [Fact]
        public void CreatePostShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Create(new FormMovieModule 
            {
                Title = "test2",
                Description = "test2",
                Tagline = "test",
                Runtime = 20,
                Revenue = 20,
                Budget = 20,
                DatePublished = DateTime.UtcNow,
                Poster = "test",
                Backdrop = "test",
                Trailer = "test",
                Genres = "Test2, ",
            });

            Assert.NotNull(result);
        }

        [Fact]
        public void CreatePostShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Create(new FormMovieModule
            {
                Title = "test2",
                Description = "test2",
                Genres = "Test2, ",
            });

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void EditShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Edit(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void EditShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Edit(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<FormMovieModule>(viewResult.Model);
        }

        [Fact]
        public void EditShouldReturnErrorView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            movieController.TempData = mockTempData.Object;

            var result = movieController.Edit(2);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void EditPostShouldReturnValidModelWhenModelStateIsNotValid()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            movieController.ModelState.AddModelError("Content", "Content is mandatory");

            var result = movieController.Edit(1, new FormMovieModule
            {
                Title = "test2",
                Description = "test2",
                Genres = "Test2, ",
            });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<FormMovieModule>(viewResult.Model);
        }

        [Fact]
        public void EditPostShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Edit(1, new FormMovieModule
            {
                Title = "test2",
                Description = "test2",
                Genres = "Test2, ",
            });

            Assert.NotNull(result);
        }

        [Fact]
        public void EditPostShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Edit(1, new FormMovieModule
            {
                Title = "test2",
                Description = "test2",
                Genres = "Test2, ",
            });

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "UserMovies");
            Assert.True(viewResult.ControllerName == "Movie");
        }

        [Fact]
        public void EditPostShouldReturnErrorView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            movieController.TempData = mockTempData.Object;

            var result = movieController.Edit(2, new FormMovieModule
            {
                Title = "test2",
                Description = "test2",
                Genres = "Test2, ",
            });

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void EditPostShouldReturnCorrectViewWhenNoGenres()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Edit(3, new FormMovieModule
            {
                Title = "test2",
                Description = "test2",
                Genres = "Test2, ",
            });

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "UserMovies");
            Assert.True(viewResult.ControllerName == "Movie");
        }

        [Fact]
        public void DeleteShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Delete(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Delete(1);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void DeleteShouldNotDeleteMovie()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            movieController.TempData = mockTempData.Object;

            var result = movieController.Delete(2);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void UserMoviesShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.UserMovies();

            Assert.NotNull(result);
        }

        [Fact]
        public void UserMoviesShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.UserMovies();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void UserMoviesShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.UserMovies();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<ViewUserMovieCardModel>>(viewResult.Model);
        }

        [Fact]
        public void MovieUserShouldReturnErrorView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            movieController.TempData = mockTempData.Object;

            var result = movieController.MovieUser(0);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void MovieUserShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieUser(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void MovieUserShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieUser(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void MovieUserShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieUser(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<UserSingleMovieModel>(viewResult.Model);
        }

        [Fact]
        public void MovieTmdbShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieTmdb(validMovieTmdbId);

            Assert.NotNull(result);
        }

        [Fact]
        public void MovieTmdbShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieTmdb(validMovieTmdbId);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void MovieTmdbShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieTmdb(validMovieTmdbId);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TmdbSingleMovieModel>(viewResult.Model);
        }

        [Fact]
        public void MovieTmdbShouldReturnCorrectViewModelWhenMovieIsThere()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.MovieTmdb(120);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TmdbSingleMovieModel>(viewResult.Model);
        }

        [Fact]
        public void MovieTmdbShouldReturnErrorView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            movieController.TempData = mockTempData.Object;

            var result = movieController.MovieTmdb(invalidId);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Error");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void PopularShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Popular();

            Assert.NotNull(result);
        }

        [Fact]
        public void PopularShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Popular();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PopularShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.Popular();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<PopularMovieResultModule>>(viewResult.Model);
        }

        [Fact]
        public void TopRatedShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.TopRated();

            Assert.NotNull(result);
        }

        [Fact]
        public void TopRatedShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.TopRated();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TopRatedShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new MovieService(data);
            var movieController = new MovieController(service);

            var result = movieController.TopRated();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<PopularMovieResultModule>>(viewResult.Model);
        }

        private ViewMoviesDbContext PrepareDb()
        {
            var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = "1"
            });
            data.Users.Add(new User
            {
                Id = "2"
            });
            data.Genres.Add(new Genre
            {
                Id = 1,
                Name = "Test"
            });
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test",
                UserId = "1",
                GenresCollection = new List<Genre>
                {
                    new Genre
                    {
                        Id = 2,
                        Name = "Test2"
                    }
                },
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Id = 1,
                        Content = "test",
                        UserId = "1",
                        MovieId = 1
                    }
                }
            });
            data.Movies.Add(new Movie
            {
                Id = 2,
                Title = "test2",
                Description = "test2",
                UserId = "2"
            });
            data.Movies.Add(new Movie
            {
                Id = 3,
                Title = "test3",
                Description = "test3",
                UserId = "1"
            });
            data.Movies.Add(new Movie
            {
                Id = 4,
                TmdbId = 120,
                Title = "test4",
                Description = "test4",
                UserId = "1"
            });
            data.SaveChanges();

            return data;
        }
    }
}
