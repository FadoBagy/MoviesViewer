namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorAreaName)]
    public abstract class AdminController : Controller
    {

    }
}
