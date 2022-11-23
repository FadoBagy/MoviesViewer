namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Models.PersonModels;
    using RentAMovie.Services.Person;
    using System;

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

            if (person.Department == "Acting")
            {
                service.ValidateActorData(person);

            }
            else
            {
                service.ValidateDirectorData(person);
            }

            return View(person);
        }
    }
}
