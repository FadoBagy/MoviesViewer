namespace RentAMovie.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using RentAMovie.Controllers;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Review;
    using RentAMovie.Services.Review;
    using RentAMovie.Test.Mocks;

    public class ReviewControllerTest
    {
        [Fact]
        public void CreateShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Create(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void CreateShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Create(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Create(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<FormReviewModel>(viewResult.Model);
        }

        [Fact]
        public void CreateShouldReturnRedirectWhenMovieIsNull()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            reviewController.TempData = mockTempData.Object;

            var result = reviewController.Create(0);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void CreatePostShouldReturnRedirectWhenMovieIsNull()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            reviewController.TempData = mockTempData.Object;

            var result = reviewController.Create(new FormReviewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void CreatePostShouldReturnValidModelWhenModelStateIsNotValid()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);
            reviewController.ModelState.AddModelError("Content", "Content is mandatory");

            var result = reviewController.Create(new FormReviewModel
            {
                MovieId = 1
            });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<FormReviewModel>(viewResult.Model);
        }

        [Fact]
        public void CreatePostShouldReturnRedirectWhenMovieEverythingIsCorrent()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Create(new FormReviewModel
            {
                Content = "test",
                MovieId = 1
            });

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "All");
            Assert.True(viewResult.ControllerName == "Review");
        }

        [Fact]
        public void SingleReviewShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.SingleReview(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void SingleReviewShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.SingleReview(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SingleReviewShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.SingleReview(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewReviewModel>(viewResult.Model);
        }

        [Fact]
        public void SingleReviewShouldReturnRedirectWhenReviewIsNull()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            reviewController.TempData = mockTempData.Object;

            var result = reviewController.SingleReview(0);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void AllShouldNotReturnNullView()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.All(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void AllShouldReturnRedirectWhenReviewIsNull()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            reviewController.TempData = mockTempData.Object;

            var result = reviewController.All(0);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void AllShouldReturnCorrectView()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.All(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.All(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<ViewReviewModel>>(viewResult.Model);
        }

        [Fact]
        public void AllShouldFullUpViewBagsAndReturnViewWhenMovieHasNoReviews()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.All(2);

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllUserReviewShouldNotReturnNullViewForAllMovies()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.AllUserReview(0);

            Assert.NotNull(result);
        }

        [Fact]
        public void AllUserReviewShouldNotReturnNullViewForSingleMovie()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.AllUserReview(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void AllUserReviewShouldReturnCorrectViewForAllMovies()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.AllUserReview(0);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllUserReviewShouldReturnCorrectViewForSingleMovie()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.AllUserReview(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllUserReviewShouldReturnCorrectViewModel()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.AllUserReview(0);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<QueryReviewModel>(viewResult.Model);
        }

        [Fact]
        public void AllUserReviewShouldReturnCorrectReviewsForMovies()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.AllUserReview(0);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<QueryReviewModel>(viewResult.Model);
            Assert.Equal(2, model.Reviews.Count());
        }

        [Fact]
        public void DeleteShouldReturnRedirectWhenReviewIdIsInvalid()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);
            var mockTempData = new Mock<ITempDataDictionary>();
            reviewController.TempData = mockTempData.Object;

            var result = reviewController.Delete(1);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public void DeleteShouldReturnRedirectWhenReviewIsDeleted()
        {
            using var data = PrepareDb();
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Delete(3);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "All");
            Assert.True(viewResult.ControllerName == "Review");
        }

        private static ViewMoviesDbContext PrepareDb()
        {
            var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = "1",
                UserName = "test"
            });
            data.Users.Add(new User
            {
                Id = "2",
                UserName = "test2"
            });
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test",
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Id = 2,
                        Content = "test",
                        MovieId = 1,
                        UserId = "1"
                    }
                },
                UserId = "1"
            });
            data.Movies.Add(new Movie
            {
                Id = 2,
                Title = "test2",
                Description = "test2",
                UserId = "1"
            });
            data.Reviews.Add(new Review
            {
                Id = 1,
                Content = "test",
                MovieId = 1,
                UserId = "1"
            });
            data.Reviews.Add(new Review
            {
                Id = 3,
                Content = "test3",
                MovieId = 1,
                UserId = "2"
            });
            data.SaveChanges();

            return data;
        }
    }
}
