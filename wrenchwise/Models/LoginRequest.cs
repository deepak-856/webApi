namespace wrenchwise.Models
{
    public class LoginRequest
    {
        public string? email { get; set; }     // Email used for login
        public string? password { get; set; }
        public int? role_type { get; set; }
    }
}

