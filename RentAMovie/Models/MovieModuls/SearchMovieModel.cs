namespace RentAMovie.Models.MovieModuls
{
    public class SearchMovieModel
	{
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? PosterPath { get; set; }

        public string? Rating { get; set; }

        public int? TmdbId { get; set; }

        public int? VoteCount { get; set; }
    }
}
