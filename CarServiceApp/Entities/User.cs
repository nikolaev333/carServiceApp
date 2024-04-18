using System.ComponentModel.DataAnnotations;

namespace CarServiceApp.Entities
{
    public class User
    {
        public uint Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
