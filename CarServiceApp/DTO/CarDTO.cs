namespace CarServiceApp.DTO
{
    public class CarDTO
    {
        public uint OwnerUserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
    }
}
