﻿namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.CodeAnalysis;
    using Newtonsoft.Json;
    using RentAMovie.Data.Models;
    using RentAMovie.Infrastructure;
    using RentAMovie.Models.MovieModuls;
    using RentAMovie.Models.PersonModels;
    using RentAMovie.Services.Movie;
    using RentAMovie.Services.Review;
    using System.Linq;
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;

    public class MovieController : Controller
    {
        private readonly IMovieService service;
        private readonly IReviewService reviewService;
        public MovieController(IMovieService service, IReviewService reviewService)
        {
            this.service = service;
            this.reviewService = reviewService;
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

            var genresCollection = new List<Genre>();
            foreach (var genre in newMovieGenres)
            {
                if (genre != null && genre != "")
                {
                    genresCollection.Add(service.GetGenreByName(genre));
                }
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
                UserId = GetCurrentUserId() ?? "1",
                Genres = genres,
                GenresCollection = genresCollection,
                IsPublic = false
            };

            service.AddMovie(newMovie);

			TempData["created"] = "Movie submission successful, awaiting approval!";
			return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("/Movies/MyMovies/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var movie = service.GetMovie(id);
            var userId = GetCurrentUserId() ?? "1";

            if (movie?.UserId != userId && !User.IsAdmin())
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

            var movie = service.GetMovieWithGenresCollection(id);
            var userId = GetCurrentUserId() ?? "1";
            if (movie?.UserId != userId && !User.IsAdmin())
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
            if (movie.GenresCollection.Count() > 0)
            {
                movie.GenresCollection.Clear();

                var newMovieGenres = model.Genres.Split(", ", StringSplitOptions.TrimEntries);
                var genresCollection = new List<Genre>();
                foreach (var genre in newMovieGenres)
                {
                    var currentGenre = service.GetGenreByName(genre);
                    if (currentGenre != null)
                    {
                        genresCollection.Add(currentGenre);
                    }
                }
                movie.GenresCollection = genresCollection;
            }
            else
            {
                var newMovieGenres = model.Genres.Split(", ", StringSplitOptions.TrimEntries);
                var genresCollection = new List<Genre>();
                foreach (var genre in newMovieGenres)
                {
                    if (genre != null && genre != "")
                    {
                        genresCollection.Add(service.GetGenreByName(genre));
                    }
                }
                movie.GenresCollection = genresCollection;
            }
            movie.IsPublic = false;

            service.SaveChanges();

			TempData["edit"] = "Movie information updated successfully, awaiting approval!";
			if (User.IsAdmin())
            {
				return RedirectToAction("Index", "Movie", new { area =  AdministratorAreaName});
			}
            return RedirectToAction("UserMovies", "Movie");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var movie = service.GetMovie(id);
            var userid = GetCurrentUserId() ?? "1";
            if (movie?.UserId != userid && !User.IsAdmin())
            {
                TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            if (movie != null)
            {
                service.RemoveMovie(movie);
            }

            TempData["delete"] = "Movie removed successfully!";
            if (User.IsAdmin())
			{
				return RedirectToAction("Index", "Movie", new { area = AdministratorAreaName });
			}
			return RedirectToAction("UserMovies", "Movie");
        }

        [Authorize]
        [Route("/Movies/MyMovies")]
        public IActionResult UserMovies()
        {
            var userMovies = service.GetUserMovies(GetCurrentUserId());

            return View(userMovies);
        }

        [Route("/Movies/{movieId}/{movieInfo?}")]
        public IActionResult MovieUser(int movieId, string movieInfo)
        {
            var movie = service.GetMovie(movieId);
            var userId = GetCurrentUserId();
            if (movie == null || movie.TmdbId != null)
            {
                TempData["error"] = "Could not find!";
                return RedirectToAction("Index", "Home");
            }
            if (movie.IsPublic == false && !User.IsAdmin())
            {
                TempData["error"] = "Not approved yet!";
                return RedirectToAction("Index", "Home");
            }

            var lastReview = service.GetLastReviewForMovie(movieId);
            var movieModel = new UserSingleMovieModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Tagline = movie.Tagline,
                Runtime = movie.Runtime,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                ReleaseDate = movie.DatePublished,
                Poster = movie.Poster,
                BackdropPath = movie.BackdropPath,
                Trailer = movie.Trailer,
                Genres = movie.Genres,
                Review = lastReview,
                ReviewOwner = lastReview != null ? service.GetUserById(lastReview.UserId) : null,
                IsWatchlistedByUser = service.IsWatchlisted(userId, movie.Id),
                ReviewCount = reviewService.GetReviews(movie.Id).Count(),
                VoteCount = movie.VoteCount,
                Rating = (movie.Rating).ToString(),
                IsRatedByUser = service.IsRatedByUser(userId, movie.Id),
                CurrentUserRating = service.GetCurrentUserMovieRating(userId, movie.Id)
            };

            int? publishYear;
            if (movieModel.ReleaseDate != null)
            {
                publishYear = movieModel.ReleaseDate.Value.Year;
            }
            else
            {
                publishYear = null;
            }

            if (movieInfo != WebUtilities.SimplifyMovieUrlInformation(movieModel.Title, publishYear))
            {
                return BadRequest();
            }

            return View(movieModel);
        }

        [Route("/Movies/{id}-tmdb/{movieInfo?}")]
        public IActionResult MovieTmdb(int id, string movieInfo)
        {
            var movieDataRequest 
                = ControllerConstants.BaseUrl + $"/movie/{id}?" + ControllerConstants.ApiKey;

            var movie = new TmdbSingleMovieModel();
            var movieToCheck = service.GetMovieWithGenresCollectionTmdb(id);
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(movieDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var movieData = JsonConvert.DeserializeObject<TmdbSingleMovieModel>(json);

                int? publishYear = null;
                if (movieData.ReleaseDate != null)
                {
                    publishYear = movieData.ReleaseDate.Value.Year;
				}
                if (movieInfo != WebUtilities.SimplifyMovieUrlInformation(movieData.Title, publishYear))
                {
                    return BadRequest();
                }

                try
                {
                    ValidateSingleMovieData(movieToCheck, movieData);
                    service.SaveChanges();
                }
                catch (Exception)
                {
                    // Most likely invalid movie ID
                    return RedirectToAction("Error", "Home");
                }

                movieToCheck = service.GetMovieWithGenresCollectionTmdb(id);
                movie = GetSingleMovieData(movieData, movieToCheck);

                service.SaveChanges();
            }

            if (movie.Actors != null)
            {
                foreach (var actor in movie.Actors)
                {
                    if (!service.IsActorPresent(actor.Id))
                    {
                        var newActor = new Actor()
                        {
                            TmdbId = actor.Id,
                            Gender = actor.Gender,
                            Name = actor.Name,
                            Photo = actor.Photo
                        };
                        service.AddActorToMovie(id, newActor);
                    }
                    else 
                    {
                        var moiveWithActors = service.GetMovieWithActorsTmdb(movie.TmdbId);
                        if (!moiveWithActors.Actors.Any(a => a.Id == actor.Id))
                        {
                            service.AddActorToMovie(id, service.GetActorTmdb(actor.Id));
                        }
                    }
                }
            }
            service.SaveChanges();
            if (movie.Crew != null)
            {
                foreach (var crewMember in movie.Crew)
                {
                    if (!service.IsCrewMemberPresent(crewMember.Id))
                    {
                        var newCrewMember = new Director()
                        {
                            TmdbId = crewMember.Id,
                            Gender = crewMember.Gender,
                            Name = crewMember.Name,
                            Photo = crewMember.Photo
                        };
                        service.AddCrewMemberToMovie(id, newCrewMember);
                    }
                    else
                    {
                        var moiveWithCrew = service.GetMovieWithCrewMembersTmdb(movie.TmdbId);
                        if (!moiveWithCrew.Directors.Any(a => a.Id == crewMember.Id))
                        {
                            service.AddCrewMemberToMovie(id, service.GetCrewMemberTmdb(crewMember.Id));
                        }
                    }
                }
            }
            service.SaveChanges();

            return View(new TmdbMoviePageModel
            {
                Movie = movie,
                RecommendedMovies = TmdbApiCalls.RecommendationsMoviesRequest(movie.TmdbId)
            });
        }

        [ActionName("Popular")]
        [Route("/Movies/Popular")]
        public IActionResult Popular(PopularViewModel model)
        {
            if (model.CurrentPage <= 0)
            {
                model.CurrentPage = 1;
            }
			else if (model.CurrentPage > 500)
			{
				model.CurrentPage = 500;
			}

			string mostPopularRequest
				= ControllerConstants.BaseUrl + $"/discover/movie?sort_by=popularity.desc&page={model.CurrentPage}&" + ControllerConstants.ApiKey;

			var movies = new List<PopularMovieResultModule>();
			CollectMoviesData(mostPopularRequest, movies);

			return View(new PopularViewModel
			{
				Movies = movies,
				CurrentPage = model.CurrentPage
			});
		}

		[Route("/Movies/TopRated")]
        public IActionResult TopRated(TopRatedViewModel model)
        {
			if (model.CurrentPage <= 0)
			{
				model.CurrentPage = 1;
			}
            else if (model.CurrentPage > 15)
            {
                model.CurrentPage = 15;
            }

			string topRatedRequest 
                = ControllerConstants.BaseUrl + $"/discover/movie?sort_by=vote_average.desc&vote_count.gte=9200&page={model.CurrentPage}&" + ControllerConstants.ApiKey;
            var movies = new List<PopularMovieResultModule>();
            CollectMoviesData(topRatedRequest, movies);

            return View(new TopRatedViewModel
            {
                Movies = movies,
                CurrentPage = model.CurrentPage
            });
        }

        [Authorize]
        [Route("/Watchlist")]
        public IActionResult UserWatchlist()
        {
            var movies = service.GetWatchlistedMovies(GetCurrentUserId());
            return View(movies);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToWatchlist(int movieId, string movieInfo)
        {
            var movie = service.GetMovie(movieId);
            var userId = GetCurrentUserId();

            if (movie != null)
            {
                if (service.IsWatchlisted(userId, movieId) == false)
                {
                    service.AddToWatchlist(userId, movieId);
                }

                if (movie.TmdbId == null)
                {
                    return RedirectToActionPermanent("MovieUser", "Movie", new { movieId = movieId, movieInfo = movieInfo });
                }
                else
                {
                    return RedirectToActionPermanent("MovieTmdb", "Movie", new { id = movie.TmdbId, movieInfo = movieInfo });
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveFromWatchlist(int movieId, string movieInfo)
        {
            var movie = service.GetMovie(movieId);
            var userId = GetCurrentUserId();

            if (movie != null)
            {
                if (service.IsWatchlisted(userId, movieId) == true)
                {
                    service.RemoveFromWatchlist(userId, movieId);

                }

                if (movie.TmdbId == null)
                {
                    return RedirectToActionPermanent("MovieUser", "Movie", new { movieId = movieId, movieInfo = movieInfo });
                }
                else
                {
                    return RedirectToActionPermanent("MovieTmdb", "Movie", new { id = movie.TmdbId, movieInfo = movieInfo });
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUserRating(UserSingleMovieModel model, int movieId, string movieInfo)
        {
            var movie = service.GetMovie(movieId);
            var userId = GetCurrentUserId();

            if (movie != null)
            {
                service.RateMovie(userId, movieId, model.UserRating);
                return RedirectToActionPermanent("MovieUser", "Movie", new { movieId = movieId, movieInfo = movieInfo });
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveUserRating(UserSingleMovieModel model, int movieId, string movieInfo)
        {
            var movie = service.GetMovie(movieId);
            var userId = GetCurrentUserId();

            if (movie != null)
            {
                service.RemoveMovieRating(userId, movieId);
                return RedirectToActionPermanent("MovieUser", "Movie", new { movieId = movieId, movieInfo = movieInfo });
            }

            return NotFound();
        }

        private string GetCurrentUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private TmdbSingleMovieModel GetSingleMovieData(TmdbSingleMovieModel movie, Movie dbMovie)
        {
            var lastReview = service.GetLastReviewForMovie(dbMovie.Id);

            return new TmdbSingleMovieModel()
            {
                Id = dbMovie.Id,
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
                Crew = GetCrewModels(movie.TmdbId),
                Genres = movie.Genres,
                Review = lastReview,
                ReviewOwner = lastReview != null ? service.GetUserById(lastReview.UserId) : null,
                IsWatchlistedByUser = service.IsWatchlisted(GetCurrentUserId(), dbMovie.Id),
                ReviewCount = reviewService.GetReviews(dbMovie.Id).Count()
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
                        var movieToCheck = service.GetMovieTmdb(movie.TmdbId);
                        if (movieToCheck == null)
                        {
                            var newPopularMovie = new Movie
                            {
                                Title = movie.Title,
                                Description = movie.Description.Length > 800 ? movie.Description[..800] : movie.Description,
                                DatePublished = movie.ReleaseDate,
                                Poster = movie.PosterPath,
                                Rating = float.Parse(movie.Rating),
                                TmdbId = movie.TmdbId,
                                VoteCount = movie.VoteCount
                            };
                            service.AddMovie(newPopularMovie);
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
                    service.SaveChanges();
                }
            }
        }

        private void ValidateSingleMovieData(Movie movieToCheck, TmdbSingleMovieModel movieData)
        {
            if (movieToCheck == null)
            {
                string genres = "";
                foreach (var genre in movieData.Genres)
                {
                    genres += genre.Name + ", ";
                }

                var genresCollection = new List<Genre>();
                foreach (var genre in movieData.Genres)
                {
                    var currentGenre = service.GetGenreByName(genre.Name);
                    if (currentGenre != null)
                    {
                        genresCollection.Add(currentGenre);
                    }
                }

                var newTmdbMovie = new Movie
                {
                    Title = movieData.Title,
                    Description = movieData.Description.Length > 800 ? movieData.Description[..800] : movieData.Description,
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
                    Genres = genres,
                    GenresCollection = genresCollection
                };
                service.AddMovie(newTmdbMovie);
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
                if (movieToCheck.Genres == "" || movieToCheck.Genres == null)
                {
                    string genres = "";
                    foreach (var genre in movieData.Genres)
                    {
                        genres += genre.Name + ", ";
                    }

                    movieToCheck.Genres = genres;
                }
                if (movieToCheck.GenresCollection.Count() == 0)
                {
                    var genresCollection = new List<Genre>();
                    foreach (var genre in movieData.Genres)
                    {
                        var currentGenre = service.GetGenreByName(genre.Name);
                        if (currentGenre != null)
                        {
                            genresCollection.Add(currentGenre);
                        }
                    }

                    movieToCheck.GenresCollection = genresCollection;
                }
            }
        }

        private ICollection<ProductionTeamCastModel> GetActorModels(int? movieId)
        {
            var teamDataRequest 
                = ControllerConstants.BaseUrl + $"/movie/{movieId}/casts?" + ControllerConstants.ApiKey;

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

        private ICollection<ProductionTeamCrewModel> GetCrewModels(int? movieId)
        {
            var teamDataRequest
                = ControllerConstants.BaseUrl + $"/movie/{movieId}/casts?" + ControllerConstants.ApiKey;

            var crewMembersModel = new List<ProductionTeamCrewModel>();
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(teamDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var teamData = JsonConvert.DeserializeObject<ProductionTeamModel>(json);

                if (teamData != null && teamData.Crew != null)
                {
                    foreach (var member in teamData.Crew)
                    {
                        if (member.Job == "Screenplay" || member.Job == "Director" || member.Job == "Producer")
                        {
                            var newCrewMemberModel = new ProductionTeamCrewModel()
                            {
                                Gender = member.Gender,
                                Id = member.Id,
                                Name = member.Name,
                                Photo = member.Photo,
                                Job = member.Job
                            };
                            crewMembersModel.Add(newCrewMemberModel);
                        }
                    }
                }
            }

            return crewMembersModel;
        }
    }
}