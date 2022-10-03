namespace RentAMovie.Models.MovieModuls
{
    using System.ComponentModel.DataAnnotations;

    public class MovieFormModule
    {
        [MinLength(2)]
        [MaxLength(100)]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Required(ErrorMessage = "Required field")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime DateCreated { get; set; }

        [MinLength(3)]
        [MaxLength(500)]
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Required(ErrorMessage = "Required field")]
        public string Description { get; set; }
    }
}
