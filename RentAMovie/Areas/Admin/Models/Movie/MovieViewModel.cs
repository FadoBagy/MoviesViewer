﻿namespace RentAMovie.Areas.Admin.Models.Movie
{
	public class MovieViewModel
	{
        public List<CardMovieModel>? UnapprovedMovies { get; set; }

        public List<CardMovieModel>? UsersMovies { get; set; }

		public List<CardMovieModel>? TmdbMovies { get; set; }
	}
}
