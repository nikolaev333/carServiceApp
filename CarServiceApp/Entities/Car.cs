using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApp.Entities
{
    public class Car
    {
        public uint Id { get; set; }
        public uint OwnerUserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
