namespace OpenSenseMap.API.Models
{
    public class NewSenseBox
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Exposure { get; set; }
        public required string Model { get; set; }
        public required Geolocation Location { get; set; }
    }
}
