namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Review;

    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxReviewContent)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
