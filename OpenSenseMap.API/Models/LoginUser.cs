using System.ComponentModel.DataAnnotations;

namespace OpenSenseMap.API.Models
{
    public class LoginUser : BaseUser
    {
        [Required]
        public required string Password { get; set; }

    }
}