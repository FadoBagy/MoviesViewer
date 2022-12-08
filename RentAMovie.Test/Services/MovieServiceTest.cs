namespace RentAMovie.Test.Services
{
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.Review;
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

        [Fact]
        public void GetMovieWithCrewMembersTmdbShouldReturnSingleMovieWithDirectors()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetMovieWithCrewMembersTmdb(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test3");
            Assert.Single(result.Directors);
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetMovieWithActorsTmdbShouldReturnSingleMovieWithActors()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetMovieWithActorsTmdb(1);

            Assert.NotNull(result);
            Assert.True(result.Title == "test3");
            Assert.Single(result.Actors);
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void GetUserMoviesShouldReturnAllMoviesForUser()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetUserMovies("1");

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "test");
            Assert.Single(result);
            Assert.IsType<List<ViewUserMovieCardModel>>(result);
        }

        [Fact]
        public void GetUserMoviesForProfileShouldReturnAllMoviesForUser()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetUserMoviesForProfile("1");

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "test");
            Assert.Single(result);
            Assert.IsType<List<Movie>>(result);
        }

        [Fact]
        public void GetAllUsersMoviesShouldReturnAllMoviesCreatedByUser()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetAllUsersMovies();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.IsType<List<CardMovieModel>>(result);
        }

        [Fact]
        public void GetAllTmdbMoviesShouldReturnAllMoviesNotCreatedByUser()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetAllTmdbMovies();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.IsType<List<CardMovieModel>>(result);
        }
        
        [Fact]
        public void AddMovieShouldAddMovie()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            movieService.AddMovie(new Movie
            {
                Id = 5,
                Title = "test5",
                Description = "test5"
            });

            Assert.Equal(5, data.Movies.Count());
        }

        [Fact]
        public void RemoveMovieShouldRemoveMovie()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            movieService.RemoveMovie(data.Movies.FirstOrDefault());

            Assert.Equal(3, data.Movies.Count());
        }

        [Fact]
        public void GetGenreByNameShouldReturnGenre()
        {
            var data = DatabaseMock.Instance;
            data.Genres.Add(new Genre
            {
                Id = 1,
                Name = "test",
                Movies = new List<Movie> { new Movie 
                {
                    Title = "test",
                    Description = "test"
                }}
            });
            data.SaveChanges();
            var movieService = new MovieService(data);

            var result = movieService.GetGenreByName("test");

            Assert.NotNull(result);
            Assert.IsType<Genre>(result);
        }

        [Fact]
        public void GetLastReviewForMovieShouldReview()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetLastReviewForMovie(1);

            Assert.NotNull(result);
            Assert.True(result.Content == "last review");
            Assert.IsType<Review>(result);
        }

        [Fact]
        public void GetLastUserReviewShouldReview()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetLastUserReview("1");

            Assert.NotNull(result);
            Assert.True(result.Content == "last review");
            Assert.IsType<ViewReviewModel>(result);
        }

        [Fact]
        public void GetActorTmdbShouldReturnActor()
        {
            var data = DatabaseMock.Instance;
            data.Actors.Add(new Actor
            {
                Id = 1,
                TmdbId = 1,
                Name = "test",
                PlayedInMovies = new List<Movie> 
                {
                    new Movie 
                    {
                        Id = 1,
                        Title = "test",
                        Description = "test"
                    }
                }
            });
            data.SaveChanges();
            var movieService = new MovieService(data);

            var result = movieService.GetActorTmdb(1);

            Assert.NotNull(result);
            Assert.True(result.Name == "test");
            Assert.IsType<Actor>(result);
        }

        [Fact]
        public void GetCrewMemberTmdbShouldReturnDirector()
        {
            var data = DatabaseMock.Instance;
            data.Directors.Add(new Director
            {
                Id = 1,
                TmdbId = 1,
                Name = "test",
                DirectedMovies = new List<Movie>
                {
                    new Movie
                    {
                        Id = 1,
                        Title = "test",
                        Description = "test"
                    }
                }
            });
            data.SaveChanges();
            var movieService = new MovieService(data);

            var result = movieService.GetCrewMemberTmdb(1);

            Assert.NotNull(result);
            Assert.True(result.Name == "test");
            Assert.IsType<Director>(result);
        }

        [Fact]
        public void AddActorToMovieShouldAddActor()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            movieService.AddActorToMovie(1, new Actor
            {
                Name = "test"
            });

            var movie = data.Movies.FirstOrDefault(m => m.TmdbId == 1);
            Assert.Equal(2, movie.Actors.Count());
        }

        [Fact]
        public void AddCrewMemberToMovieShouldAddDirecor()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            movieService.AddCrewMemberToMovie(1, new Director
            {
                Name = "test"
            });

            var movie = data.Movies.FirstOrDefault(m => m.TmdbId == 1);
            Assert.Equal(2, movie.Directors.Count());
        }

        [Fact]
        public void IsActorPresentShouldReturnTrue()
        {
            var data = DatabaseMock.Instance;
            data.Actors.Add(new Actor
            {
                TmdbId = 1,
                Name = "test"
            });
            data.SaveChanges();
            var movieService = new MovieService(data);

            var result = movieService.IsActorPresent(1);

            Assert.True(result);
        }

        [Fact]
        public void IsCrewMemberPresentShouldReturnTrue()
        {
            var data = DatabaseMock.Instance;
            data.Directors.Add(new Director
            {
                TmdbId = 1,
                Name = "test"
            });
            data.SaveChanges();
            var movieService = new MovieService(data);

            var result = movieService.IsCrewMemberPresent(1);

            Assert.True(result);
        }

        [Fact]
        public void SaveChangesShouldSave()
        {
            var data = DatabaseMock.Instance;
            data.Directors.Add(new Director
            {
                Name = "test"
            });
            var movieService = new MovieService(data);

            movieService.SaveChanges();

            Assert.True(data.Directors.Count() > 0);
        }

        [Fact]
        public void GetUserByIdShouldReturnUser()
        {
            using var data = PrepareDbWithMovies();
            var movieService = new MovieService(data);

            var result = movieService.GetUserById("1");

            Assert.NotNull(result);
            Assert.True(result.UserName == "TestUser");
            Assert.IsType<User>(result);
        }


        private static ViewMoviesDbContext PrepareDbWithMovies()
        {
            var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = "1",
                UserName = "TestUser"
            });

            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test",
                GenresCollection = new List<Genre> { new Genre { Name = "test" } },
                Reviews = new List<Review> 
                { 
                    new Review 
                    {
                        Id = 1,
                        Content = "last review",
                        CreationDate = DateTime.Parse("01/01/2010"),
                        MovieId = 1,
                        UserId = "1"
                    },
                    new Review
                    {
                        Id = 2,
                        Content = "first review",
                        CreationDate = DateTime.Parse("01/01/2000"),
                        MovieId = 1,
                        UserId = "1"
                    }
                },
                UserId = "1"
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
                GenresCollection = new List<Genre> { new Genre { Name = "test" } },
                Directors = new List<Director> { new Director { Name = "test" } },
                Actors = new List<Actor> { new Actor { Name = "test" } }
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
