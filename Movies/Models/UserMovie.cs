namespace Movies.Models
{
    public class UserMovie
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public WatchStatus Status { get; set; }

        public int? UserRating { get; set; }

        public string? Notes { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public DateTime? WatchedAt { get; set; }
    }

    public enum WatchStatus
    {
        PlanToWatch,
        Watching,
        Completed,
        OnHold,
        Dropped
    }
}

