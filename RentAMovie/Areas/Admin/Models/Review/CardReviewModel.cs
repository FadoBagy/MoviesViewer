namespace RentAMovie.Areas.Admin.Models.Review
{
	using RentAMovie.Data.Models;

	public class CardReviewModel
	{
		public int Id { get; set; }

		public string Content { get; set; }

		public DateTime CreationDate { get; set; }

		public string UserId { get; set; }

		public string UserUsername { get; set; }

		public string UserPhoto { get; set; }

		public Movie MovieInfo { get; set; }
	}
}
