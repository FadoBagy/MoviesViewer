namespace RentAMovie.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using RentAMovie.Areas.Admin.Models.Home;
	using RentAMovie.Services.Home;

	public class HomeController : AdminController
	{
		private readonly IHomeService service;

		public HomeController(IHomeService service)
		{
			this.service = service;
		}
		public IActionResult Index()
		{
			return View(new ViewHomeModel
			{
				TotalMovies = service.GetMovieCount(),
				TotalReviews = service.GetReviewCount(),
				TotalUsers = service.GetUserCount(),
				TotalActors = service.GetActorCount(),
				TotalCast = service.GetCastCount()
			});
		}
	}
}
