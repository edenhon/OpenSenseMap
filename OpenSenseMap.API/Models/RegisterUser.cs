using System.ComponentModel.DataAnnotations;

namespace OpenSenseMap.API.Models
{
    public class RegisterUser : BaseUser
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Password { get; set; }

    }
}