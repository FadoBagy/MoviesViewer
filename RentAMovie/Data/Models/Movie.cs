namespace RentAMovie.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreated  { get; set; }

        [Required]
        public string Description { get; set; }

        //public  Length { get; set; }

        //public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        //public int Rating { get; set; }

        //public int ContentRanting  { get; set; }

        //public int Poster { get; set; }

        //public int Images { get; set; }

        //public int Trailer { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Director> Directors { get; set; } = new List<Director>();

        public ICollection<Writer> Writers { get; set; } = new List<Writer>();

        public ICollection<Actor> Actors  { get; set; } = new List<Actor>();

        public bool IsBeingRented { get; set; }

        // Owner?
    }
}
