namespace wrenchwise.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? role_type { get; set; }
        public string? name { get; set; }
        public object? Data { get; set; }
    }
}
