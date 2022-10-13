namespace RentAMovie.Models.PersonModels
{
    public class ProductionTeamModel
    {
        public int Id { get; set; }

        public IEnumerable<ProductionTeamCastModel>? Cast { get; set; }

        public IEnumerable<ProductionTeamCrewModel>? Crew { get; set; }
    }
}
