namespace RentAMovie.Services.Person
{
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;

    public class PersonService : IPersonService
    {
        private readonly ViewMoviesDbContext data;
        public PersonService(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        public void ValidateActorData(ViewTmdbSinglePersonModel person)
        {
            var personOtCheck = data.Actors.FirstOrDefault(a => a.TmdbId == person.TmdbId);
            if (personOtCheck == null)
            {
                data.Actors.Add(new Actor()
                {
                    Name = person.Name,
                    Biography = person.Biography,
                    Photo = person.Photo,
                    Gender = person.Gender,
                    DateOfBirth = person.DateOfBirth,
                    DeathDay = person.DeathDay,
                    PlaceOfBirth = person.PlaceOfBirth,
                    TmdbId = person.TmdbId
                });
            }
            else
            {
                if (personOtCheck.Name != person.Name)
                {
                    personOtCheck.Name = person.Name;
                }
                if (personOtCheck.Biography != person.Biography)
                {
                    personOtCheck.Biography = person.Biography;
                }
                if (personOtCheck.Photo != person.Photo)
                {
                    personOtCheck.Photo = person.Photo;
                }
                if (personOtCheck.Gender != person.Gender)
                {
                    personOtCheck.Gender = person.Gender;
                }
                if (personOtCheck.DateOfBirth != person.DateOfBirth)
                {
                    personOtCheck.DateOfBirth = person.DateOfBirth;
                }
                if (personOtCheck.DeathDay != person.DeathDay)
                {
                    personOtCheck.DeathDay = person.DeathDay;
                }
                if (personOtCheck.PlaceOfBirth != person.PlaceOfBirth)
                {
                    personOtCheck.PlaceOfBirth = person.PlaceOfBirth;
                }
            }
            data.SaveChanges();
        }

        public void ValidateDirectorData(ViewTmdbSinglePersonModel person)
        {
            var personOtCheck = data.Directors.FirstOrDefault(a => a.TmdbId == person.TmdbId);
            if (personOtCheck == null)
            {
                data.Directors.Add(new Director()
                {
                    Name = person.Name,
                    Biography = person.Biography,
                    Photo = person.Photo,
                    Gender = person.Gender,
                    DateOfBirth = person.DateOfBirth,
                    DeathDay = person.DeathDay,
                    PlaceOfBirth = person.PlaceOfBirth,
                    TmdbId = person.TmdbId
                });
            }
            else
            {
                if (personOtCheck.Name != person.Name)
                {
                    personOtCheck.Name = person.Name;
                }
                if (personOtCheck.Biography != person.Biography)
                {
                    personOtCheck.Biography = person.Biography;
                }
                if (personOtCheck.Photo != person.Photo)
                {
                    personOtCheck.Photo = person.Photo;
                }
                if (personOtCheck.Gender != person.Gender)
                {
                    personOtCheck.Gender = person.Gender;
                }
                if (personOtCheck.DateOfBirth != person.DateOfBirth)
                {
                    personOtCheck.DateOfBirth = person.DateOfBirth;
                }
                if (personOtCheck.DeathDay != person.DeathDay)
                {
                    personOtCheck.DeathDay = person.DeathDay;
                }
                if (personOtCheck.PlaceOfBirth != person.PlaceOfBirth)
                {
                    personOtCheck.PlaceOfBirth = person.PlaceOfBirth;
                }
            }
            data.SaveChanges();
        }
    }
}
