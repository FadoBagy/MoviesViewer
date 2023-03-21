namespace RentAMovie.Models.MovieModuls
{
    using Newtonsoft.Json;

    public class RecommendationsMovieResultModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("vote_average")]
        public string Rating { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("id")]
        public int TmdbId { get; set; }
    }
}
