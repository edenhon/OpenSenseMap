using System.ComponentModel.DataAnnotations;

namespace OpenSenseMap.API.Models
{
    public class Geolocation
    {
        [Range(-90, 90, ErrorMessage = "Latitude between -90 and 90")]
        public double Lat { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude between -180 and 180")]
        public double Lng { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Height above ground in meters.")]
        public double Height { get; set; }
    }
}
