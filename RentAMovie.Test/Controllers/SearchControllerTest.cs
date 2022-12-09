namespace RentAMovie.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAMovie.Controllers;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Search;
    using RentAMovie.Services.Search;
    using RentAMovie.Test.Mocks;

    public class SearchControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithModelData()
        {
            using var data = PrepareDbWithMovie();
            var service = new SearchService(data);
            var searchController = new SearchController(service);

            var result = searchController.Index(new SearchQueryModel
            {
                CurrentPage = 1,
                SearchTerm = "test",
                SortBy = 1
            });

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var searchQueryModel = Assert.IsType<SearchQueryModel>(model);
            Assert.True(searchQueryModel.SearchTerm == "test");
            Assert.True(searchQueryModel.CurrentPage == 1);
            Assert.True(searchQueryModel.SortBy == 1);
            Assert.Single(searchQueryModel.Movies);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            using var data = PrepareDbWithMovie();
            var service = new SearchService(data);
            var searchController = new SearchController(service);

            var result = searchController.Index(new SearchQueryModel());

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var searchQueryModel = Assert.IsType<SearchQueryModel>(model);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectSearchTerm()
        {
            using var data = PrepareDbWithMovie();
            var service = new SearchService(data);
            var searchController = new SearchController(service);

            var result = searchController.Index(new SearchQueryModel
            {
                SearchTerm = "test"
            });

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var searchQueryModel = Assert.IsType<SearchQueryModel>(model);
            Assert.True(searchQueryModel.SearchTerm == "test");
        }

        [Fact]
        public void IndexShouldReturnViewWithOneMovie()
        {
            using var data = PrepareDbWithMovie();
            var service = new SearchService(data);
            var searchController = new SearchController(service);

            var result = searchController.Index(new SearchQueryModel
            {
                SearchTerm = "test"
            });

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;
            var searchQueryModel = Assert.IsType<SearchQueryModel>(model);
            Assert.Single(searchQueryModel.Movies);
        }

        private static ViewMoviesDbContext PrepareDbWithMovie()
        {
            var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Id = 1,
                Title = "test",
                Description = "test"
            });
            data.Movies.Add(new Movie
            {
                Id = 2,
                Title = "movie",
                Description = "movie"
            });
            data.SaveChanges();

            return data;
        }
    }
}
