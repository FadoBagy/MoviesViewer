namespace RentAMovie.Models.Home
{
    using RentAMovie.Models.MovieModuls;

    public class ViewIndexModel
    {
        public IEnumerable<PopularMovieResultModule> TopActionMovies { get; set; }

        public int TotalMovies { get; set; }

        public int TotalReviews { get; set; }

        public int TotalUsers { get; set; }
    }
}
