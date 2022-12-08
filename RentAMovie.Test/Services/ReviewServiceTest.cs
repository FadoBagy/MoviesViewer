namespace RentAMovie.Test.Services
{
    using RentAMovie.Areas.Admin.Models.Review;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;
    using RentAMovie.Services.Review;
    using RentAMovie.Test.Mocks;

    public class ReviewServiceTest
    {
        [Fact]
        public void GetMovieShouldReturnSingleMovie()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test"
            });
            data.SaveChanges();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetMovie(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test");
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetMovieWithReviewsShouldReturnSingleMovieWithReviews()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test",
                Tagline = "test",
                Runtime = 1,
                Revenue = 1,
                Budget = 1,
                ContentRanting = "t",
                Trailer = "t",
                Reviews = new List<Review> 
                { 
                    new Review 
                    { 
                        Content = "test",
                        UserId = "1"
                    } 
                }
            });
            data.SaveChanges();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetMovieWithReviews(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test");
            Assert.Single(result.Reviews);
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetReviewShouldReturnSingleReview()
        {
            using var data = DatabaseMock.Instance;
            data.Reviews.Add(new Review
            {
                Id = 1,
                Content = "test",
                UserId = "1"
            });
            data.SaveChanges();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetReview(1);

            Assert.NotNull(result);
            Assert.True(result.Content == "test");
            Assert.IsType<Review>(result);
        }

        [Fact]
        public void GetReviewsShouldReturnReviewsForMovie()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test",
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Content = "test",
                        UserId = "1"
                    }
                }
            });
            data.SaveChanges();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetReviews(1);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.True(result.FirstOrDefault().Content == "test");
            Assert.IsType<List<ViewReviewModel>>(result);
        }

        [Fact]
        public void GetUserReviewsShouldReturnReviewsFromUser()
        {
            using var data = PrepareDbForReviews();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetUserReviews("1");

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<List<ViewReviewModel>>(result);
        }

        [Fact]
        public void GetUserReviewsForMovieShouldReturnCorrectUserAndReviews()
        {
            using var data = PrepareDbForReviews();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetUserReviewsForMovie("1", 1);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<List<ViewReviewModel>>(result);
        }

        [Fact]
        public void GetReviewedMoviesShouldReturnReviewsForUser()
        {
            using var data = PrepareDbForReviews();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetReviewedMovies("1");

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<List<ViewMovieModel>>(result);
        }

        [Fact]
        public void GetAllReviewsShouldReturnTwoReviews()
        {
            using var data = PrepareDbForReviews();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetAllReviews();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.IsType<List<CardReviewModel>>(result);
        }

        [Fact]
        public void AddReviewShouldAddNewReview()
        {
            using var data = DatabaseMock.Instance;
            var reviewService = new ReviewService(data);

            reviewService.AddReview(new Review
            {
                Content = "test",
                UserId = "1"
            });

            Assert.Single(data.Reviews);
        }

        [Fact]
        public void RemoveReviewShouldAddNewReview()
        {
            using var data = DatabaseMock.Instance;
            data.Reviews.Add(new Review
            {
                Id = 1,
                Content = "test",
                UserId = "1"
            });
            var reviewService = new ReviewService(data);
            reviewService.RemoveReview(data.Reviews.Find(1));

            Assert.Equal(0, data.Reviews.Count());
        }

        [Fact]
        public void GetCurrentUserShouldReturnCorrectUser()
        {
            using var data = PrepareDbForReviews();
            var reviewService = new ReviewService(data);

            var result = reviewService.GetCurrentUser("1");

            Assert.NotNull(result);
            Assert.True(result.UserName == "test");
            Assert.IsType<User>(result);
        }


        private static ViewMoviesDbContext PrepareDbForReviews()
        {
            var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = "1",
                UserName = "test"
            });
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test"
            });
            data.Reviews.Add(new Review
            {
                Id = 1,
                Content = "test",
                UserId = "1",
                MovieId = 1,
                CreationDate = DateTime.UtcNow,
                Movie = data.Movies.Find(1),
                User = data.Users.FirstOrDefault(u => u.Id == "1")
            });

            data.Users.Add(new User
            {
                Id = "2",
                UserName = "test2"
            });
            data.Movies.Add(new Movie
            {
                Id = 2,
                Title = "test2",
                Description = "test2"
            });
            data.Reviews.Add(new Review
            {
                Id = 2,
                Content = "test2",
                UserId = "2",
                MovieId = 2,
                CreationDate = DateTime.UtcNow,
                Movie = data.Movies.Find(2),
                User = data.Users.FirstOrDefault(u => u.Id == "2")
            });
            data.SaveChanges();

            return data;
        }
    }
}
