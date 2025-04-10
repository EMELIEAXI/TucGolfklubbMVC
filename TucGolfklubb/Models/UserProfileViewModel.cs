namespace TucGolfklubb.Models
{
    public class UserProfileViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }
    }
}
