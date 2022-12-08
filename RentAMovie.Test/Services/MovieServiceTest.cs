namespace RentAMovie.Test.Services
{
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Services.Movie;
    using RentAMovie.Test.Mocks;

    public class MovieServiceTest
    {
        [Fact]
        public void GetMovieShouldReturnSingleMovie()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetMovie(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test");
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetMovieTmdbShouldReturnSingleMovie()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetMovieTmdb(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test3");
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetMovieWithGenresCollectionShouldReturnSingleMovieWithGenres()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetMovieWithGenresCollection(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test");
            Assert.Single(result.GenresCollection);
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetMovieWithGenresCollectionTmdbShouldReturnSingleMovieWithGenres()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetMovieWithGenresCollectionTmdb(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test3");
            Assert.Single(result.GenresCollection);
            Assert.IsType<Movie>(result);
        }

        private static ViewMoviesDbContext PrepareDbWithMovies()
        {
            var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test",
                GenresCollection = new List<Genre> { new Genre { Name = "test" } }
            });
            data.Movies.Add(new Movie
            {
                Id = 2,
                Title = "test2",
                Description = "test2"
            });

            data.Movies.Add(new Movie
            {
                Id = 3,
                TmdbId = 1,
                Title = "test3",
                Description = "test3",
                GenresCollection = new List<Genre> { new Genre { Name = "test" } }
            });
            data.Movies.Add(new Movie
            {
                Id = 4,
                TmdbId = 2,
                Title = "test4",
                Description = "test4"
            });
            data.SaveChanges();

            return data;
        }
    }
}
