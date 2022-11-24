namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Person;

    public class Writer
    {
        [Key]
        public int Id { get; set; }

        public int? TmdbId { get; set; }

        [Required]
        [StringLength(MaxPersonName,
            MinimumLength = MinPersonName)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxPersonBiography,
            MinimumLength = MinPersonBiography)]
        public string? Biography { get; set; }

        public string? Photo { get; set; }

        [Range(typeof(int), MinPersonGender, MaxPersonGender,
            ConvertValueInInvariantCulture = true)]
        public int? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DeathDay { get; set; }

        public string? PlaceOfBirth { get; set; }

        public ICollection<Movie> WrittenMovies { get; set; } 
            = new List<Movie>();
    }
}
