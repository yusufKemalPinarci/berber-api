using System.Text.Json.Serialization;

namespace berber.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
