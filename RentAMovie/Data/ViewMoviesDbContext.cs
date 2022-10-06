namespace RentAMovie.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data.Models;

    public class ViewMoviesDbContext : IdentityDbContext
    {
        public ViewMoviesDbContext()
        {

        }

        public ViewMoviesDbContext(DbContextOptions<ViewMoviesDbContext> options)
            : base(options)
        {

        }

        // TODO: change user entity
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder
            //     .Entity<StudentCourse>(e =>
            //     {
            //         e.HasKey(sc => new { sc.StudentId, sc.CourseId });
            //     });
        }
    }
}