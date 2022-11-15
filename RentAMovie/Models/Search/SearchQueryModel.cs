namespace RentAMovie.Models.Search
{
    using RentAMovie.Models.Genre;
    using RentAMovie.Models.MovieModuls;

    public class SearchQueryModel
    {
        public const int MoviesPerPage = 12;
        public int CurrentPage { get; set; } = 1;

        public string SearchTerm { get; set; }

        public int? SortBy { get; set; }

        public int? Genre { get; set; }

        public int? FilterBy { get; set; }

        public List<SearchMovieModel> Movies { get; set; } = new List<SearchMovieModel>();

        public ICollection<ViewGenreModel> AvailableGenres { get; set; }
    }
}
