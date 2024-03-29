﻿namespace RentAMovie.Models.MovieModuls
{
    using Newtonsoft.Json;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;

    public class TmdbSingleMovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [JsonProperty("overview")]
        public string? Description { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("vote_average")]
        public string? Rating { get; set; }

        [JsonProperty("id")]
        public int TmdbId { get; set; }

        [JsonProperty("vote_count")]
        public int? VoteCount { get; set; }

        [JsonProperty("backdrop_path")]
        public string? BackdropPath { get; set; }

        public long? Budget { get; set; }

        public long? Revenue { get; set; }

        public int? Runtime { get; set; }

        public string? Tagline { get; set; }

        [JsonProperty("genres")]
        public ICollection<Genre>? Genres { get; set; }

        public ICollection<ProductionTeamCastModel>? Actors { get; set; }

        public ICollection<ProductionTeamCrewModel>? Crew { get; set; }

        public Review? Review { get; set; }

        public User? ReviewOwner { get; set; }

		public bool IsWatchlistedByUser { get; set; }

        public int ReviewCount { get; set; }
    }
}
