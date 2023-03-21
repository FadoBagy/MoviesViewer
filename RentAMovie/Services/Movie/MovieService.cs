namespace RentAMovie.Services.Movie
{
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;
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

        public Movie GetMovieWithCrewMembersTmdb(int id)
        {
            return data.Movies
                .Include(m => m.Directors)
                .FirstOrDefault(m => m.TmdbId == id);
        }

        public Movie GetMovieWithActorsTmdb(int id)
        {
            return data.Movies
                .Include(m => m.Actors)
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
                    Id = m.Id,
                    IsPublic = m.IsPublic
                })
                .ToList();
        }

        public List<Movie> GetUserMoviesForProfile(string userId)
        {
            return data.Movies
                .Where(m => m.UserId == userId)
                .ToList()
                .OrderByDescending(m => m.DateCreated)
                .Take(3)
                .ToList();
        }

        public List<CardMovieModel> GetAllUnapprovedMovies()
        {
            return data.Movies
                .Where(m => m.IsPublic == false)
                .OrderByDescending(m => m.DateCreated)
                .Select(m => new CardMovieModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Poster = m.Poster,
                    DatePublished = m.DatePublished,
                    TmdbId = m.TmdbId
                })
                .ToList();
        }

        public List<CardMovieModel> GetAllUsersMovies()
		{
			return data.Movies
                .Where(m => m.TmdbId == null && m.IsPublic == true)
                .OrderByDescending(m => m.DateCreated)
                .Select(m => new CardMovieModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Poster = m.Poster,
                    DatePublished = m.DatePublished
                })
                .ToList();
		}

		public List<CardMovieModel> GetAllTmdbMovies()
		{
			return data.Movies
				.Where(m => m.TmdbId != null && m.IsPublic == true)
				.OrderByDescending(m => m.DateCreated)
				.Select(m => new CardMovieModel
				{
					Id = m.Id,
                    TmdbId = m.TmdbId,
					Title = m.Title,
                    Poster = m.Poster,
					DatePublished = m.DatePublished
				})
				.ToList();
		}

        public List<SearchMovieModel> GetWatchlistedMovies(string userId)
        {
            return data.UsersMovies
                    .Where(um => um.UserId == userId && um.Movie.IsPublic == true)
                    .Select(um => new SearchMovieModel
					{
                        Id = um.Movie.Id,
                        TmdbId= um.Movie.TmdbId,
                        Title = um.Movie.Title,
                        Poster = um.Movie.Poster,
                        Rating = um.Movie.Rating.ToString(),
                        ReleaseDate = um.Movie.DatePublished,
                        VoteCount = um.Movie.VoteCount
                    })
                    .OrderByDescending(m => m.ReleaseDate)
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

        public void AddToWatchlist(string userId, int movieId)
        {
            data.UsersMovies.Add(new UserMovie
            {
                UserId = userId,
                MovieId = movieId
            });
            data.SaveChanges();
        }

        public void RemoveFromWatchlist(string userId, int movieId)
        {
            var userMovie = data.UsersMovies
                .Where(um => um.UserId == userId && um.MovieId == movieId)
                .FirstOrDefault();
            data.UsersMovies.Remove(userMovie);
            data.SaveChanges();
        }

        public bool IsWatchlisted(string userId, int movieId)
        {
            if (data.UsersMovies.Any(um => um.UserId == userId && um.MovieId == movieId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRatedByUser(string userId, int movieId)
        {
            if (data.UsersMoviesRatings.Any(umr => umr.UserId == userId && umr.MovieId == movieId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeVisibility(int id)
        {
            var movie = data.Movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
				if (movie.IsPublic == false)
				{
					movie.IsPublic = true;
				}
				else
				{
					movie.IsPublic = false;
				}
			}
            data.SaveChanges();
        }

        public void RateMovie(string userId, int movieId, float rating)
        {
            var movie = GetMovieWithGenresCollection(movieId);
            bool alreadyRated = false;
            if (data.UsersMoviesRatings.Any(umr => umr.UserId == userId && umr.MovieId == movieId))
            {
                alreadyRated = true;
            }

            if (alreadyRated)
            {
                RemoveMovieRating(userId, movieId);
            }

            data.UsersMoviesRatings.Add(new UserMovieRating
            {
                UserId = userId,
                MovieId = movieId,
                Rating = rating
            });
            if (movie.VoteCount == null)
            {
                movie.VoteCount = 1;
            }
            else
            {
                movie.VoteCount++;
            }
            if (movie.VoteCount <= 1)
            {
                movie.Rating = rating;
            }
            else
            {
                movie.Rating = (movie.Rating * (movie.VoteCount - 1) + rating) / movie.VoteCount;
            }

            data.SaveChanges();
        }

        public void RemoveMovieRating(string userId, int movieId)
        {
            var movie = GetMovieWithGenresCollection(movieId);
            bool alreadyRated = false;
            if (data.UsersMoviesRatings.Any(umr => umr.UserId == userId && umr.MovieId == movieId))
            {
                alreadyRated = true;
            }

            if (alreadyRated)
            {
                data.UsersMoviesRatings.Remove(data.UsersMoviesRatings.FirstOrDefault(umr => umr.UserId == userId && umr.MovieId == movieId));
                data.SaveChanges();
                movie.VoteCount--;

                float[] newRatings = data.UsersMoviesRatings
                                        .Where(umr => umr.MovieId == movieId)
                                        .Select(umr => umr.Rating)
                                        .ToArray();
                var asd = newRatings.Average();
                movie.Rating = newRatings.Average();

                data.SaveChanges();
            }
        }

        public float GetCurrentUserMovieRating(string userId, int movieId)
        {
            if (IsRatedByUser(userId, movieId))
            {
                return data.UsersMoviesRatings
                        .FirstOrDefault(umr => umr.UserId == userId && umr.MovieId == movieId)
                        .Rating;
            }
            else
            {
                return 0.0f;
            }
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

        public ViewReviewModel GetLastUserReview(string userId)
        {
            return data.Reviews
                .Where(r => r.UserId == userId)
                .Select(r => new ViewReviewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreationDate = r.CreationDate,
                    MovieInfo = new Movie
                    {
                        Id = r.MovieId,
                        Title = r.Movie.Title
                    }
                })
                .OrderByDescending(r => r.CreationDate)
                .FirstOrDefault();
        }

        public Actor GetActorTmdb(int id)
        {
            return data.Actors.FirstOrDefault(a => a.TmdbId == id);
        }

        public Director GetCrewMemberTmdb(int id)
        {
            return data.Directors.FirstOrDefault(d => d.TmdbId == id);
        }

        public void AddActorToMovie(int movieId, Actor actor)
        {
            data.Movies.FirstOrDefault(m => m.TmdbId == movieId).Actors.Add(actor);
        }

        public void AddCrewMemberToMovie(int movieId, Director crewMember)
        {
            data.Movies.FirstOrDefault(m => m.TmdbId == movieId).Directors.Add(crewMember);
        }

        public bool IsActorPresent(int id)
        {
            return data.Actors.Any(a => a.TmdbId == id);
        }

        public bool IsCrewMemberPresent(int id)
        {
            return data.Directors.Any(a => a.TmdbId == id);
        }

        public void SaveChanges()
        {
            data.SaveChanges();
        }

        public User GetUserById(string id)
        {
            return data.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}