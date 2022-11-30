namespace RentAMovie.Services.Home
{
    using RentAMovie.Data;

    public class HomeService : IHomeService
    {
        private readonly ViewMoviesDbContext data;

        public HomeService(ViewMoviesDbContext data)
        {
            this.data = data;
        }
		
		public int GetMovieCount()
        {
            return data.Movies.Count();
        }

        public int GetReviewCount()
        {
            return data.Reviews.Count();
        }

        public int GetUserCount()
        {
            return data.Users.Count();
        }

		public int GetActorCount()
		{
			return data.Actors.Count();
		}

		public int GetCastCount()
		{
			return data.Directors.Count();
		}
	}
}
