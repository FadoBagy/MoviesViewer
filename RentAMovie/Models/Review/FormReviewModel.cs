namespace RentAMovie.Models.Review
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Review;

    public class FormReviewModel
    {
        [StringLength(MaxReviewContent,
            MinimumLength = MinReviewContent,
            ErrorMessage = "{0} length must be between {2} and {1}.")]
        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Review")]
        public string Content { get; init; }

        [Display(Name = "This review contains spoilers")]
        public bool IsSpoiler { get; init; }

        public int MovieId { get; set; }
    }
}
