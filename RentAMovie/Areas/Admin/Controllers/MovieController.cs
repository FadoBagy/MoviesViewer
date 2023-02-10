namespace RentAMovie.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Services.Movie;

    public class MovieController : AdminController
    {
		private readonly IMovieService service;
		public MovieController(IMovieService service)
		{
			this.service = service;
		}

		public IActionResult Index()
        {
            return View(new MovieViewModel
            {
                UnapprovedMovies = service.GetAllUnapprovedMovies(),
                UsersMovies = service.GetAllUsersMovies(),
                TmdbMovies = service.GetAllTmdbMovies()
            });
        }

        public IActionResult ChangeVisibility(int id)
        {
            service.ChangeVisibility(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
