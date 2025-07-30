namespace berber.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int ServiceId { get; set; }

        // Navigation
        public User? User { get; set; }
        public Service? Service { get; set; }
    }
}
