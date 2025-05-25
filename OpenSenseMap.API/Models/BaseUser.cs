using System.ComponentModel.DataAnnotations;

namespace OpenSenseMap.API.Models
{
    public abstract class BaseUser
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

    }
}