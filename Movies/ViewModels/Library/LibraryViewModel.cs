using Movies.Models;

namespace Movies.ViewModels.Library
{
    public class LibraryViewModel
    {
        public List<UserMovieItem> Items { get; set; } = new();
        public WatchStatus? CurrentFilter { get; set; }
        public MediaType? TypeFilter { get; set; }
        public string? SearchQuery { get; set; }
    }

    public class UserMovieItem
    {
        public int UserMovieId { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public int? Year { get; set; }
        public MediaType Type { get; set; }
        public WatchStatus Status { get; set; }
        public int? UserRating { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
