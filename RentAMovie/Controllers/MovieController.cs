namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis;
    using Newtonsoft.Json;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.PersonModels;
    using System.Linq;
    using System.Security.Claims;

    public class MovieController : Controller
    {
        private readonly string apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
        private readonly string baseUrl = "https://api.themoviedb.org/3";

        private readonly UserManager<User> userManager;
        private readonly ViewMoviesDbContext data;
        public MovieController(ViewMoviesDbContext data, UserManager<User> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(FormMovieModule movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            var newMovieGenres = movie.Genres.Split(", ", StringSplitOptions.TrimEntries);

            string genres = "";
            foreach (var genre in newMovieGenres)
            {
                genres += genre + ", ";
            }

            var newMovie = new Movie
            {
                Title = movie.Title.TrimEnd(),
                Description = movie.Description.TrimEnd(),
                Tagline = movie.Tagline?.TrimEnd(),
                Runtime = movie.Runtime,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                DatePublished = movie.DatePublished,
                Poster = movie.Poster,
                BackdropPath = movie.Backdrop,
                Trailer = movie.Trailer,
                UserId = GetCurrentUserId(),
                Genres = genres
            };

            data.Movies.Add(newMovie);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // Bugprone
        [Authorize]
        [Route("/Movies/MyMovies/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var movie = data.Movies.Find(id);

            if (movie?.UserId != GetCurrentUserId())
            {
                TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            return View(new FormMovieModule()
            {
                Title = movie.Title,
                Description = movie.Description,
                Tagline = movie.Tagline,
                Runtime = movie.Runtime,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                DatePublished = movie.DatePublished,
                Poster = movie.Poster,
                Backdrop = movie.BackdropPath,
                Trailer = movie.Trailer,
                Genres = movie.Genres
            });
        }

        [HttpPost]
        [Authorize]
        [Route("/Movies/MyMovies/Edit/{id}")]
        public IActionResult Edit(int id, FormMovieModule model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var movie = data.Movies.Find(id);

            if (movie?.UserId != GetCurrentUserId())
            {
                TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            movie.Title = model.Title.TrimEnd();
            movie.Description = model.Description?.TrimEnd();
            movie.Tagline = model.Tagline?.TrimEnd();
            movie.Runtime = model.Runtime;
            movie.Revenue = model.Revenue;
            movie.Budget = model.Budget;
            movie.DatePublished = model.DatePublished;
            movie.Poster = model.Poster;
            movie.BackdropPath = model.Backdrop;
            movie.Trailer = model.Trailer;
            if (movie.Genres != model.Genres)
            {
                var newMovieGenres = model.Genres.Split(", ", StringSplitOptions.TrimEntries);
                string genres = "";
                foreach (var genre in newMovieGenres)
                {
                    if (genre != "")
                    {
                        genres += genre + ", ";
                    }
                }
                movie.Genres = genres;
            }

            data.SaveChanges();

            return RedirectToAction("UserMovies", "Movie");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var movie = data.Movies.Find(id);
            if (movie?.UserId != GetCurrentUserId())
            {
                TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            if (movie != null)
            {
                data.Movies.Remove(movie);
                data.SaveChanges();
            }

            return RedirectToAction("UserMovies", "Movie");
        }

        [Authorize]
        [Route("/Movies/MyMovies")]
        public IActionResult UserMovies()
        {
            var userMovies = data.Movies
                .Where(m => m.UserId == GetCurrentUserId())
                .OrderByDescending(m => m.DateCreated)
                .Select(m => new ViewUserMovieCardModel
                {
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.DatePublished,
                    PosterPath = m.Poster,
                    Id = m.Id
                })
                .ToList();

            return View(userMovies);
        }

        [Route("/Movies/{movieId}")]
        public IActionResult MovieUser(int movieId)
        {
            var movie = data.Movies.Find(movieId);
            var lastReview = data.Reviews
                .Where(r => r.MovieId == movieId)
                .OrderByDescending(r => r.CreationDate)
                .FirstOrDefault();

            var movieModel = new UserSingleMovieModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Tagline = movie.Tagline,
                Runtime = movie.Runtime,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                DatePublished = movie.DatePublished,
                Poster = movie.Poster,
                BackdropPath = movie.BackdropPath,
                Trailer = movie.Trailer,
                Genres = movie.Genres,
                Review = lastReview
            };

            return View(movieModel);
        }

        [Route("/Movies/{id}-tmdb")]
        public IActionResult MovieTmdb(int id)
        {
            var movieDataRequest = baseUrl + $"/movie/{id}?" + apiKey;

            var movie = new TmdbSingleMovieModel();
            var movieToCheck = data.Movies
                .FirstOrDefault(m => m.TmdbId == id);
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(movieDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieData = JsonConvert.DeserializeObject<TmdbSingleMovieModel>(json);

                try
                {
                    ValidateSingleMovieData(movieToCheck, movieData);
                }
                catch (Exception)
                {
                    // Most likely invalid movie ID
                    return RedirectToAction("Error", "Home");
                }

                movie = GetSingleMovieData(movieData);

                data.SaveChanges();
            }

            if (movie.Actors != null)
            {
                foreach (var actor in movie.Actors)
                {
                    if (!data.Actors.Any(a => a.TmdbId == actor.Id))
                    {
                        var newActor = new Actor()
                        {
                            TmdbId = actor.Id,
                            Gender = actor.Gender,
                            Name = actor.Name,
                            Photo = actor.Photo
                        };

                        data.Movies.FirstOrDefault(m => m.TmdbId == id).Actors.Add(newActor);
                    }
                }
            }
            data.SaveChanges();

            return View(movie);
        }

        [ActionName("Popular")]
        [Route("/Movies/Popular")]
        public IActionResult Popular()
        {
            string mostPopularRequest = baseUrl + "/discover/movie?sort_by=popularity.desc&" + apiKey;
            string pages = "api.themoviedb.org/3/discover/movie?sort_by=popularity.desc&page=2&api_key=827b5d3636ed4d470d182016543dc5cf";

            var movies = new List<PopularMovieResultModule>();
            CollectMoviesData(mostPopularRequest, movies);

            return View(movies);
        }

        [Route("/Movies/TopRated")]
        public IActionResult TopRated()
        {
            string topRatedRequest = baseUrl + "/discover/movie?sort_by=vote_average.desc&vote_count.gte=9200&" + apiKey;
            var movies = new List<PopularMovieResultModule>();
            CollectMoviesData(topRatedRequest, movies);

            return View(movies);
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                Tagline = movie.Tagline,
                Actors = GetActorModels(movie.TmdbId),
                Genres = movie.Genres
            };
        }

        private void CollectMoviesData(string Url, List<PopularMovieResultModule> movies)
        {
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(Url);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
                if (movieDto != null && movieDto.Results != null)
                {
                    foreach (var movie in movieDto.Results)
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

                        movies.Add(movie);
                    }
                    data.SaveChanges();
                }
            }
        }

        private void ValidateSingleMovieData(Movie movieToCheck, TmdbSingleMovieModel movieData)
        {
            if (movieToCheck == null)
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
                    Tagline = movieData.Tagline,
                    //Genres = movieData?.Genres
                };
                data.Movies.Add(newTmdbMovie);
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
                //if (movieToCheck.Genres.Count != movieData.Genres?.Count)
                //{
                //    if (movieData.Genres != null)
                //    {
                //        movieToCheck.Genres.AddRange(movieData.Genres);
                //    }
                //}
            }
        }

        private ICollection<ProductionTeamCastModel> GetActorModels(int? movieId)
        {
            var teamDataRequest = baseUrl + $"/movie/{movieId}/casts?" + apiKey;

            var actorsModel = new List<ProductionTeamCastModel>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(teamDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var teamData = JsonConvert.DeserializeObject<ProductionTeamModel>(json);

                if (teamData != null && teamData.Cast != null)
                {
                    foreach (var member in teamData.Cast)
                    {
                        if (member.Role == "Acting")
                        {
                            var newActorModel = new ProductionTeamCastModel()
                            {
                                Gender = member.Gender,
                                Id = member.Id,
                                Role = member.Role,
                                Name = member.Name,
                                Photo = member.Photo,
                                Character = member.Character
                            };

                            actorsModel.Add(newActorModel);
                        }
                    }
                }
            }

            return actorsModel;
        }

        // Slow don't use
        private ICollection<Actor> GetActorsFromMovie(int movieId)
        {
            var allActorsDataRequest = baseUrl + $"/movie/{movieId}/casts?" + apiKey;

            var actorModels = new List<Actor>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(allActorsDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var teamData = JsonConvert.DeserializeObject<ProductionTeamModel>(json);

                int index = 0;
                if (teamData != null && teamData.Cast != null)
                {
                    foreach (var member in teamData.Cast)
                    {
                        var actorDataRequest = baseUrl + $"/person/{member.Id}?" + apiKey;

                        if (member.Role == "Acting")
                        {
                            var endpointSingle = new Uri(actorDataRequest);
                            var resultSingle = httpClient.GetAsync(endpointSingle).Result;
                            var jsonSingle = resultSingle.Content.ReadAsStringAsync().Result;

                            var actorData = JsonConvert.DeserializeObject<ViewTmdbSingleActorModel>(jsonSingle);

                            var newActor = new Actor()
                            {
                                Name = actorData.Name,
                                Biography = actorData.Biography,
                                Photo = actorData.Photo,
                                Gender = actorData.Gender,
                                DateOfBirth = null,
                                DeathDay = null,
                                PlaceOfBirth = actorData.PlaceOfBirth,
                                TmdbId = actorData.TmdbId
                            };

                            actorModels.Add(newActor);
                        }
                        index++;
                        if (index == 30)
                        {
                            break;
                        }
                    }
                }
            }

            return actorModels;
        }
    }
}