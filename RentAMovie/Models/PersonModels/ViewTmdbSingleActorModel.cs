namespace RentAMovie.Models.PersonModels
{
    using Newtonsoft.Json;
    using RentAMovie.Data.Models;

    public class ViewTmdbSingleActorModel
    {
        public string? Biography { get; set; }

        [JsonProperty("birthday")]
        public string DateOfBirth { get; set; }

        [JsonProperty("deathday")]
        public string? DeathDay { get; set; }

        public int? Gender { get; set; }

        [JsonProperty("id")]
        public int TmdbId { get; set; }

        public string Name { get; set; }

        [JsonProperty("place_of_birth")]
        public string? PlaceOfBirth { get; set; }

        [JsonProperty("profile_path")]
        public string? Photo { get; set; }

        public Movie CurrentMovie { get; set; }
    }
}
