﻿namespace RentAMovie.Models.Review
{
    using RentAMovie.Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Review;

    public class FormReviewModel
    {
        [StringLength(MaxReviewContent,
            MinimumLength = MinReviewContent,
            ErrorMessage = "{0} length must be between {2} and {1}.")]
        [Required(ErrorMessage = "Required field")]
        public string Content { get; init; }

        public int MovieId { get; set; }
    }
}