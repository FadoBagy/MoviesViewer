namespace RentAMovie.Infrastructure
{
    using Newtonsoft.Json;
    using RentAMovie.Controllers;
    using RentAMovie.Models.MovieModuls;

    public class TmdbApiCalls
    {
        public static List<PopularMovieResultModule> TopActionMoviesRequest()
        {
            var topRatedActionsRequest
                = ControllerConstants.BaseUrl + "/discover/movie?with_genres=28&sort_by=vote_average.desc&vote_count.gte=500&" + ControllerConstants.ApiKey;
            var topActionMovies = new List<PopularMovieResultModule>();

            using var httpClient = new HttpClient();
            var endpoint = new Uri(topRatedActionsRequest);
            var result = httpClient.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;

            var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
            if (movieDto != null && movieDto.Results != null)
            {
                foreach (var movie in movieDto.Results)
                {
                    topActionMovies.Add(movie);
                }
            }

            return topActionMovies;
        }

        public static List<PopularMovieResultModule> NowPlayingMoviesRequest()
        {
            var nowPlayingRequest
                = ControllerConstants.BaseUrl + "/movie/now_playing?" + ControllerConstants.ApiKey;
            var nowPlayingMovies = new List<PopularMovieResultModule>();

            using var httpClient = new HttpClient();
            var endpoint = new Uri(nowPlayingRequest);
            var result = httpClient.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;

            var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
            if (movieDto != null && movieDto.Results != null)
            {
                foreach (var movie in movieDto.Results)
                {
                    nowPlayingMovies.Add(movie);
                }
            }

            return nowPlayingMovies;
        }

        public static List<PopularMovieResultModule> TrendingMoviesRequest()
        {
            var trendingRequest
                = ControllerConstants.BaseUrl + "/trending/movie/week?" + ControllerConstants.ApiKey;
            var trendingMovies = new List<PopularMovieResultModule>();

            using var httpClient = new HttpClient();
            var endpoint = new Uri(trendingRequest);
            var result = httpClient.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;

            var movieDto = JsonConvert.DeserializeObject<PopularMovieModule>(json);
            if (movieDto != null && movieDto.Results != null)
            {
                foreach (var movie in movieDto.Results)
                {
                    trendingMovies.Add(movie);
                }
            }

            return trendingMovies;
        }
    }
}
