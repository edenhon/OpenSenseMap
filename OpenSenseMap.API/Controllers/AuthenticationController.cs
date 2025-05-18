using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSenseMap.API.Services;
using OpenSenseMap.API.Models;

namespace OpenSenseMap.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService authenticationService = authenticationService;


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>  
        /// Returns an IActionResult indicating the result of the operation.  
        /// Possible responses include:  
        /// - BadRequest (400) if the request is invalid.  
        /// - Unauthorized (401) if the user is not authenticated or credentials are incorrect.  
        /// - NotFound (404) if the requested resource is not found.  
        /// - Conflict (409) if there is a conflict during the operation.  
        /// - Forbid (403) if the operation is forbidden.  
        /// - Created (201) if a new resource is created as part of the operation.  
        /// - Ok (200) if the operation is successful.  
        /// - NoContent (204) if the operation is successful with no additional content.  
        /// </returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            try
            {
                var response = await authenticationService.RegisterAsync(user);

                if (response == null)
                {
                    return BadRequest("Bad request: Login faiiled.");
                }

                if (response is ConflictObjectResult conflict)
                {
                    return Conflict(conflict.Value);
                }

                if (response is UnauthorizedObjectResult unauthorized)
                {
                    return Unauthorized(unauthorized.Value);
                }

                if (response is NotFoundObjectResult notFound)
                {
                    return NotFound(notFound.Value);
                }

                if (response is ForbidResult)
                {
                    return Forbid();
                }

                if (response is BadRequestObjectResult badRequest)
                {
                    return BadRequest(badRequest.Value);
                }

                if (response is CreatedAtActionResult created)
                {
                    return CreatedAtAction(nameof(Login), created.Value);
                }

                if (response is OkObjectResult ok)
                {
                    return Ok(ok.Value);
                }

                if (response is NoContentResult noContent)
                {
                    return NoContent();
                }

                // If the response is not one of the expected types, return it as is
                return response;
            }
            catch (HttpRequestException e)
            {
                // Return an error response in case of failure  
                return new ContentResult
                {
                    Content = $"{{\"error\":\"{e.Message}\"}}",
                    ContentType = "application/json",
                    StatusCode = e.StatusCode == System.Net.HttpStatusCode.Unauthorized ? 401 : 500
                };
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                return new ContentResult
                {
                    Content = $"{{\"error\":\"{ex.Message}\"}}",
                    ContentType = "application/json",
                    StatusCode = 500
                };
            }
        }
        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>  
        /// Returns an IActionResult containing the result of the login operation.  
        /// Possible responses include:  
        /// - BadRequest (400) if the login request is invalid.  
        /// - Unauthorized (401) if the credentials are incorrect.  
        /// - NotFound (404) if the user is not found.  
        /// - Conflict (409) if there is a conflict during the login process.  
        /// - Forbid (403) if the operation is forbidden.  
        /// - Created (201) if a new resource is created as part of the login process.  
        /// - Ok (200) if the login is successful.  
        /// - NoContent (204) if the login is successful with no additional content.  
        /// </returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            try
            {
                var response = await authenticationService.LoginAsync(user);

                if (response == null)
                {
                    return BadRequest("Bad request: Login faiiled.");
                }

                if (response is ConflictObjectResult conflict)
                {
                    return Conflict(conflict.Value);
                }

                if (response is UnauthorizedObjectResult unauthorized)
                {
                    return Unauthorized(unauthorized.Value);
                }

                if (response is NotFoundObjectResult notFound)
                {
                    return NotFound(notFound.Value);
                }

                if (response is ForbidResult)
                {
                    return Forbid();
                }

                if (response is BadRequestObjectResult badRequest)
                {
                    return BadRequest(badRequest.Value);
                }

                if (response is CreatedAtActionResult created)
                {
                    return CreatedAtAction(nameof(Login), created.Value);
                }

                if (response is OkObjectResult ok)
                {
                    return Ok(ok.Value);
                }

                if (response is NoContentResult noContent)
                {
                    return NoContent();
                }

                // If the response is not one of the expected types, return it as is
                return response;
            }
            catch (HttpRequestException e)
            {
                // Return an error response in case of failure  
                return new ContentResult
                {
                    Content = $"{{\"error\":\"{e.Message}\"}}",
                    ContentType = "application/json",
                    StatusCode = e.StatusCode == System.Net.HttpStatusCode.Unauthorized ? 401 : 500
                };
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                return new ContentResult
                {
                    Content = $"{{\"error\":\"{ex.Message}\"}}",
                    ContentType = "application/json",
                    StatusCode = 500
                };
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>
        /// Returns an IActionResult indicating the result of the logout operation.
        /// Possible responses include:
        /// - Unauthorized (401) if the user is not authenticated.
        /// - NotFound (404) if the user session is not found.
        /// - Forbid (403) if the operation is forbidden.
        /// - BadRequest (400) if the request is invalid.
        /// - NoContent (204) if the logout is successful with no additional content.
        /// - Conflict (409) if there is a conflict during the operation.
        /// - Created (201) if a new resource is created as part of the logout process.
        /// </returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var response = await authenticationService.LogoutAsync();

                if (response is UnauthorizedObjectResult unauthorized)
                {
                    return Unauthorized(unauthorized.Value);
                }

                if (response is NotFoundObjectResult notFound)
                {
                    return NotFound(notFound.Value);
                }

                if (response is ForbidResult)
                {
                    return Forbid();
                }

                if (response is BadRequestObjectResult badRequest)
                {
                    return BadRequest(badRequest.Value);
                }

                if (response is NoContentResult noContent)
                {
                    return NoContent();
                }

                if (response is ConflictObjectResult conflict)
                {
                    return Conflict(conflict.Value);
                }

                if (response is CreatedAtActionResult created)
                {
                    return CreatedAtAction(nameof(Logout), created.Value);
                }

                return response;
            }
            catch (HttpRequestException e)
            {
                // Return an error response in case of failure  
                return new ContentResult
                {
                    Content = $"{{\"error\":\"{e.Message}\"}}",
                    ContentType = "application/json",
                    StatusCode = e.StatusCode == System.Net.HttpStatusCode.Unauthorized ? 401 : 500
                };
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                return new ContentResult
                {
                    Content = $"{{\"error\":\"{ex.Message}\"}}",
                    ContentType = "application/json",
                    StatusCode = 500
                };
            }
        }
    }
}
