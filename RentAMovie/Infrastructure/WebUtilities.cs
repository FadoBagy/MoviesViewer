namespace RentAMovie.Infrastructure
{
    public class WebUtilities
    {
        public static string SimplifyMovieUrlInformation(string movieTitle, int? movieYear)
        {
            movieTitle = movieTitle.ToLower();
            if (movieTitle.Contains(" "))
            {
                movieTitle = movieTitle.Replace(" ", "-");
            }
            if (movieTitle.Contains(":"))
            {
                movieTitle = movieTitle.Replace(":", "");
            }

            if (movieYear != null)
            {
                return movieTitle + "-" + movieYear;
            }
            else
            {
                return movieTitle;
            }
        }
    }
}
