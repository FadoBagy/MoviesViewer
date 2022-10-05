namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Models;
    using RentAMovie.Models.MovieModuls;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
            var baseUrl = "https://api.themoviedb.org/3";
            var mostPopularRequest = baseUrl + "/discover/movie?sort_by=popularity.desc&" + apiKey;
            var mostPopular21Request = "http://api.themoviedb.org/3/discover/movie?primary_release_date.gte=2021-01-01&primary_release_date.lte=2021-09-31&api_key=827b5d3636ed4d470d182016543dc5cf";
            var topRatedActionsRequest = "http://api.themoviedb.org/3/discover/movie?with_genres=28&sort_by=vote_average.desc&vote_count.gte=500&api_key=827b5d3636ed4d470d182016543dc5cf";

            var movies = new List<PopularMovieResultModule>();
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
                        movies.Add(movie);
                    }
                }
            }

            var moviesTop5 = movies.Take(5);
            return View(moviesTop5);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}