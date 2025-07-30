using System.Text.Json.Serialization;

namespace berber.Models
{

    public enum UserRole
    {
        Customer,
        Barber,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } // Düz şifre değil, hash
        public UserRole Role { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
