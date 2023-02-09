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
            if (movieTitle.Contains("&"))
            {
                movieTitle = movieTitle.Replace("&", "and");
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

        public static string GetRatingColor(string? movieRating)
        {
			string rating = "0.0";

			if (movieRating != null)
			{
				rating = movieRating;
			}

			if (float.Parse(rating) >= 8)
			{
				return "green";
			}
			else if (float.Parse(rating) >= 5)
			{
                return "orange";
			}
			else if (float.Parse(rating) < 5 && float.Parse(rating) > 0)
			{
                return "red";
			}
			else
			{
                return "black";
			}
		}

        public static int? GetReleaseDateYear(DateTime? date)
        {
            if (date != null)
            {
                return date.Value.Year;
            }
            else
            {
                return null;
            }
        }
    }
}
