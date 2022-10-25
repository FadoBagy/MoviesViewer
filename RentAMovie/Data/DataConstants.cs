namespace RentAMovie.Data
{
    public class DataConstants
    {
        public class Movie
        {
            public const int MaxMovieTitle = 210;
            public const int MinMovieTitle = 1;

            public const int MaxMovieDescription = 800;
            public const int MinMovieDescription = 3;

            public const string MaxMovieRuntime = "880";
            public const string MinMovieRuntime = "0";

            public const string MaxMovieRating = "10.0";
            public const string MinMovieRating = "0.0";
        }

        public class User
        {
            public const int MaxUserUsername = 40;
            public const int MinUserUsername = 0;

            public const int MaxUserPassword = 100;
            public const int MinUserPassword = 6;
        }

        public class Person
        {
            public const int MaxPersonName = 56;
            public const int MinPersonName = 0;

            public const int MaxPersonBiography = 5000;
            public const int MinPersonBiography = 0;

            public const string MaxPersonGender = "2";
            public const string MinPersonGender = "1";
        }

        public class Review
        {
            public const int MaxReviewContent = 1000;
        }
    }
}
