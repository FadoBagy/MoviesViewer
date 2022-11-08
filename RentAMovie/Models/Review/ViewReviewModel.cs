namespace RentAMovie.Models.Review
{
    using Data.Models;
    public class ViewReviewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }

        public Movie MovieInfo { get; set; }
    }
}
