namespace RentAMovie.Models
{
    using Newtonsoft.Json;

    public class Movie2Module
    {
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
    }
}
