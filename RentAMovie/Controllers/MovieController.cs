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
                        if (!data.Movies.Any(m => m.TmdbId == movie.TmdbId))
                        {
                            var newPopularMovie = new Movie
                            {
                                Title = movie.Title,
                                Description = movie.Description,
                                DatePublished = movie.ReleaseDate,
                                Poster = movie.PosterPath,
                                Rating = float.Parse(movie.Rating),
                                TmdbId = movie.TmdbId,
                                VoteCount = movie.VoteCount
                            };
                            data.Movies.Add(newPopularMovie);
                        }
                        else
                        {
                            var movieToCheck = data.Movies.FirstOrDefault(m => m.TmdbId == movie.TmdbId);
                            if (movieToCheck.DatePublished != movie.ReleaseDate)
                            {
                                movieToCheck.DatePublished = movie.ReleaseDate;
                            }
                            else if (movieToCheck.Poster != movie.PosterPath)
                            {
                                movieToCheck.Poster = movie.PosterPath;
                            }
                            else if (movieToCheck.Rating != float.Parse(movie.Rating))
                            {
                                movieToCheck.Rating = float.Parse(movie.Rating);
                            }
                            else if (movieToCheck.VoteCount != movie.VoteCount)
                            {
                                movieToCheck.VoteCount = movie.VoteCount;
                            }
                        }

                        movies.Add(movie);
                    }
                    data.SaveChanges();
                }
            }

            var moviesTop5 = movies.Take(5);
            return View(movies);
        }

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

        //[Route("/Movie/{id}-{tmdbId}")]
        public IActionResult MovieTmdb(int id) 
        {
            var movieDataRequest = baseUrl + $"/movie/{id}?" + apiKey;

            var movie = new TmdbSingleMovieModel();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(movieDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieData = JsonConvert.DeserializeObject<TmdbSingleMovieModel>(json);

                var newMovie = new TmdbSingleMovieModel()
                {
                    Title = movieData.Title,
                    Description = movieData.Description,
                    ReleaseDate = movieData.ReleaseDate,
                    PosterPath = movieData.PosterPath,
                    Rating = movieData.Rating,
                    TmdbId = movieData.TmdbId,
                    VoteCount = movieData.VoteCount,
                    BackdropPath = movieData.BackdropPath,
                    Budget = movieData.Budget,
                    Revenue = movieData.Revenue,
                    Runtime = movieData.Runtime,
                    Tagline = movieData.Tagline
                };

                movie = newMovie;
            }

            return View(movie);
        }
    }
}
