using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApp.Entities
{
        public class Service
        {
        public uint Id { get; set; }
        public uint CarId { get; set; }
        public DateTime DateReceived { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<ServiceEvent> ServiceEvents { get; set; }

    }
}
