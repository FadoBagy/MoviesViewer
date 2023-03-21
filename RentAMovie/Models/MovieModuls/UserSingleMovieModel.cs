namespace RentAMovie.Models.MovieModuls
{
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;

    public class UserSingleMovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Tagline { get; set; }

        public int? Runtime { get; set; }

        public long? Revenue { get; set; }

        public long? Budget { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Poster { get; set; }

        public string? BackdropPath { get; set; }

        public string? Trailer { get; set; }

        public string? Genres { get; set; }

        public ICollection<ProductionTeamCastModel>? Actors { get; set; }

        public int? VoteCount { get; set; }

        public string? Rating { get; set; }

        public Review? Review { get; set; }

        public User? ReviewOwner { get; set; }

        public bool IsWatchlistedByUser { get; set; }

        public int ReviewCount { get; set; }

        public int UserRating { get; set; }

        public bool IsRatedByUser { get; set; }

        public float CurrentUserRating { get; set; }
    }
}
