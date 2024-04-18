using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApp.Entities
{ 
        public class ServiceEvent
        {
        public uint Id { get; set; }
        public uint ServiceId { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public virtual Service Service { get; set; }
    }
    }

