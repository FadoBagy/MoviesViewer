namespace RentAMovie.Models.PersonModels
{
    using RentAMovie.Data.Models;

    public class ViewAllCastModel
    {
        public Movie? Movie { get; set; }

        public List<ProductionTeamCastModel>? Actors { get; set; }
    }
}
