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
            var movieQuery = data.Movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                movieQuery = movieQuery
                    .Where(m => m.Title.Contains(searchTerm))
                    .OrderByDescending(m => m.DatePublished);
            }

            return View(new SearchQueryModel()
            {
                SearchTerm = searchTerm,
                Movies = movieQuery.ToList()
            });
        }
    }
}
