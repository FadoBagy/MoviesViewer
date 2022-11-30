namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
