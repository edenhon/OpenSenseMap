using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenSenseMap.API.Models;
using System.Net.Http.Headers;


namespace OpenSenseMap.API.Services
{
    public class AuthenticationService(HttpService httpService) : IAuthenticationService
    {
        private readonly HttpService httpService = httpService;

        async Task<IActionResult> IAuthenticationService.RegisterAsync(RegisterUser user)
        {

            try
            {
                HttpResponseMessage response = await httpService.Client.PostAsJsonAsync("/users/register", user);

                response.EnsureSuccessStatusCode();

                // Deserialize the response content into AuthResponse  
                string responseContent = await response.Content.ReadAsStringAsync();

                // Format the JSON string for better readability
                string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseContent), Formatting.Indented);

                // Deserialize the response content into AuthResponse  
                var authResponse = JsonConvert.DeserializeObject<Response<User>>(responseContent);

                // check if authResponse is null
                if (authResponse == null)
                {
                    return new ContentResult
                    {
                        Content = formattedJson,
                        ContentType = "application/json",
                        StatusCode = (int)response.StatusCode
                    };
                }
                else
                {
                    // Check if the response is successful and contains a token
                    if (!string.IsNullOrEmpty(authResponse.Token))
                    {
                        // Store the token in cache memory
                        httpService.SetToken(authResponse.Token);
                    }

                    // Return the formatted JSON string as a ContentResult
                    return new ContentResult
                    {
                        Content = formattedJson,
                        ContentType = "application/json",
                        StatusCode = (int)response.StatusCode
                    };
                }

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
            finally
            {
                // Ensure the Authorization of client is reset properly
                // This is important to avoid sending the token in subsequent requests
                httpService.Client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(httpService.GetToken())
                   ? null
                   : new AuthenticationHeaderValue("Bearer", httpService.GetToken());
            }
        }

        async Task<IActionResult> IAuthenticationService.LoginAsync(LoginUser user)
        {

            try
            {
                HttpResponseMessage response = await httpService.Client.PostAsJsonAsync("/users/sign-in", user);

                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                // Format the JSON string for better readability
                string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseContent), Formatting.Indented);

                // Deserialize the response content into AuthResponse  
                var authResponse = JsonConvert.DeserializeObject<Response<User>>(formattedJson);

                if (authResponse == null)
                {
                    return new ContentResult
                    {
                        Content = formattedJson,
                        ContentType = "application/json",
                        StatusCode = (int)response.StatusCode
                    };
                }
                else
                {
                    // Check if the response is successful and contains a token
                    if (!string.IsNullOrEmpty(authResponse.Token))
                    {
                        // Store the token in cache memory
                        httpService.SetToken(authResponse.Token);
                    }

                    // Return the formatted JSON string as a ContentResult
                    return new ContentResult
                    {
                        Content = formattedJson,
                        ContentType = "application/json",
                        StatusCode = (int)response.StatusCode
                    };
                }
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
            finally
            {
                // Ensure the Authorization of client is reset properly
                // This is important to avoid sending the token in subsequent requests
                httpService.Client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(httpService.GetToken())
                   ? null
                   : new AuthenticationHeaderValue("Bearer", httpService.GetToken());
            }
        }

        async Task<IActionResult> IAuthenticationService.LogoutAsync()
        {

            try
            {
                // Explicitly set the Authorization header with the token.
                httpService.Client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(httpService.GetToken())
                   ? null
                   : new AuthenticationHeaderValue("Bearer", httpService.GetToken());

                HttpResponseMessage response = await httpService.Client.PostAsync("/users/sign-out", null);

                response.EnsureSuccessStatusCode();

                // Deserialize the response content into AuthResponse  
                string responseContent = await response.Content.ReadAsStringAsync();

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
            finally
            {
                // Remove the token from cache memory
                // Explicitly set the Authorization header to null
                // This is important to avoid sending the token in subsequent requests
                if (httpService.GetToken() != null)
                {
                    httpService.RemoveToken();
                }
                httpService.Client.DefaultRequestHeaders.Authorization = null;
            }
        }

    }
}
