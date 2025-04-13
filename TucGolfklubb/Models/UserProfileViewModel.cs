namespace TucGolfklubb.Models
{
    public class UserProfileViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImagePath { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }

        public List<UserActivity>? RecentActivities { get; set; }
    }
}
