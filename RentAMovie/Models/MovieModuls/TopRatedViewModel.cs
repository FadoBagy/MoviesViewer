namespace RentAMovie.Models.MovieModuls
{
	public class TopRatedViewModel
	{
		public List<PopularMovieResultModule> Movies { get; set; }

		public int CurrentPage { get; set; } = 1;
	}
}
