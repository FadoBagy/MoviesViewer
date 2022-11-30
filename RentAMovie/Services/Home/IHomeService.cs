namespace RentAMovie.Services.Home
{
    public interface IHomeService
    {
        int GetMovieCount();

        int GetUserCount();

        int GetReviewCount();

        int GetActorCount();

        int GetCastCount();
    }
}
