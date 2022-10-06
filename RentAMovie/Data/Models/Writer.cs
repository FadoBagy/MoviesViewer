namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Writer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(PersonBiographyMaxLength)]
        public string? Biography { get; set; }

        public string? Photo { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DeathDay { get; set; }

        public string? PlaceOfBirth { get; set; }

        public ICollection<Movie> WrittenMovies { get; set; } = new List<Movie>();
    }
}
