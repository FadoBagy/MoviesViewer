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
    }
}
