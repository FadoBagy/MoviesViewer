namespace RentAMovie.Data.Models
{
    using RentAMovie.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants;

    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MovieDescriptionMaxLength)]
        public string? Description { get; set; }

        [Range(MovieMinRuntime, MovieMaxRuntime)]
        public int? Runtime { get; set; }

        [Range(MovieMinRating, MovieMaxRating)]
        public int Rating { get; set; }

        public int? Revenue { get; set; }

        public int? Budget { get; set; }

        public string? ContentRanting { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DatePublished { get; set; }

        public string? Poster { get; set; }

        public string? Trailer { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Director> Directors { get; set; } = new List<Director>();

        public ICollection<Writer> Writers { get; set; } = new List<Writer>();

        public ICollection<Actor> Actors { get; set; } = new List<Actor>();

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public User? User { get; set; }

        //public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        //public ICollection<string> Images { get; set; } = new List<string>();
    }
}