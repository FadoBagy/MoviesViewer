namespace RentAMovie.Test.Services
{
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;
    using RentAMovie.Services.Person;
    using RentAMovie.Test.Mocks;

    public class PersonServiceTest
    {
        [Fact]
        public void GetDirectorWithMoviesShouldReturnSingleDirector()
        {
            using var data = DatabaseMock.Instance;
            data.Directors.Add(new Director
            {
                TmdbId = 1,
                Name = "test"
            });
            data.SaveChanges();
            var personService = new PersonService(data, MapperMock.Instance);

            var result = personService.GetDirectorWithMovies(1);

            Assert.NotNull(result);
            Assert.IsType<Director>(result);
        }

        [Fact]
        public void GetActorWithMoviesShouldReturnSingleActor()
        {
            using var data = DatabaseMock.Instance;
            data.Actors.Add(new Actor
            {
                TmdbId = 1,
                Name = "test"
            });
            data.SaveChanges();
            var personService = new PersonService(data, MapperMock.Instance);

            var result = personService.GetActorWithMovies(1);

            Assert.NotNull(result);
            Assert.IsType<Actor>(result);
        }

        [Fact]
        public void GetMovieTmdbShouldReturnSingleMovie()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                TmdbId = 1,
                Title = "test",
                Description = "test"
            });
            data.SaveChanges();
            var personService = new PersonService(data, MapperMock.Instance);

            var result = personService.GetMovieTmdb(1);

            Assert.NotNull(result);
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public void ValidateDirectorDataShouldAddDirector()
        {
            using var data = DatabaseMock.Instance;
            var personService = new PersonService(data, MapperMock.Instance);

            personService.ValidateDirectorData(new ViewTmdbSinglePersonModel
            {
                TmdbId = 1,
                Name = "test"
            });

            Assert.Single(data.Directors);
        }

        [Fact]
        public void ValidateDirectorDataShouldChangeDirectorData()
        {
            using var data = DatabaseMock.Instance;
            data.Directors.Add(new Director
            {
                TmdbId = 1,
                Name = "test"
            });
            data.SaveChanges();
            var personService = new PersonService(data, MapperMock.Instance);

            personService.ValidateDirectorData(new ViewTmdbSinglePersonModel
            {
                TmdbId = 1,
                Name = "Test Test",
                Biography = "test",
                Photo = "test",
                Gender = 1,
                DateOfBirth = "test",
                DeathDay = "test",
                PlaceOfBirth = "test"
            });

            var director = data.Directors.FirstOrDefault();
            Assert.True(director.TmdbId == 1);
            Assert.True(director.Name == "Test Test");
            Assert.True(director.Biography == "test");
            Assert.True(director.Photo == "test");
            Assert.True(director.Gender == 1);
            Assert.True(director.DateOfBirth == "test");
            Assert.True(director.DeathDay == "test");
            Assert.True(director.PlaceOfBirth == "test");
        }

        [Fact]
        public void ValidateActorDataShouldAddActor()
        {
            using var data = DatabaseMock.Instance;
            var personService = new PersonService(data, MapperMock.Instance);

            personService.ValidateActorData(new ViewTmdbSinglePersonModel
            {
                TmdbId = 1,
                Name = "test"
            });

            Assert.Single(data.Actors);
        }

        [Fact]
        public void ValidateActorDataShouldChangeActorData()
        {
            using var data = DatabaseMock.Instance;
            data.Actors.Add(new Actor
            {
                TmdbId = 1,
                Name = "test"
            });
            data.SaveChanges();
            var personService = new PersonService(data, MapperMock.Instance);

            personService.ValidateActorData(new ViewTmdbSinglePersonModel
            {
                TmdbId = 1,
                Name = "Test Test",
                Biography = "test",
                Photo = "test",
                Gender = 1,
                DateOfBirth = "test",
                DeathDay = "test",
                PlaceOfBirth = "test"
            });

            var actor = data.Actors.FirstOrDefault();
            Assert.True(actor.TmdbId == 1);
            Assert.True(actor.Name == "Test Test");
            Assert.True(actor.Biography == "test");
            Assert.True(actor.Photo == "test");
            Assert.True(actor.Gender == 1);
            Assert.True(actor.DateOfBirth == "test");
            Assert.True(actor.DeathDay == "test");
            Assert.True(actor.PlaceOfBirth == "test");
        }
    }
}
