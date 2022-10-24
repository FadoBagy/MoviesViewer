namespace RentAMovie.Models.MovieModuls
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Movie;

    public class AddMovieFormModule
    {
        [StringLength(MaxMovieTitle, 
            MinimumLength = MinMovieTitle,
            ErrorMessage = "{0} length must be between {2} and {1}.")]
        [Required(ErrorMessage = "Required field")]
        public string Title { get; init; }

        [StringLength(MaxMovieDescription,
            MinimumLength = MinMovieDescription, 
            ErrorMessage = "{0} length must be between {2} and {1}.")]
        [Required(ErrorMessage = "Required field")]
        public string Description { get; init; }

        public string? Tagline { get; set; }

        [Range(typeof(int), MinMovieRuntime, MaxMovieRuntime,
            ErrorMessage = "{0} length must be between {1} and {2}.",
            ConvertValueInInvariantCulture = true)]
        public int? Runtime { get; init; }

        public int? Revenue { get; init; }

        public int? Budget { get; init; }

        [BindProperty, DataType(DataType.Date)]
        [Display(Name = "Date Published")]
        public DateTime? DatePublished { get; init; }

        [Url(ErrorMessage = "Invalid URL")]
        [Display(Name = "Poster URL")]
        public string? Poster { get; init; }

        [Url(ErrorMessage = "Invalid URL")]
        [Display(Name = "Backdrop URL")]
        public string? Backdrop { get; init; }

        [Url(ErrorMessage = "Invalid URL")]
        [Display(Name = "Trailer URL")]
        public string? Trailer { get; init; }

        //[Display(Name = "Content Ranting")]
        //public string ContentRanting { get; init; }
        //public IEnumerable<Genre> Genres { get; set; }
    }
}
