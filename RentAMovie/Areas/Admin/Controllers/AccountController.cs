namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Services.Account;

    using static AdminConstants;

    public class AccountController : AdminController
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var users = service.GetAllUsers();
            return View(users);
        }

        public IActionResult Delete(string userId) 
        {
            var user = service.GetUser(userId);
            if (user == null) 
            {
                throw new NullReferenceException();
            }

            service.RemoveUser(user);

            return RedirectToAction("Index", "Account", new { area = AdministratorAreaName });
        }
    }
}
