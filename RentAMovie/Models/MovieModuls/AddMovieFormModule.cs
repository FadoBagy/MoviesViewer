namespace RentAMovie.Models.MovieModuls
{
    using RentAMovie.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AddMovieFormModule
    {
        //[MinLength(2)]
        //[MaxLength(100)]
        //[StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        //[Required(ErrorMessage = "Required field")]
        //public string Title { get; set; }

        //[Required(ErrorMessage = "Required field")]
        //public DateTime DateCreated { get; set; }

        //[MinLength(3)]
        //[MaxLength(500)]
        //[StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        //[Required(ErrorMessage = "Required field")]
        //public string Description { get; set; }


        public string Title { get; init; }

        public string Description { get; init; }

        public int? Runtime { get; init; }

        public int? Revenue { get; init; }

        public int? Budget { get; init; }

        public string ContentRanting { get; init; }

        public DateTime? DatePublished { get; init; }

        public string? Poster { get; init; }

        public string? Trailer { get; init; }

        public int UserId { get; init; }
    }
}
