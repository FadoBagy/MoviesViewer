namespace RentAMovie.Services.Search
{
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Genre;
    using System.Collections.Generic;

    public class SearchService : ISearchService
    {
        private readonly ViewMoviesDbContext data;
        public SearchService(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        public List<ViewGenreModel> GetAllGenres()
        {
            return data.Genres
                .OrderBy(g => g.Name)
                .Select(g => new ViewGenreModel
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList();
        }

        public IQueryable<Movie> ValidateSearchQuery(SearchServiceModel query)
        {
            var movieQuery = data.Movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                movieQuery = movieQuery
                    .Where(m => m.Title.Contains(query.SearchTerm) && m.IsPublic == true)
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
                    //case 3:
                    //    break;
                    //case 4:
                    //    break;
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
                    case 9:
                        movieQuery = movieQuery
                            .OrderByDescending(m => m.VoteCount)
                            .ThenBy(m => m.Title);
                        break;
                    case 10:
                        movieQuery = movieQuery
                           .OrderBy(m => m.VoteCount)
                           .ThenBy(m => m.Title);
                        break;
                }
            }

            if (query.Genre != null)
            {
                movieQuery = movieQuery
                    .Where(m => m.GenresCollection.Any(g => g.Id == query.Genre));
            }

            return movieQuery;
        }
    }
}
