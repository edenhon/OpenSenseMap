using System.ComponentModel.DataAnnotations;

namespace OpenSenseMap.API.Models
{
    public class BaseUser
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

    }
}