namespace RentAMovie.Models.MovieModuls
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddMovieFormModule
    {
        [StringLength(MovieTitleMaxLength, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = MovieTitleMinLength)]
        [Required(ErrorMessage = "Required field")]
        public string Title { get; init; }

        [StringLength(MovieDescriptionMaxLength, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = MovieDescriptionMinLength)]
        [Required(ErrorMessage = "Required field")]
        public string Description { get; init; }

        public string? Tagline { get; set; }

        [Range(MovieMinRuntime, MovieMaxRuntime)]
        public int? Runtime { get; init; }

        public int? Revenue { get; init; }

        public int? Budget { get; init; }

        [Display(Name = "Date Published")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? DatePublished { get; init; }

        public string? Poster { get; init; }

        public string? Trailer { get; init; }

        public int UserId { get; init; }

        //[Display(Name = "Content Ranting")]
        //public string ContentRanting { get; init; }
        //public IEnumerable<Genre> Genres { get; set; }
    }
}
