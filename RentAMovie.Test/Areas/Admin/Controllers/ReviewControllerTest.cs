namespace RentAMovie.Test.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Areas.Admin.Controllers;
    using RentAMovie.Areas.Admin.Models.Review;
    using RentAMovie.Services.Review;
    using RentAMovie.Test.Mocks;

    public class ReviewControllerTest
    {
        [Fact]
        public void IndexShouldNotReturnNullView()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectView()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnCorrectViewModel()
        {
            using var data = DatabaseMock.Instance;
            var service = new ReviewService(data);
            var reviewController = new ReviewController(service);

            var result = reviewController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<CardReviewModel>>(viewResult.Model);
        }
    }
}
