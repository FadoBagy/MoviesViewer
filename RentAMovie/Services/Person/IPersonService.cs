namespace RentAMovie.Services.Person
{
    using RentAMovie.Models.PersonModels;
    using Data.Models;

    public interface IPersonService
    {
        void ValidateActorData(ViewTmdbSinglePersonModel person);

        void ValidateDirectorData(ViewTmdbSinglePersonModel person);

        Movie GetMovieTmdb(int id);

        Actor GetActorWithMovies(int id);

        Director GetDirectorWithMovies(int id);
    }
}
