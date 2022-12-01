namespace RentAMovie.Services.Review
{
    using Microsoft.EntityFrameworkCore;
	using RentAMovie.Areas.Admin.Models.Review;
	using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;
    using System.Collections.Generic;

    public class ReviewService : IReviewService
    {
        private readonly ViewMoviesDbContext data;
        public ReviewService(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        public Movie GetMovie(int movieId)
        {
            return data.Movies.Find(movieId);
        }

        public Movie GetMovieWithReviews(int movieId)
        {
            return data.Movies
                .Include(m => m.Reviews)
                .FirstOrDefault(m => m.Id == movieId);
        }

        public Review GetReview(int reviewId)
        {
            return data.Reviews.Find(reviewId);
        }

        public List<ViewReviewModel> GetReviews(int movieId)
        {
            var movie = GetMovieWithReviews(movieId);
            return data.Reviews
                .Where(r => r.MovieId == movieId)
                .OrderByDescending(r => r.CreationDate)
                .Select(r => new ViewReviewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreationDate = r.CreationDate,
                    MovieInfo = new Movie
                    {
                        Id = movie.Id,
                        TmdbId = movie.TmdbId,
                        Title = movie.Title,
                        Poster = movie.Poster,
                        BackdropPath = movie.BackdropPath,
                        DatePublished = movie.DatePublished
                    },
                    UserId = r.UserId
                })
                .ToList();
        }

        public List<ViewReviewModel> GetUserReviews(string userId)
        {
            return data.Reviews
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreationDate)
                .Select(r => new ViewReviewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreationDate = r.CreationDate,
                    UserId = userId,
                    MovieInfo = new Movie
                    {
                        Id = r.Movie.Id,
                        Title = r.Movie.Title
                    }
                })
                .ToList();
        }

        public List<ViewReviewModel> GetUserReviewsForMovie(string userId, int movieId)
        {
            return data.Reviews
                .Where(r => r.UserId == userId && r.MovieId == movieId)
                .OrderByDescending(r => r.CreationDate)
                .Select(r => new ViewReviewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreationDate = r.CreationDate,
                    UserId = userId,
                    MovieInfo = new Movie
                    {
                        Id = r.Movie.Id,
                        Title = r.Movie.Title
                    }
                })
                .ToList();
        }

        public List<ViewMovieModel> GetReviewedMovies(string userId)
        {
            return data.Reviews
                .Where(r => r.UserId == userId)
                .Select(r => new ViewMovieModel
                {
                    Id = r.Movie.Id,
                    Title = r.Movie.Title
                })
                .OrderByDescending(m => m.Title)
                .Distinct()
                .ToList();
        }

		public List<CardReviewModel> GetAllReviews()
		{
			return data.Reviews
                .OrderByDescending(r => r.CreationDate)
                .Select(r => new CardReviewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreationDate = r.CreationDate,
                    MovieInfo = r.Movie,
                    UserId = r.UserId,
                    UserUsername = r.User.UserName,
                    UserPhoto = r.User.Photo
                })
                .ToList();
		}

		public void AddReview(Review review)
        {
            data.Reviews.Add(review);
            data.SaveChanges();
        }

        public void RemoveReview(Review review)
        {
            data.Reviews.Remove(review);
            data.SaveChanges();
        }

        public User GetCurrentUser(string userId)
        {
            return data.Users.FirstOrDefault(u => u.Id == userId);
        }
	}
}
