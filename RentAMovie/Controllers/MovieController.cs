namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis;
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

        [ActionName("Popular")]
        [Route("/Movies/Popular")]
        public IActionResult Popular()
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
                        ValidateMovieApiRequest(movie);
                        movies.Add(movie);
                    }
                    data.SaveChanges();
                }
            }

            return View(movies);
        }

        [Route("/Movies/TopRated")]
        public IActionResult TopRated()
        {
            var topRatedRequest = baseUrl + "/discover/movie?sort_by=vote_average.desc&vote_count.gte=9200&" + apiKey;

            var movies = new List<PopularMovieResultModule>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(topRatedRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
                if (movieDto != null && movieDto.Results != null)
                {
                    foreach (var movie in movieDto.Results)
                    {
                        ValidateMovieApiRequest(movie);
                        movies.Add(movie);
                    }
                    data.SaveChanges();
                }
            }

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

        [Route("/Movie/{id}-tmdb")]
        public IActionResult MovieTmdb(int id) 
        {
            var movieDataRequest = baseUrl + $"/movie/{id}?" + apiKey;

            var movie = new TmdbSingleMovieModel();
            var movieToCheck = data.Movies.FirstOrDefault(m => m.TmdbId == id);
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(movieDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieData = JsonConvert.DeserializeObject<TmdbSingleMovieModel>(json);
                if (movieToCheck == null)
                {
                    try
                    {
                        var newTmdbMovie = new Movie
                        {
                            Title = movieData.Title,
                            Description = movieData.Description,
                            DatePublished = movieData.ReleaseDate,
                            Poster = movieData.PosterPath,
                            Rating = float.Parse(movieData.Rating),
                            TmdbId = movieData.TmdbId,
                            VoteCount = movieData.VoteCount,
                            BackdropPath = movieData.BackdropPath,
                            Budget = movieData.Budget,
                            Revenue = movieData.Revenue,
                            Runtime = movieData.Runtime,
                            Tagline = movieData.Tagline
                        };
                        data.Movies.Add(newTmdbMovie);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    if (movieToCheck.DatePublished != movieData.ReleaseDate)
                    {
                        movieToCheck.DatePublished = movieData.ReleaseDate;
                    }
                    if (movieToCheck.Poster != movieData.PosterPath)
                    {
                        movieToCheck.Poster = movieData.PosterPath;
                    }
                    if (movieToCheck.Rating != float.Parse(movieData.Rating))
                    {
                        movieToCheck.Rating = float.Parse(movieData.Rating);
                    }
                    if (movieToCheck.VoteCount != movieData.VoteCount)
                    {
                        movieToCheck.VoteCount = movieData.VoteCount;
                    }
                    if (movieToCheck.BackdropPath != movieData.BackdropPath)
                    {
                        movieToCheck.BackdropPath = movieData.BackdropPath;
                    }
                    if (movieToCheck.Budget != movieData.Budget)
                    {
                        movieToCheck.Budget = movieData.Budget;
                    }
                    if (movieToCheck.Revenue != movieData.Revenue)
                    {
                        movieToCheck.Revenue = movieData.Revenue;
                    }
                    if (movieToCheck.Runtime != movieData.Runtime)
                    {
                        movieToCheck.Runtime = movieData.Runtime;
                    }
                    if (movieToCheck.Tagline != movieData.Tagline)
                    {
                        movieToCheck.Tagline = movieData.Tagline;
                    }
                }
                data.SaveChanges();
                movie = GetSingleMovieData(movieData);
            }

            return View(movie);
        }

        private TmdbSingleMovieModel GetSingleMovieData(TmdbSingleMovieModel movie)
        {
            return new TmdbSingleMovieModel()
            {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                PosterPath = movie.PosterPath,
                Rating = movie.Rating,
                TmdbId = movie.TmdbId,
                VoteCount = movie.VoteCount,
                BackdropPath = movie.BackdropPath,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                Runtime = movie.Runtime,
                Tagline = movie.Tagline
            };
        }

        private void ValidateMovieApiRequest(PopularMovieResultModule movie)
        {
            var movieToCheck = data.Movies.FirstOrDefault(m => m.TmdbId == movie.TmdbId);
            if (movieToCheck == null)
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
                if (movieToCheck.DatePublished != movie.ReleaseDate)
                {
                    movieToCheck.DatePublished = movie.ReleaseDate;
                }
                if (movieToCheck.Poster != movie.PosterPath)
                {
                    movieToCheck.Poster = movie.PosterPath;
                }
                if (movieToCheck.Rating != float.Parse(movie.Rating))
                {
                    movieToCheck.Rating = float.Parse(movie.Rating);
                }
                if (movieToCheck.VoteCount != movie.VoteCount)
                {
                    movieToCheck.VoteCount = movie.VoteCount;
                }
            }
        }
    }
}