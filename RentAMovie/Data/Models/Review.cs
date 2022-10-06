namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants;

    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ReviewContentMaxLength)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Creator))]
        public int? CreatorId { get; set; }
        public User? Creator { get; set; }

        [ForeignKey(nameof(Movie))]
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }

        public ICollection<Review> Comments { get; set; } = new List<Review>();
    }
}
