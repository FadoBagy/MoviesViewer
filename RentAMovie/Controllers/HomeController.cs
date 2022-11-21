namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Models;
    using RentAMovie.Models.Home;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Services.Home;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly string apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
        private readonly string baseUrl = "https://api.themoviedb.org/3";

        private readonly IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var topRatedActionsRequest = baseUrl + "/discover/movie?with_genres=28&sort_by=vote_average.desc&vote_count.gte=500&" + apiKey;
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
            return View(new ViewIndexModel()
            {
                TopActionMovies = moviesTop5,
                TotalMovies = service.GetMovieCount(),
                TotalUsers = service.GetUserCount(),
                TotalReviews = service.GetReviewCount()
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}