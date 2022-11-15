namespace RentAMovie.Models.MovieModuls
{
    public class FilterMovieModel
    {
        public int SortBy { get; set; }
        public List<SearchMovieModel> Movies { get; set; } = new List<SearchMovieModel>();
    }
}
