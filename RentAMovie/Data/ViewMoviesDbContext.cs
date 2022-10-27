namespace RentAMovie.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data.Models;

    public class ViewMoviesDbContext : IdentityDbContext<User>
    {
        public ViewMoviesDbContext(DbContextOptions<ViewMoviesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder
            //    .Entity<Movie>()
            //    .HasOne(m => m.User)
            //    .WithMany(u => u.UploadedMovies)
            //    .HasForeignKey(m => m.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder
            //   .Entity<Director>()
            //   .Property(x => x.Name)
            //   .HasColumnType("nvarchar(56)");

            //modelBuilder
            //     .Entity<StudentCourse>(e =>
            //     {
            //         e.HasKey(sc => new { sc.StudentId, sc.CourseId });
            //     });

            modelBuilder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 28,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 35,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 16,
                    Name = "Animation"
                },
                new Genre()
                {
                    Id = 9648,
                    Name = "Mystery"
                },
                new Genre()
                {
                    Id = 27,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 18,
                    Name = "Drama"
                },
                new Genre()
                {
                    Id = 53,
                    Name = "Thriller"
                },
                new Genre()
                {
                    Id = 12,
                    Name = "Adventure"
                },
                new Genre()
                {
                    Id = 878,
                    Name = "Science Fiction"
                },
                new Genre()
                {
                    Id = 80,
                    Name = "Crime"
                },
                new Genre()
                {
                    Id = 99,
                    Name = "Documentary"
                },
                new Genre()
                {
                    Id = 14,
                    Name = "Fantasy"
                },
                new Genre()
                {
                    Id = 36,
                    Name = "History"
                },
                new Genre()
                {
                    Id = 10749,
                    Name = "Romance"
                },
                new Genre()
                {
                    Id = 10752,
                    Name = "War"
                },
                new Genre()
                {
                    Id = 37,
                    Name = "Western"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}