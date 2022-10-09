namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
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
        [Route("/Movies")]
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
                        // TODO: Change the validation to the TMDB ID
                        if (!data.Movies.Any(m => m.Title == movie.Title))
                        {
                            var newMovie = new Movie
                            {
                                Title = movie.Title,
                                Description = movie.Description,
                                DatePublished = movie.ReleaseDate,
                                Poster = movie.PosterPath
                            };

                            data.Movies.Add(newMovie);
                            data.SaveChanges();
                        }
                        // If alredy in the db update Rating (and other data?)
                        movies.Add(movie);
                    }
                }
            }
           

            var moviesTop5 = movies.Take(5);
            return View(movies);
        }

        //[HttpPost]
        //[Route("/Movies")]
        //public IActionResult List(string searchTerm)
        //{
        //    if (searchTerm is null)
        //    {
        //        throw new ArgumentNullException(nameof(searchTerm));
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

            var newMovie = new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                Tagline = movie.Tagline,
                Runtime = movie.Runtime,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                DatePublished = movie.DatePublished,
                Poster = movie.Poster,
                Trailer = movie.Trailer
            };

            data.Movies.Add(newMovie);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SpecificMovie(int id) 
        {
            return Ok(id);
        }
    }
}
