namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MovieController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
