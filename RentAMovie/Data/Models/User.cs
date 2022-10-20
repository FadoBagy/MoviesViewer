namespace RentAMovie.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string? Photo { get; set; }

        public ICollection<Movie> UploadedMovies { get; set; } = new List<Movie>();

        public ICollection<Review> WrittenReviews { get; set; } = new List<Review>();

        //public ICollection<Movie> WatchList { get; set; } = new List<Movie>();
    }
}