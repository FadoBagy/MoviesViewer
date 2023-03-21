namespace RentAMovie.Services.Movie
{
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;

    public interface IMovieService
    {
        Movie GetMovie(int id);

        Movie GetMovieTmdb(int id);

        Movie GetMovieWithGenresCollection(int id);

        Movie GetMovieWithGenresCollectionTmdb(int id);

        Movie GetMovieWithCrewMembersTmdb(int id);

        Movie GetMovieWithActorsTmdb(int id);

        List<ViewUserMovieCardModel> GetUserMovies(string userId);

        List<Movie> GetUserMoviesForProfile(string userId);

        List<CardMovieModel> GetAllUnapprovedMovies();

        List<CardMovieModel> GetAllUsersMovies();

        List<CardMovieModel> GetAllTmdbMovies();

        List<SearchMovieModel> GetWatchlistedMovies(string userId);

        void AddMovie(Movie movie);

        void RemoveMovie(Movie movie);

        void AddToWatchlist(string userId, int movieId);

        void RemoveFromWatchlist(string userId, int movieId);

        bool IsWatchlisted(string userId, int movieId);

        bool IsRatedByUser(string userId, int movieId);

        void ChangeVisibility(int id);

        void RateMovie(string userId, int movieId, float rating);

        void RemoveMovieRating(string userId, int movieId);

        float GetCurrentUserMovieRating(string userId, int movieId);

		Genre GetGenreByName(string name);

        Review GetLastReviewForMovie(int movieId);

        ViewReviewModel GetLastUserReview(string userId);

        Actor GetActorTmdb(int id);

        Director GetCrewMemberTmdb(int id);

        void AddActorToMovie(int movieId, Actor actor);

        void AddCrewMemberToMovie(int movieId, Director crewMember);

        bool IsActorPresent(int id);

        bool IsCrewMemberPresent(int id);

        void SaveChanges();

        User GetUserById(string id);
    }
}
