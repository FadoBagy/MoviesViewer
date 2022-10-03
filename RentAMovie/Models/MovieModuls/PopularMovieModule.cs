namespace RentAMovie.Models.MovieModuls
{
    public class PopularMovieModule
    {
        public int Page { get; set; }

        public IEnumerable<PopularMovieResultModule>? Results { get; set; }
    }
}
