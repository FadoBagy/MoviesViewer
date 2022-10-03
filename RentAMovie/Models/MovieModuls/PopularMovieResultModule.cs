namespace RentAMovie.Models.MovieModuls
{
    using Newtonsoft.Json;

    public class PopularMovieResultModule
    {
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string? Description { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }
    }
}
