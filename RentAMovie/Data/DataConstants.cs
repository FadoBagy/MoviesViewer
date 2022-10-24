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

        public const int ActorNameMaxLength = 55;
        public const int PersonBiographyMaxLength = 5000;
        public const int PersonGenderMaxLength = 2;
        public const int PersonGenderMinLength = 1;

        public const int ReviewContentMaxLength = 1000;

        public const int UserUsernameMaxLength = 30;
        public const int UserEmailMaxLength = 350;
    }
}
