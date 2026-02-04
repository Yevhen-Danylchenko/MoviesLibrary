using Microsoft.AspNetCore.Mvc.Formatters;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string? Description { get; set; }
        public string? PosterUrl { get; set; }
        public int? Year { get; set; }
        public string? Genre { get; set; }
        public double? Rating { get; set; }
        public MediaType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }

    public enum MediaType
    {
        Movie,
        Series
    }
}
