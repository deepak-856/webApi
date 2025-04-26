using System.ComponentModel.DataAnnotations;

namespace wrenchwise.Models
{
    public class RegisterRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        //public int RoleType { get; set; } // "Admin", "User", "Technician"

        //[Required]
        //[EmailAddress]
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        //public int CreatedBy { get; set; }  // Use 0 for self-registration
    }
}
