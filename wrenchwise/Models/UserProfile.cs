namespace wrenchwise.Models
{
    public class UserProfile
    {
        public int LoginId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? RoleType { get; set; }
        public string? CreatedBy { get; set; }
        public string? Address { get; set; }
        public DateTime? UserCreatedAt { get; set; }
        public DateTime? UserUpdatedAt { get; set; }
    }
}
