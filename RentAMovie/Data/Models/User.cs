﻿namespace RentAMovie.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string? Photo { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public ICollection<Movie>? UploadedMovies { get; set; }
            = new List<Movie>();

        public ICollection<Review>? WrittenReviews { get; set; }
            = new List<Review>();

        public ICollection<UserMovie> WatchlistedMovies { get; set; }
            = new List<UserMovie>();

        public ICollection<UserMovieRating> RatedMovies { get; set; }
            = new List<UserMovieRating>();
    }
}