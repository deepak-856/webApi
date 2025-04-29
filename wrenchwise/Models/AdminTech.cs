namespace wrenchwise.Models
{
    public class AdminTech
    {
        public int LoginId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? RoleType { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
