namespace RentAMovie.Services.Person
{
    using RentAMovie.Models.PersonModels;

    public interface IPersonService
    {
        public void ValidatePersonData(ViewTmdbSingleActorModel person);
    }
}
