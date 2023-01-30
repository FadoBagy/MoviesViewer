namespace RentAMovie.Areas.Admin.Models.Movie
{
	public class CardMovieModel
	{
		public int? Id { get; set; }

		public int? TmdbId { get; set; }

		public string? Title { get; set; }

		public string? Poster { get; set; }

        public string? Rating { get; set; }

        public DateTime? DatePublished { get; set; }
	}
}
