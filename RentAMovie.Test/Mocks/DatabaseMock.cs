namespace RentAMovie.Test.Mocks
{
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data;

    public static class DatabaseMock
    {
        public static ViewMoviesDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ViewMoviesDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new ViewMoviesDbContext(dbContextOptions);
            }
        }
    }
}
