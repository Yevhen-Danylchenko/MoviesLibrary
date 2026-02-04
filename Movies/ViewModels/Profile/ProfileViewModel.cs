namespace Movies.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime RegisterAt { get; set; }

        public int TotalMovies { get; set; }
        public int TotalSeries { get; set; }
        public int Watching { get; set; }
        public int Completed { get; set; }
        public int PlanToWatch { get; set; }
    }
}
