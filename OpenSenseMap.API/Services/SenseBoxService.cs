using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenSenseMap.API.Models;
using System.Net.Http.Headers;

namespace OpenSenseMap.API.Services
{
    public class SenseBoxService(HttpService httpService) : ISenseBoxService
    {
        private readonly HttpService httpService = httpService;

        async Task<IActionResult> ISenseBoxService.NewSenseBoxAsync(NewSenseBox senseBox)
        {

            try
            {
                // Explicitly set the Authorization header with the token.
                httpService.Client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(httpService.GetToken())
                   ? null
                   : new AuthenticationHeaderValue("Bearer", httpService.GetToken());

                HttpResponseMessage response = await httpService.Client.PostAsJsonAsync("/boxes", senseBox);

                response.EnsureSuccessStatusCode();

                // Deserialize the response content into a strongly-typed object or keep it as a string  
                string responseContent = await response.Content.ReadAsStringAsync();

                // Format the JSON string for better readability
                string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseContent), Formatting.Indented);

                return new ContentResult
                {
                    Content = formattedJson,
                    ContentType = "application/json",
                    StatusCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException e)
            {
                // Handle Forbidden error  
                if (e.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return new ContentResult
                    {
                        Content = $"{{\"error\":\"Access forbidden: {e.Message}\"}}",
                        ContentType = "application/json",
                        StatusCode = 403
                    };
                }

                // Return an error response in case of other failures  
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

        async Task<IActionResult> ISenseBoxService.GetSenseBoxByIdAsync(string id)
        {
            try
            {
                // No authorization header is needed for this request.

                HttpResponseMessage response = await httpService.Client.GetAsync($"/boxes/{id}");

                response.EnsureSuccessStatusCode();

                // Deserialize the response content into a strongly-typed object or keep it as a string  
                string responseContent = await response.Content.ReadAsStringAsync();

                // Format the JSON string for better readability
                string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseContent), Formatting.Indented);

                // Return the formatted JSON string as a ContentResult
                return new ContentResult
                {
                    Content = formattedJson,
                    ContentType = "application/json",
                    StatusCode = (int)response.StatusCode
                };
            }
            catch (HttpRequestException e)
            {
                //If the e is not found
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new NotFoundObjectResult($"{{\"error\":\"{e.Message}\"}}");
                }

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
