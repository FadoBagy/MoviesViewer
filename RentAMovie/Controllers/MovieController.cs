namespace RentAMovie.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Data.Models;
    using RentAMovie.Models;

    public class MovieController : Controller
    {
        [ActionName("List")]
        [Route("/Movie/List")]
        public IActionResult List()
        {
            var reviews = new List<Movie>()
            {
                new Movie{
                    Id = 1,
                    Title = "The Dark Knight",
                    Description = "Story about Bruce Wayne as Batman fighting for justice.",
                    DateCreated = DateTime.Now},
                new Movie{
                    Id = 2,
                    Title = "Joker",
                    Description = "Where normal meets subnormal.",
                    DateCreated = DateTime.Now}
            };

            return View(reviews);
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
