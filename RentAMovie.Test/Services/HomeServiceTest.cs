namespace RentAMovie.Test.Services
{
    using RentAMovie.Data.Models;
    using RentAMovie.Services.Home;
    using RentAMovie.Test.Mocks;

    public class HomeServiceTest
    {
        [Fact]
        public void GetMovieCountShouldReturnOne()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Title = "Title",
                Description = "Description"
            });
            //data.Movies
            //    .AddRange(Enumerable.Range(0, 10)
            //    .Select(i => new Movie()));
            data.SaveChanges();
            var homeService = new HomeService(data);

            var result = homeService.GetMovieCount();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetReviewCountShouldReturnOne()
        {
            using var data = DatabaseMock.Instance;
            data.Reviews.Add(new Review
            {
               Content = "Content",
               UserId = "TestUserId"
            });
            data.SaveChanges();
            var homeService = new HomeService(data);

            var result = homeService.GetReviewCount();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetUserCountShouldReturnOne()
        {
            using var data = DatabaseMock.Instance;
            data.Users.Add(new User());
            data.SaveChanges();
            var homeService = new HomeService(data);

            var result = homeService.GetUserCount();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetActorCountShouldReturnOne()
        {
            using var data = DatabaseMock.Instance;
            data.Actors.Add(new Actor
            {
                Name = "Test"
            });
            data.SaveChanges();
            var homeService = new HomeService(data);

            var result = homeService.GetActorCount();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCastCountShouldReturnOne()
        {
            using var data = DatabaseMock.Instance;
            data.Directors.Add(new Director
            {
                Name = "Test"
            });
            data.SaveChanges();
            var homeService = new HomeService(data);

            var result = homeService.GetCastCount();

            Assert.Equal(1, result);
        }
    }
}
