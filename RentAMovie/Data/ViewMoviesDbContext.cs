namespace RentAMovie.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data.Models;
    using RentAMovie.Infrastructure;

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
            modelBuilder.SeedAdmin();
            modelBuilder.SeedGenres();
            base.OnModelCreating(modelBuilder);

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
        }
    }
}