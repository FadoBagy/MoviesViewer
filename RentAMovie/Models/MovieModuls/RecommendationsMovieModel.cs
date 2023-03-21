namespace RentAMovie.Models.MovieModuls
{
    public class RecommendationsMovieModel
    {
        public int Page { get; set; }

        public IEnumerable<RecommendationsMovieResultModel>? Results { get; set; }
    }
}
