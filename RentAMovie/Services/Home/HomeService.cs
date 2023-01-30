namespace RentAMovie.Services.Home
{
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly ViewMoviesDbContext data;

        public HomeService(ViewMoviesDbContext data)
        {
            this.data = data;
        }
		
		public int GetMovieCount()
        {
            return data.Movies.Count();
        }

        public int GetReviewCount()
        {
            return data.Reviews.Count();
        }

        public int GetUserCount()
        {
            return data.Users.Count();
        }

		public int GetActorCount()
		{
			return data.Actors.Count();
		}

		public int GetCastCount()
		{
			return data.Directors.Count();
		}

        public List<CardMovieModel> GetLatestMovies()
        {
            return data.Movies
                    .OrderByDescending(m => m.Id)
                    .Select(m => new CardMovieModel
                    {
                        Id = m.Id,
                        TmdbId = m.TmdbId,
                        Poster = m.Poster,
                        Title = m.Title,
                        Rating = (m.Rating).ToString()
                    })
                    .Take(20)
                    .ToList();
        }

        public List<CardMovieModel> GetWatchlistMovies(string userId)
        {
            return data.UsersMovies
                .Where(um => um.UserId == userId)
                .OrderByDescending(um => um.Movie.Rating)
                .Select(um => new CardMovieModel
                {
                    Id = um.Movie.Id,
                    TmdbId = um.Movie.TmdbId,
                    Title = um.Movie.Title,
                    Poster = um.Movie.Poster,
                    Rating = (um.Movie.Rating).ToString()
                })
                .Take(15)
                .ToList();
        }
    }
}
