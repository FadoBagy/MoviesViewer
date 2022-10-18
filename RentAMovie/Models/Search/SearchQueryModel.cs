namespace RentAMovie.Models.Search
{
    using RentAMovie.Models.MovieModuls;

    public class SearchQueryModel
    {
        public const int MoviesPerPage = 12;

        public int CurrentPage { get; set; } = 1;

        public string SearchTerm { get; set; }

        public List<SearchMovieModel> Movies { get; set; } = new List<SearchMovieModel>();
    }
}
