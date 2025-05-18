using Microsoft.AspNetCore.Mvc;
using OpenSenseMap.API.Models;

namespace OpenSenseMap.API.Services
{
    public interface ISenseBoxService
    {
        Task<IActionResult> NewSenseBoxAsync(NewSenseBox senseBox);
        Task<IActionResult> GetSenseBoxByIdAsync(string senseBoxid);
    }
}
