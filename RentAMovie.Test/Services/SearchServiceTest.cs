namespace RentAMovie.Test.Services
{
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.Genre;
    using RentAMovie.Services.Search;
    using RentAMovie.Test.Mocks;

    public class SearchServiceTest
    {
        [Fact]
        public void GetAllGenresShouldReturnListWithGenres()
        {
            using var data = DatabaseMock.Instance;
            data.Genres.Add(new Genre
            {
                Name = "TestGenre"
            });
            data.SaveChanges();
            var searchService = new SearchService(data);

            var result = searchService.GetAllGenres();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<List<ViewGenreModel>>(result);
        }

        [Fact]
        public void ValidateSearchQueryShouldReturnMovies()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Title = "test",
                Description = "test"
            });
            data.SaveChanges();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SearchTerm = "test"
            });

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Movie>>(result);
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByTitle()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Title = "test",
                Description = "test"
            });
            data.Movies.Add(new Movie
            {
                Title = "rest",
                Description = "rest"
            });
            data.SaveChanges();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SearchTerm = "test"
            });

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByGenre()
        {
            using var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Title = "test",
                Description = "test",
                GenresCollection = new List<Genre> 
                {
                    new Genre 
                    {
                        Id = 1,
                        Name = "test"
                    } 
                },
                Genres = "test, "
            });
            data.Movies.Add(new Movie
            {
                Title = "rest",
                Description = "rest"
            });
            data.SaveChanges();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                Genre = 1
            });

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByTitleAscending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 7
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Atest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByTitleDescending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 8
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Zrest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByRatingDescending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 1
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Zrest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByRatingAscending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 2
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Atest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByDatePublishedDescending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 5
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Atest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByDatePublishedAscending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 6
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Zrest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByVoteCountDescending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 9
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Zrest");
        }

        [Fact]
        public void ValidateSearchQueryShouldFilterByVoteCountAscending()
        {
            using var data = PrepareDbToFilter();
            var searchService = new SearchService(data);

            var result = searchService.ValidateSearchQuery(new SearchServiceModel
            {
                SortBy = 10
            });

            Assert.NotNull(result);
            Assert.True(result.FirstOrDefault().Title == "Atest");
        }

        private static ViewMoviesDbContext PrepareDbToFilter()
        {
            var data = DatabaseMock.Instance;
            data.Movies.Add(new Movie
            {
                Title = "Atest",
                Description = "test",
                Rating = 1,
                DatePublished = DateTime.UtcNow,
                VoteCount = 1
            });
            data.Movies.Add(new Movie
            {
                Title = "Zrest",
                Description = "rest",
                Rating = 2,
                DatePublished = DateTime.Parse("05/05/2000"),
                VoteCount = 2
            });
            data.SaveChanges();

            return data;
        }
    }
}
