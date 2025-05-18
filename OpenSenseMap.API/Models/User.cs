using System.ComponentModel.DataAnnotations;

namespace OpenSenseMap.API.Models
{
    public class User : BaseUser
    {

        [Required]
        public required string Name { get; set; }

        public required string Role { get; set; }

        public required string Language { get; set; }

        public bool EmailIsConfirmed { get; set; }

    }
}
