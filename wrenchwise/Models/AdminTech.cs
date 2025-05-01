namespace wrenchwise.Models
{
    public class AdminTech
    {
        public int LoginId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; } // Ideally hash/encrypt in production
        public string? RoleType { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public string? Specialization { get; set; }
        public string? Address { get; set; }
        public int ExperienceYears { get; set; }
    }
}
