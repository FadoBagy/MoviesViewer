namespace RentAMovie.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public int Password { get; set; }

        //public  Photo { get; set; }

        public ICollection<Movie> UploadedMovies { get; set; } = new List<Movie>();

        public ICollection<Movie> RentedMovies { get; set; } = new List<Movie>();

        public ICollection<Review> WrittenReviews { get; set; } = new List<Review>();
    }
}
