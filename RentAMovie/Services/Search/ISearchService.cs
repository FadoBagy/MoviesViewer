namespace RentAMovie.Services.Search
{
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Genre;

    public interface ISearchService
    {
        List<ViewGenreModel> GetAllGenres();

        IQueryable<Movie> ValidateSearchQuery(SearchServiceModel query);
    }
}
