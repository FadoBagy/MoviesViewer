namespace RentAMovie.Models
{
    public class MovieGetModule
    {
        public int Page { get; set; }

        public IEnumerable<Movie2Module>? Results { get; set; }
    }
}
