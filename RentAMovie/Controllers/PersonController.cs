namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;
    using RentAMovie.Services.Person;
    using System;
    using System.Collections;

    public class PersonController : Controller
    {
        private readonly IPersonService service;
        public PersonController(IPersonService service)
        {
            this.service = service;
        }

        [Route("/Person/{id}-tmdb")]
        public IActionResult PersonTmdb(int id)
        {
            var personDataRequest 
                = ControllerConstants.BaseUrl + $"/person/{id}?" + ControllerConstants.ApiKey;

            ViewTmdbSinglePersonModel person;
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(personDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var personData = JsonConvert.DeserializeObject<ViewTmdbSinglePersonModel>(json);

                person = new ViewTmdbSinglePersonModel{
                    Biography = personData.Biography,
                    DateOfBirth = personData.DateOfBirth,
                    DeathDay = personData.DeathDay,
                    Gender = personData.Gender,
                    Department = personData.Department,
                    Name = personData.Name,
                    PlaceOfBirth = personData.PlaceOfBirth,
                    Photo = personData.Photo,
                    TmdbId = personData.TmdbId
                };
            }

            try
            {
                if (person.Department == "Acting")
                {
                    service.ValidateActorData(person);
                    return View(new ViewPersonModel
                    {
                        PersonData = person,
                        Movies = service
                            .GetActorWithMovies(person.TmdbId).PlayedInMovies
                            .OrderByDescending(m => m.DatePublished)
                            .ToList()
                    });
                }
                else
                {
                    service.ValidateDirectorData(person);
                    return View(new ViewPersonModel
                    {
                        PersonData = person,
                        Movies = service
                            .GetDirectorWithMovies(person.TmdbId).DirectedMovies
                            .OrderByDescending(m => m.DatePublished)
                            .ToList()
                    });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Route("/Movies/{movieId}-tmdb/Cast")]
        public IActionResult List(int movieId)
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

            var movie = service.GetMovieTmdb(movieId);
            if (movie == null)
            {
                TempData["error"] = "Could not find!";
                return RedirectToAction("Index", "Home");
            }

            return View(new ViewAllCastModel
            {
                Movie = movie,
                Actors = actorsModel
            });
        }
    }
}
