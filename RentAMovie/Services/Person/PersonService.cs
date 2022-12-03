namespace RentAMovie.Services.Person
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;

    public class PersonService : IPersonService
    {
        private readonly ViewMoviesDbContext data;
        private readonly IMapper mapper;
        public PersonService(ViewMoviesDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void ValidateActorData(ViewTmdbSinglePersonModel person)
        {
            var personOtCheck = data.Actors.FirstOrDefault(a => a.TmdbId == person.TmdbId);
            if (personOtCheck == null)
            {
                var actor = mapper.Map<Actor>(person);
                data.Actors.Add(actor);
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
                var director = mapper.Map<Director>(person);
                data.Directors.Add(director);
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

        public Movie GetMovieTmdb(int id)
        {
            return data.Movies
                .FirstOrDefault(m => m.TmdbId == id);
        }

        public Actor GetActorWithMovies(int id)
        {
            return data.Actors
                .Include(a => a.PlayedInMovies)
                .Where(a => a.TmdbId == id)
                .FirstOrDefault();
        }

        public Director GetDirectorWithMovies(int id)
        {
            return data.Directors
                .Include(d => d.DirectedMovies)
                .Where(d => d.TmdbId == id)
                .FirstOrDefault();
        }
    }
}
