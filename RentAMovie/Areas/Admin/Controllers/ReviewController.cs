namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Services.Review;

    public class ReviewController : AdminController
    {
        private readonly IReviewService service;
        public ReviewController(IReviewService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var reviews = service.GetAllReviews();
            return View(reviews);
        }
    }
}
