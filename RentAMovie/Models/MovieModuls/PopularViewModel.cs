namespace RentAMovie.Models.MovieModuls
{
	public class PopularViewModel
	{
		public List<PopularMovieResultModule> Movies { get; set; }

		public int CurrentPage { get; set; } = 1;
	}
}
