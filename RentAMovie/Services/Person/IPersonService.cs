namespace RentAMovie.Services.Person
{
    using RentAMovie.Models.PersonModels;

    public interface IPersonService
    {
        void ValidatePersonData(ViewTmdbSingleActorModel person);
    }
}
