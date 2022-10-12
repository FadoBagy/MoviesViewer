﻿namespace RentAMovie.Models.MovieModuls
{
    using Newtonsoft.Json;

    public class TmdbSingleMovieModel
    {
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("vote_average")]
        public string Rating { get; set; }

        [JsonProperty("id")]
        public int TmdbId { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("backdrop_path")]
        public string? BackdropPath { get; set; }

        public int? Budget { get; set; }

        public int? Revenue { get; set; }

        public int? Runtime { get; set; }

        public string? Tagline { get; set; }
    }
}
