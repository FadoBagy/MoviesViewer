namespace RentAMovie.Services.Movie
{
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;

    public interface IMovieService
    {
        Movie GetMovie(int id);

        Movie GetMovieTmdb(int id);

        Movie GetMovieWithGenresCollection(int id);

        Movie GetMovieWithGenresCollectionTmdb(int id);

        List<ViewUserMovieCardModel> GetUserMovies(string userId);

        void AddMovie(Movie movie);

        void RemoveMovie(Movie movie);

        Genre GetGenreByName(string name);

        Review GetLastReviewForMovie(int movieId);

        void AddActorToMovie(int movieId, Actor actor);

        bool IsActorPresent(int id);

        void SaveChanges();
    }
}
