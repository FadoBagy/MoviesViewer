namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<Review> Comments { get; set; } = new List<Review>();
    }
}
