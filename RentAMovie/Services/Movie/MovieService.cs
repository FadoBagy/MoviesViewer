namespace RentAMovie.Services.Movie
{
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using System.Collections.Generic;

    public class MovieService : IMovieService
    {
        private readonly ViewMoviesDbContext data;
        public MovieService(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        public Movie GetMovie(int id)
        {
            return data.Movies.Find(id);
        }

        public Movie GetMovieTmdb(int id)
        {
            return data.Movies.FirstOrDefault(m => m.TmdbId == id);
        }

        public Movie GetMovieWithGenresCollection(int id)
        {
            return data.Movies
                .Include(m => m.GenresCollection)
                .FirstOrDefault(m => m.Id == id);
        }

        public Movie GetMovieWithGenresCollectionTmdb(int id)
        {
            return data.Movies
                .Include(m => m.GenresCollection)
                .FirstOrDefault(m => m.TmdbId == id);
        }

        public List<ViewUserMovieCardModel> GetUserMovies(string userId)
        {
            return data.Movies
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.DateCreated)
                .Select(m => new ViewUserMovieCardModel
                {
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.DatePublished,
                    PosterPath = m.Poster,
                    Id = m.Id
                })
                .ToList();
        }

        public void AddMovie(Movie movie)
        {
            data.Movies.Add(movie);
            data.SaveChanges();
        }

        public void RemoveMovie(Movie movie)
        {
            data.Movies.Remove(movie);
            data.SaveChanges();
        }

        public Genre GetGenreByName(string name)
        {
            return data.Genres.FirstOrDefault(g => g.Name == name);
        }

        public Review GetLastReviewForMovie(int movieId)
        {
            return data.Reviews
                .Where(r => r.MovieId == movieId)
                .OrderByDescending(r => r.CreationDate)
                .FirstOrDefault();
        }

        public void AddActorToMovie(int movieId, Actor actor)
        {
            data.Movies.FirstOrDefault(m => m.TmdbId == movieId).Actors.Add(actor);
        }

        public bool IsActorPresent(int id)
        {
            return data.Actors.Any(a => a.TmdbId == id);
        }

        public void SaveChanges()
        {
            data.SaveChanges();
        }
    }
}
