namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
