namespace CarServiceApp.DTO
{
    public class ServiceDTO
    {
        public uint CarId { get; set; }

        public DateTime DateReceived { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
