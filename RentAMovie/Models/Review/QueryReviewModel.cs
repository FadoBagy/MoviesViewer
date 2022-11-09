namespace RentAMovie.Models.Review
{
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.User;
    using System.ComponentModel.DataAnnotations;

    public class QueryReviewModel
    {

        public ViewUserModel UserInfo { get; set; }

        [Display(Name = "Select movie")]
        public ICollection<ViewMovieModel> ReviewedMovies { get; set; }

        public ICollection<ViewReviewModel> Reviews { get; set; }
    }
}
