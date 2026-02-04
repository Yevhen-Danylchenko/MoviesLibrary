using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<UserMovie> UserMovies => Set<UserMovie>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // один фільм один раз може бути в біблітеці к-ча
        builder.Entity<UserMovie>()
            .HasIndex(um => new { um.UserId, um.MovieId })
            .IsUnique();

        //зв'язки
        builder.Entity<UserMovie>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMovies)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserMovie>()
            .HasOne(um => um.Movie)
            .WithMany(m => m.UserMovies)
            .HasForeignKey(um => um.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        // початкові дані
        builder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Title = "Втеча з Шоушенка",
                Year = 1994,
                Genre = "Драма",
                Rating = 9.3,
                Type = MediaType.Movie,
                Description = "Історія банкіра Енді Дюфрейна, засудженого до довічного ув'язнення."
            },
            new Movie
            {
                Id = 2,
                Title = "Хрещений батько",
                Year = 1972,
                Genre = "Кримінал, Драма",
                Rating = 9.2,
                Type = MediaType.Movie,
                Description = "Сага про могутню італо-американську кримінальну сім'ю."
            },
            new Movie
            {
                Id = 3,
                Title = "Breaking Bad",
                Year = 2008,
                Genre = "Кримінал, Драма, Трилер",
                Rating = 9.5,
                Type = MediaType.Series,
                Description = "Вчитель хімії стає виробником метамфетаміну."
            },
            new Movie
            {
                Id = 4,
                Title = "Гра престолів",
                Year = 2011,
                Genre = "Фентезі, Драма",
                Rating = 9.3,
                Type = MediaType.Series,
                Description = "Епічна сага про боротьбу за Залізний трон."
            },
            new Movie
            {
                Id = 5,
                Title = "Інтерстеллар",
                Year = 2014,
                Genre = "Наукова фантастика, Драма",
                Rating = 8.7,
                Type = MediaType.Movie,
                Description = "Команда дослідників подорожує крізь червоточину в космосі."
            }
        );
    }
}