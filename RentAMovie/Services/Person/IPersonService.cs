namespace RentAMovie.Services.Person
{
    using RentAMovie.Models.PersonModels;

    public interface IPersonService
    {
        void ValidateActorData(ViewTmdbSinglePersonModel person);

        void ValidateDirectorData(ViewTmdbSinglePersonModel person);
    }
}
