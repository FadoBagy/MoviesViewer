namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;
    using System;

    public class PersonController : Controller
    {
        private readonly string apiKey = "api_key=827b5d3636ed4d470d182016543dc5cf";
        private readonly string baseUrl = "https://api.themoviedb.org/3";

        private readonly ViewMoviesDbContext data;
        public PersonController(ViewMoviesDbContext data)
        {
            this.data = data;
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

            ValidatePersonData(person);
            data.SaveChanges();

            return View(person);
        }

        private void ValidatePersonData(ViewTmdbSingleActorModel person)
        {
            var personOtCheck = data.Actors.FirstOrDefault(a => a.TmdbId == person.TmdbId);
            if (personOtCheck == null)
            {
                data.Actors.Add(new Actor()
                {
                    Name = person.Name,
                    Biography = person.Biography,
                    Photo = person.Photo,
                    Gender = person.Gender,
                    DateOfBirth = person.DateOfBirth,
                    DeathDay = person.DeathDay,
                    PlaceOfBirth = person.PlaceOfBirth,
                    TmdbId = person.TmdbId
                });
            }
            else
            {
                if (personOtCheck.Name != person.Name)
                {
                    personOtCheck.Name = person.Name;
                }
                if (personOtCheck.Biography != person.Biography)
                {
                    personOtCheck.Biography = person.Biography;
                }
                if (personOtCheck.Photo != person.Photo)
                {
                    personOtCheck.Photo = person.Photo;
                }
                if (personOtCheck.Gender != person.Gender)
                {
                    personOtCheck.Gender = person.Gender;
                }
                if (personOtCheck.DateOfBirth != person.DateOfBirth)
                {
                    personOtCheck.DateOfBirth = person.DateOfBirth;
                }
                if (personOtCheck.DeathDay != person.DeathDay)
                {
                    personOtCheck.DeathDay = person.DeathDay;
                }
                if (personOtCheck.PlaceOfBirth != person.PlaceOfBirth)
                {
                    personOtCheck.PlaceOfBirth = person.PlaceOfBirth;
                }
            }
        }
    }
}
