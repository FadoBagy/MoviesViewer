namespace RentAMovie.Models.MovieModuls
{
    public class TmdbMoviePageModel
    {
        public TmdbSingleMovieModel Movie { get; set; }

        public IEnumerable<RecommendationsMovieResultModel> RecommendedMovies { get; set; }
    }
}
