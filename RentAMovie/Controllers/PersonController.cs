namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Models.PersonModels;
    using RentAMovie.Services.Person;
    using System;

    public class PersonController : Controller
    {
        private readonly string apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
        private readonly string baseUrl = "https://api.themoviedb.org/3";

        private readonly IPersonService service;
        public PersonController(IPersonService service)
        {
            this.service = service;
        }

        [Route("/Person/{id}-tmdb")]
        public IActionResult PersonTmdb(int id)
        {
            var personDataRequest = baseUrl + $"/person/{id}?" + apiKey;

            ViewTmdbSingleActorModel person;
            using (var httpClient = new HttpClient())
            {
                var endpoint = new Uri(personDataRequest);
                var result = httpClient.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var personData = JsonConvert.DeserializeObject<ViewTmdbSingleActorModel>(json);

                person = new ViewTmdbSingleActorModel{
                    Biography = personData.Biography,
                    DateOfBirth = personData.DateOfBirth,
                    DeathDay = personData.DeathDay,
                    Gender = personData.Gender,
                    Name = personData.Name,
                    PlaceOfBirth = personData.PlaceOfBirth,
                    Photo = personData.Photo,
                    TmdbId = personData.TmdbId
                };
            }

            service.ValidatePersonData(person);

            return View(person);
        }
    }
}
