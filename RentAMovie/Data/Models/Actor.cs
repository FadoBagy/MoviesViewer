namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Actor
    {
        [Key]
        public int Id { get; set; }

        public int? TmdbId { get; set; }

        [Required]
        [MaxLength(ActorNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PersonBiographyMaxLength)]
        public string? Biography { get; set; }

        public string? Photo { get; set; }

        [Range(PersonGenderMinLength, PersonGenderMaxLength)]
        public int? Gender { get; set; }

        public string? DateOfBirth { get; set; }

        public string? DeathDay { get; set; }

        public string? PlaceOfBirth { get; set; }

        public ICollection<Movie> PlayedInMovies { get; set; } = new List<Movie>();
    }
}
