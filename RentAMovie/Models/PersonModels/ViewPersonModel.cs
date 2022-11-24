namespace RentAMovie.Models.PersonModels
{
    using RentAMovie.Data.Models;

    public class ViewPersonModel
    {
        public ViewTmdbSinglePersonModel PersonData { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
