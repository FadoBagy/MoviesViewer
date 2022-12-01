namespace RentAMovie.Services.Review
{
    using RentAMovie.Areas.Admin.Models.Review;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;

    public interface IReviewService
    {
        Movie GetMovie(int movieId);

        Movie GetMovieWithReviews(int movieId);

        Review GetReview(int reviewId);

        List<ViewReviewModel> GetReviews(int movieId);

        List<ViewReviewModel> GetUserReviews(string userId);

        List<ViewReviewModel> GetUserReviewsForMovie(string userId, int movieId);

        List<ViewMovieModel> GetReviewedMovies(string userId);

        List<CardReviewModel> GetAllReviews();

        void AddReview(Review review);

        void RemoveReview(Review review);

        User GetCurrentUser(string userId);
    }
}
