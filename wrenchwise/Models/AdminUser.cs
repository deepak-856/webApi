namespace wrenchwise.Models
{
    public class AdminUser
    {
        public int LoginId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? RoleType { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
