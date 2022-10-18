namespace RentAMovie.Models.Search
{
    using RentAMovie.Data.Models;

    public class SearchQueryModel
    {
        public string SearchTerm { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
