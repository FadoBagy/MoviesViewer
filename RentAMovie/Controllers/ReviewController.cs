namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Review;
    using RentAMovie.Models.User;
    using RentAMovie.Services.Review;
    using System.Security.Claims;

    public class ReviewController : Controller
    {
        private readonly IReviewService service;
        public ReviewController(IReviewService service)
        {
            this.service = service;
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
            var movie = service.GetMovie(review.MovieId);

            var newReview = new Review
            {
                Content = review.Content,
                UserId = GetCurrentUserId(),
                MovieId = review.MovieId
            };

            movie.Reviews.Add(newReview);
            service.AddReview(newReview);

            return RedirectToAction("All", "Review", new { movieId = review.MovieId }); 
        }

        [Route("/Reviews/{reviewId}")]
        public IActionResult SingleReview(int reviewId)
        {
            var review = service.GetReview(reviewId);
            if (review == null)
            {
                TempData["error"] = "Could not find!";
                return RedirectToAction("Index", "Home");
            }

            var movie = service.GetMovie(review.MovieId);
            return View(new ViewReviewModel
            {
                Id = review.Id,
                Content = review.Content,
                CreationDate = review.CreationDate,
                MovieInfo = new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Poster = movie.Poster,
                    BackdropPath = movie.BackdropPath,
                    DatePublished = movie.DatePublished
                },
                UserId = review.UserId
            });
        }

        [Route("/Movies/{movieId}/Reviews")]
        public IActionResult All(int movieId)
        {
            var movie = service.GetMovieWithReviews(movieId);
            if (movie == null)
            {
                TempData["error"] = "Could not find movie!";
                return RedirectToAction("Index", "Home");
            }

            var reviews = service.GetReviews(movieId);
            if (reviews.Count() == 0)
            {
                ViewBag.MovieId = movie.Id;
                ViewBag.MovieTmdbId = movie.TmdbId;
                ViewBag.MovieTitle = movie.Title;
                ViewBag.MoviePoster = movie.Poster;
                ViewBag.MovieBackdropPath = movie.BackdropPath;
                ViewBag.MovieDatePublished = movie.DatePublished;

                return View();
            }

            return View(reviews);
        }

        [Authorize]
        [Route("/Reviews/MyReviews")]
        public IActionResult AllUserReview(int reviewedMovies)
        {
            string userId = GetCurrentUserId();
            var currentUser = service.GetCurrentUser(userId);

            List<ViewReviewModel> reviews = new List<ViewReviewModel>();
            if (reviewedMovies == 0)
            {
                reviews = service.GetUserReviews(userId);
            }
            else
            {
                reviews = service.GetUserReviewsForMovie(userId, reviewedMovies);
            }

            var userReviewedMovies = service.GetReviewedMovies(userId);

            return View(new QueryReviewModel
            {
                UserInfo = new ViewUserModel
                {
                    UserName = currentUser.UserName,
                    Photo = currentUser.Photo
                },
                ReviewedMovies = userReviewedMovies,
                Reviews = reviews
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int reviewId)
        {
            var review = service.GetReview(reviewId);
            if (review?.UserId != GetCurrentUserId())
            {
                TempData["error"] = "You cannot delete this review!";
                return RedirectToAction("Index", "Home");
            }

            if (review != null)
            {
                service.RemoveReview(review);
            }

            return RedirectToAction("All", "Review", new { movieId = review.MovieId });
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
