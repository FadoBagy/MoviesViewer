namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Infrastructure;
    using RentAMovie.Models.Home;
    using RentAMovie.Services.Home;
    using System.Security.Claims;

    public class HomeController : Controller
    {
        private readonly IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View(new ViewIndexModel()
            {
                TopActionMovies = TmdbApiCalls.TopActionMoviesRequest().Take(5),
                LatestMovies = service.GetLatestMovies(),
                WatchlistMovies = service.GetWatchlistMovies(GetCurrentUserId()),
                NowPlayingMovies = TmdbApiCalls.NowPlayingMoviesRequest().Take(20),
                TrendingMovies = TmdbApiCalls.TrendingMoviesRequest().Take(20),
                TotalMovies = service.GetMovieCount(),
                TotalUsers = service.GetUserCount(),
                TotalReviews = service.GetReviewCount()
            });
        }

        public IActionResult Source()
        {
            return View();
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }

        private string GetCurrentUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}