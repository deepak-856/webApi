namespace wrenchwise.Models
{
    public class UserServiceResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
