namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Models.Home;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Services.Home;

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

            return View(new ViewIndexModel()
            {
                TopActionMovies = movies.Take(5),
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
    }
}