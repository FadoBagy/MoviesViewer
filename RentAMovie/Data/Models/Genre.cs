namespace RentAMovie.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Genre
	{
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
            = new List<Movie>();
    }
}
