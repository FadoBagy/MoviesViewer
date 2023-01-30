namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Models.Home;
    using RentAMovie.Models.MovieModuls;
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
            var topRatedActionsRequest 
                = ControllerConstants.BaseUrl + "/discover/movie?with_genres=28&sort_by=vote_average.desc&vote_count.gte=500&" + ControllerConstants.ApiKey;
            var nowPlayingRequest 
                = ControllerConstants.BaseUrl + "/movie/now_playing?" + ControllerConstants.ApiKey;
            var trendingRequest 
                = ControllerConstants.BaseUrl + "/trending/movie/week?" + ControllerConstants.ApiKey;

            var topActionMovies = new List<PopularMovieResultModule>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(topRatedActionsRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
                if (movieDto != null && movieDto.Results != null)
                {
                    foreach (var movie in movieDto.Results)
                    {
                        topActionMovies.Add(movie);
                    }
                }
            }

            var nowPlayingMovies = new List<PopularMovieResultModule>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(nowPlayingRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
                if (movieDto != null && movieDto.Results != null)
                {
                    foreach (var movie in movieDto.Results)
                    {
                        nowPlayingMovies.Add(movie);
                    }
                }
            }

            var trendingMovies = new List<PopularMovieResultModule>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(trendingRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
                if (movieDto != null && movieDto.Results != null)
                {
                    foreach (var movie in movieDto.Results)
                    {
                        trendingMovies.Add(movie);
                    }
                }
            }

            return View(new ViewIndexModel()
            {
                TopActionMovies = topActionMovies.Take(5),
                LatestMovies = service.GetLatestMovies(),
                WatchlistMovies = service.GetWatchlistMovies(GetCurrentUserId()),
                NowPlayingMovies = nowPlayingMovies.Take(20),
                TrendingMovies = trendingMovies.Take(20),
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