using Microsoft.AspNetCore.Mvc;
using OpenSenseMap.API.Services;
using OpenSenseMap.API.Models;

namespace OpenSenseMap.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SenseBoxController(ISenseBoxService senseBoxService) : ControllerBase
    {
        private readonly ISenseBoxService senseBoxService = senseBoxService;

        /// <summary>  
        /// Creates a new SenseBox with the provided details.  
        /// </summary>  
        /// <param name="senseBox">The details of the SenseBox to be created, including email, name, exposure, model, and location.</param>  
        /// <returns>  
        /// An IActionResult containing the result of the operation:  
        /// - 201 Created: If the SenseBox is successfully created.  
        /// - 400 Bad Request: If the request is invalid or the SenseBox creation fails.  
        /// - 401 Unauthorized: If the user is not authorized to perform this action.  
        /// - 403 Forbid: If access to the resource is forbidden.  
        /// - 404 Not Found: If a required resource for the operation is not found.  
        /// - 409 Conflict: If there is a conflict in the request.  
        /// - 204 No Content: If no content is available for the request.  
        /// - 500 Internal Server Error: If an unexpected error occurs.  
        /// </returns>
        [HttpPost]
        [Route("NewSenseBox")]
        public async Task<IActionResult> NewSenseBox(NewSenseBox senseBox)
        {
            try
            {
                var response = await senseBoxService.NewSenseBoxAsync(senseBox);

                if (response == null)
                {
                    return BadRequest("Bad request! Failed to create a new sense box.");
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
                    return CreatedAtAction(nameof(NewSenseBox), created.Value);
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
                // Log the error message for debugging purposes
                return Problem($"Something went wrong in the {nameof(NewSenseBox)}: {e.Message}", statusCode: 500);
            }
            catch (Exception e)
            {
                return Problem($"Something went wrong in the {nameof(NewSenseBox)}: {e.Message}", statusCode: 500);
            }
        }

        /// <summary>
        /// Retrieves a SenseBox by its unique identifier.
        /// </summary>
        /// <param name="senseBoxid">The unique identifier of the SenseBox to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the result of the operation:
        /// - 200 OK: If the SenseBox is successfully retrieved.
        /// - 400 Bad Request: If the request is invalid or the SenseBox retrieval fails.
        /// - 401 Unauthorized: If the user is not authorized to access the resource.
        /// - 403 Forbid: If access to the resource is forbidden.
        /// - 404 Not Found: If the SenseBox with the specified ID does not exist.
        /// - 409 Conflict: If there is a conflict in the request.
        /// - 204 No Content: If no content is available for the request.
        /// - 500 Internal Server Error: If an unexpected error occurs.
        /// </returns>
        [HttpGet()]
        [Route("GetSenseBoxById")]
        public async Task<IActionResult> GetSenseBoxById(string senseBoxid)
        {
            try
            {

                var response = await senseBoxService.GetSenseBoxByIdAsync(senseBoxid);

                if (response == null)
                {
                    return BadRequest("Bad request: Failed to get the sense box.");
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
                    return CreatedAtAction(nameof(GetSenseBoxById), created.Value);
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
                return Problem($"Something went wrong in the {nameof(NewSenseBox)}: {e.Message}", statusCode: 500);
            }
            catch (Exception e)
            {
                return Problem($"Something went wrong in the {nameof(NewSenseBox)}: {e.Message}", statusCode: 500);
            }
        }
    }
}
