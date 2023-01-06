namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Search;
    using RentAMovie.Services.Search;
    using System.Linq;

    public class SearchController : Controller
    {
        private readonly ISearchService service;
        public SearchController(ISearchService service)
        {
            this.service = service;
        }

        [Route("/Search")]
        public IActionResult Index([FromQuery]SearchQueryModel query)
        {
            var movieQuery = service.ValidateSearchQuery(new SearchServiceModel
            {
                CurrentPage = query.CurrentPage,
                SearchTerm = query.SearchTerm,
                Genre = query.Genre,
                SortBy = query.SortBy
            });

            var movies = movieQuery
                //.Skip((query.CurrentPage - 1) * SearchQueryModel.MoviesPerPage)
                //.Take(SearchQueryModel.MoviesPerPage)
                .Select(m => new SearchMovieModel
                {
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.DatePublished,
                    Rating = m.Rating.ToString(),
                    TmdbId = (int)m.TmdbId,
                    Id = m.Id,
                    VoteCount = (int)m.VoteCount,
                    PosterPath = m.Poster
                })
                .ToList();

            if (movies != null)
            {
                query.Movies.AddRange(movies);
            }
            query.SearchTerm = query.SearchTerm;
            query.AvailableGenres = service.GetAllGenres();
            return View(query);
        }

        //[HttpPost]
        //public IActionResult IndexRedirect()
        //{
        //    return RedirectToAction("Index", "Search", new SearchQueryModel { SearchTerm = "a" });
        //}
    }
}
