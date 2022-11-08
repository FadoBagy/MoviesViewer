namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Review;
    using System.Security.Claims;

    public class ReviewController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ViewMoviesDbContext data;
        public ReviewController(ViewMoviesDbContext data, UserManager<User> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create(int movieId)
        {
            return View(new FormReviewModel
            {
                MovieId = movieId,
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(FormReviewModel review)
        {
            if (!ModelState.IsValid)
            {
                return View(new FormReviewModel
                {
                    Content = review.Content,
                    MovieId = review.MovieId
                });
            }
            var movie = data.Movies.Find(review.MovieId);

            var newReview = new Review
            {
                Content = review.Content,
                UserId = GetCurrentUserId(),
                MovieId = review.MovieId
            };

            movie.Reviews.Add(newReview);
            data.Reviews.Add(newReview);
            data.SaveChanges();

            return RedirectToAction("All", "Review", new { movieId = review.MovieId }); 
        }

        // TODO: Error handling for comments - no reviews (home)
        public IActionResult All(int movieId)
        {
            var movie = data.Movies.Find(movieId);
            var reviews = data.Reviews
                .Where(r => r.MovieId == movieId)
                .OrderByDescending(r => r.CreationDate)
                .Select(r => new ViewReviewModel
                {
                    Id = r.MovieId,
                    Content = r.Content,
                    CreationDate = r.CreationDate,
                    MovieInfo = new Movie
                    {
                        Title = movie.Title,
                        Poster = movie.Poster,
                        BackdropPath = movie.BackdropPath,
                        DateCreated = movie.DateCreated
                    },
                    Comments = r.Comments,
                    UserId = r.UserId
                })
                .ToList();

            return View(reviews);
        }

        //[Route("/Movies/Reviews/{movieId}")]
        //public IActionResult MovieReviews(int movieId)
        //{
        //}

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
