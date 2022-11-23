namespace RentAMovie.Models.PersonModels
{
    using Newtonsoft.Json;

    public class ProductionTeamCrewModel
    {
        public int Gender { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public string Photo { get; set; }

        public string Job { get; set; }
    }
}
