namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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

        [Route("/Reviews/{reviewId}")]
        public IActionResult SingleReview(int reviewId)
        {
            var review = data.Reviews.Find(reviewId);
            if (review?.UserId != GetCurrentUserId())
            {
                TempData["error"] = "Could not find!";
                return RedirectToAction("Index", "Home");
            }

            var movie = data.Movies.Find(review.MovieId);
            return View(new ViewReviewModel
            {
                Id = review.Id,
                Content = review.Content,
                CreationDate = review.CreationDate,
                MovieInfo = new Movie
                {
                    Title = movie.Title,
                    Poster = movie.Poster,
                    BackdropPath = movie.BackdropPath,
                    DatePublished = movie.DatePublished
                },
                UserId = review.UserId
            });
        }

        // TODO: Error handling for no revews for the given movie id (home)
        [Route("/Movies/{movieId}/Reviews")]
        public IActionResult All(int movieId)
        {
            var movie = data.Movies.Include(m => m.Reviews).FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
            {
                TempData["error"] = "Could not find movie!";
                return RedirectToAction("Index", "Home");
            }
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
                        DatePublished = movie.DatePublished
                    },
                    UserId = r.UserId
                })
                .ToList();

            if (reviews.Count() == 0)
            {
                return View(new List<ViewReviewModel>() {
                    new ViewReviewModel
                    {
                        MovieInfo = new Movie
                        {
                            Title = movie.Title,
                            Poster = movie.Poster,
                            BackdropPath = movie.BackdropPath,
                            DatePublished = movie.DatePublished
                        }
                    }
                });
            }

            return View(reviews);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int reviewId)
        {
            var review = data.Reviews.Find(reviewId);
            if (review?.UserId != GetCurrentUserId())
            {
                TempData["error"] = "You cannot delete this review!";
                return RedirectToAction("Index", "Home");
            }

            if (review != null)
            {
                data.Reviews.Remove(review);
                data.SaveChanges();
            }

            return RedirectToAction("All", "Review", new { movieId = review.MovieId });
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
