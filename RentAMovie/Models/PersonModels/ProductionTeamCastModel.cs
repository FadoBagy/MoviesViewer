namespace RentAMovie.Models.PersonModels
{
    using Newtonsoft.Json;

    public class ProductionTeamCastModel
    {
        public int Gender { get; set; }

        public int Id { get; set; }

        [JsonProperty("known_for_department")]
        public string Role { get; set; }

        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public string Photo { get; set; }

        public string Character { get; set; }
    }
}
