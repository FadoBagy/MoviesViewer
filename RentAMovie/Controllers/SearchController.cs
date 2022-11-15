namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data;
    using RentAMovie.Models.Genre;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Search;
    using System.Linq.Expressions;

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

            if (query.SortBy != null)
            {
                switch (query.SortBy)
                {
                    case 1:
                        movieQuery = movieQuery
                            .OrderByDescending(m => m.Rating)
                            .ThenBy(m => m.Title);
                        break;
                    case 2:
                        movieQuery = movieQuery
                            .OrderBy(m => m.Rating)
                            .ThenBy(m => m.Title);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        movieQuery = movieQuery
                            .OrderByDescending(m => m.DatePublished)
                            .ThenBy(m => m.Title);
                        break;
                    case 6:
                        movieQuery = movieQuery
                            .OrderBy(m => m.DatePublished)
                            .ThenBy(m => m.Title);
                        break;
                    case 7:
                        movieQuery = movieQuery
                            .OrderBy(m => m.Title)
                            .ThenBy(m => m.DateCreated);
                        break;
                    case 8:
                        movieQuery = movieQuery
                            .OrderByDescending(m => m.Title)
                            .ThenBy(m => m.DateCreated);
                        break;
                }
            }

            if (query.Genre != null)
            {
                movieQuery = movieQuery
                    .Where(m => m.GenresCollection.Any(g => g.Id == query.Genre));
            }

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

            var genres = data.Genres
                .OrderBy(g => g.Name)
                .Select(g => new ViewGenreModel
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList();

            if (movies != null)
            {
                query.Movies.AddRange(movies);
            }
            query.SearchTerm = query.SearchTerm;
            query.AvailableGenres = genres;
            return View(query);
        }
    }
}
