namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Data;
    using RentAMovie.Models.MovieModuls;
    using System.Linq;

    public class MovieController : Controller
    {
        private readonly string apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
        private readonly string baseUrl = "https://api.themoviedb.org/3";

        private readonly ViewMoviesDbContext data;

        public MovieController(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        [ActionName("List")]
        [Route("/Movie/List")]
        public IActionResult List()
        {
            var mostPopularRequest = baseUrl + "/discover/movie?sort_by=popularity.desc&" + apiKey;

            var movies = new List<PopularMovieResultModule>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(mostPopularRequest);
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
            return View(movies);
        }

        //[HttpGet]
        //public IActionResult List(PopularMovieModule model)
        //{
        //    var query = this.Request.Query;
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    return Ok();
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddMovieFormModule movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            return Ok();
        }

        public IActionResult SpecificMovie(int id) 
        {
            return Ok(id);
        }
    }
}
