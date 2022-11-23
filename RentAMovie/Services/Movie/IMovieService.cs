namespace RentAMovie.Services.Movie
{
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;

    public interface IMovieService
    {
        Movie GetMovie(int id);

        Movie GetMovieTmdb(int id);

        Movie GetMovieWithGenresCollection(int id);

        Movie GetMovieWithGenresCollectionTmdb(int id);

        List<ViewUserMovieCardModel> GetUserMovies(string userId);

        List<Movie> GetUserMoviesForProfile(string userId);

        void AddMovie(Movie movie);

        void RemoveMovie(Movie movie);

        Genre GetGenreByName(string name);

        Review GetLastReviewForMovie(int movieId);

        ViewReviewModel GetLastUserReview(string userId);

        void AddActorToMovie(int movieId, Actor actor);

        void AddCrewMemberToMovie(int movieId, Director crewMember);

        bool IsActorPresent(int id);

        bool IsCrewMemberPresent(int id);

        void SaveChanges();

        User GetUserById(string id);
    }
}
