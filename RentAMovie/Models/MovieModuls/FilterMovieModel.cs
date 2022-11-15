namespace RentAMovie.Models.MovieModuls
{
    using RentAMovie.Models.Genre;

    public class FilterMovieModel
    {
        public int SortBy { get; set; }

        public int Genre { get; set; }

        public int FilterBy { get; set; }

        public ICollection<SearchMovieModel> Movies { get; set; } = new List<SearchMovieModel>();

        public ICollection<ViewGenreModel> AvailableGenres { get; set; }
    }
}
