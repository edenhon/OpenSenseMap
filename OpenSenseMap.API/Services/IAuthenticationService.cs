using Microsoft.AspNetCore.Mvc;
using OpenSenseMap.API.Models;

namespace OpenSenseMap.API.Services
{
    public interface IAuthenticationService
    {
        Task<IActionResult> RegisterAsync(RegisterUser user);
        Task<IActionResult> LoginAsync(LoginUser user);
        Task<IActionResult> LogoutAsync();
    }
}
