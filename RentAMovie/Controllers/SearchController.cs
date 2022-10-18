namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data;
    using RentAMovie.Models.Search;

    public class SearchController : Controller
    {
        private readonly ViewMoviesDbContext data;
        public SearchController(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        [Route("/Search")]
        public IActionResult Index(string searchTerm)
        {
            var movies = data.Movies
                .Where(m => m.Title.Contains(searchTerm))
                .OrderByDescending(m => m.DatePublished)
                .ToList();

            return View(new SearchQueryModel()
            {
                SearchTerm = searchTerm,
                Movies = movies
            });
        }
    }
}
