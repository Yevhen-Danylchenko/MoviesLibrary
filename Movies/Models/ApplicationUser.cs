using Microsoft.AspNetCore.Identity;

namespace Movies.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
