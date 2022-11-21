namespace RentAMovie.Services.Search
{
    public class SearchServiceModel
    {
        public const int MoviesPerPage = 12;
        public int CurrentPage { get; set; } = 1;

        public string SearchTerm { get; set; }

        public int? SortBy { get; set; }

        public int? Genre { get; set; }
    }
}
