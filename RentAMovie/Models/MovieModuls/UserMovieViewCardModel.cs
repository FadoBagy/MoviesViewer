namespace RentAMovie.Models.MovieModuls
{
	public class UserMovieViewCardModel
	{
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? PosterPath { get; set; }

        public string? Rating { get; set; }

        public int? VoteCount { get; set; }

        public int Id { get; set; }
    }
}
