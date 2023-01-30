namespace RentAMovie.Services.Home
{
    using RentAMovie.Areas.Admin.Models.Movie;

    public interface IHomeService
    {
        int GetMovieCount();

        int GetUserCount();

        int GetReviewCount();

        int GetActorCount();

        int GetCastCount();

        List<CardMovieModel> GetLatestMovies();

        List<CardMovieModel> GetWatchlistMovies(string userId);
    }
}
