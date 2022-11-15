namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data;
    using RentAMovie.Models.Genre;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Search;

    public class SearchController : Controller
    {
        private readonly ViewMoviesDbContext data;
        public SearchController(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        [Route("/Search")]
        public IActionResult Index([FromQuery]SearchQueryModel query)
        {
            var movieQuery = data.Movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                movieQuery = movieQuery
                    .Where(m => m.Title.Contains(query.SearchTerm))
                    .OrderByDescending(m => m.DatePublished);
            }

            var genres = data.Genres
                .OrderBy(g => g.Name)
                .Select(g => new ViewGenreModel
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList();

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
            return View(query);
        }
    }
}
