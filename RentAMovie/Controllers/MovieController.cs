namespace RentAMovie.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Data.Models;
    using RentAMovie.Models;

    public class MovieController : Controller
    {
        [ActionName("List")]
        [Route("/Movie/List")]
        public IActionResult List()
        {
            var apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
            var baseUrl = "https://api.themoviedb.org/3";
            var mostPopularRequest = baseUrl + "/discover/movie?sort_by=popularity.desc&" + apiKey;

            var movies = new List<Movie2Module>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(mostPopularRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieDto = JsonConvert.DeserializeObject<MovieGetModule>(json);
                if (movieDto != null && movieDto.Results != null)
                {
                    foreach (var movie in movieDto.Results)
                    {
                        movies.Add(movie);
                    }
                }
            }

            //var reviews = new List<Movie>()
            //{
            //    new Movie{
            //        Id = 1,
            //        Title = "The Dark Knight",
            //        Description = "Story about Bruce Wayne as Batman fighting for justice.",
            //        DateCreated = DateTime.Now},
            //    new Movie{
            //        Id = 2,
            //        Title = "Joker",
            //        Description = "Where normal meets subnormal.",
            //        DateCreated = DateTime.Now}
            //};

            return View(movies);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(MovieFormModule model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Ok();
        }

        public IActionResult SpecificMovie(int id) 
        {
            return Ok(id);
        }
    }
}
