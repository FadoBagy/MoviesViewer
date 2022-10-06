namespace RentAMovie.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(UserUsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(UserEmailMaxLength)]
        public string Email { get; set; }

        public int Password { get; set; }

        public string? Photo { get; set; }

        public ICollection<Movie> WatchList { get; set; } = new List<Movie>();

        public ICollection<Movie> UploadedMovies { get; set; } = new List<Movie>();

        public ICollection<Review> WrittenReviews { get; set; } = new List<Review>();
    }
}

//public ICollection<Movie> RentedMovies { get; set; } = new List<Movie>();