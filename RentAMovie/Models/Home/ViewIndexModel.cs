namespace RentAMovie.Models.Home
{
    using RentAMovie.Areas.Admin.Models.Movie;
    using RentAMovie.Models.MovieModuls;

    public class ViewIndexModel
    {
        public IEnumerable<PopularMovieResultModule> TopActionMovies { get; set; }

        public List<CardMovieModel> LatestMovies { get; set; }

        public List<CardMovieModel> WatchlistMovies { get; set; }

        public IEnumerable<PopularMovieResultModule> NowPlayingMovies { get; set; }

        public IEnumerable<PopularMovieResultModule> TrendingMovies { get; set; }

        public int TotalMovies { get; set; }

        public int TotalReviews { get; set; }

        public int TotalUsers { get; set; }
    }
}
