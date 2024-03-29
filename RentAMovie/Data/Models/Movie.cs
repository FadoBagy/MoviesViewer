﻿namespace RentAMovie.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Movie;

    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public int? TmdbId { get; set; }

        [Required]
        [StringLength(MaxMovieTitle,
            MinimumLength = MinMovieTitle)]
        public string Title { get; set; }

        [Required]
        [StringLength(MaxMovieDescription,
            MinimumLength = MinMovieDescription)]
        public string? Description { get; set; }

        [StringLength(MaxMovieTagline,
            MinimumLength = MinMovieTagline)]
        public string? Tagline { get; set; }

        [Range(typeof(int), MinMovieRuntime, MaxMovieRuntime,
            ConvertValueInInvariantCulture = true)]
        public int? Runtime { get; set; }

        [Range(typeof(float), MinMovieRating, MaxMovieRating,
            ConvertValueInInvariantCulture = true)]
        public float? Rating { get; set; }

        public int? VoteCount { get; set; }

        public long? Revenue { get; set; }

        public long? Budget { get; set; }

        public string? ContentRanting { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DatePublished { get; set; }

        public string? BackdropPath { get; set; }

        public string? Poster { get; set; }

        public string? Trailer { get; set; }

        public string? Genres { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Genre> GenresCollection { get; set; } 
            = new List<Genre>();

        public ICollection<Review> Reviews { get; set; } 
            = new List<Review>();

        public ICollection<Director> Directors { get; set; } 
            = new List<Director>();

        public ICollection<Writer> Writers { get; set; } 
            = new List<Writer>();

        public ICollection<Actor> Actors { get; set; } 
            = new List<Actor>();

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<UserMovie> WatchlistedByUsers { get; set; }
            = new List<UserMovie>();

        public ICollection<UserMovieRating> RatedByUsers { get; set; }
            = new List<UserMovieRating>();

        //public ICollection<string> Images { get; set; } = new List<string>();
    }
}