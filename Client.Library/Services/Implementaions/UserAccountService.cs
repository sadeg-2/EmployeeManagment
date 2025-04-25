using System.Net.Http.Json;
using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using Client.Library.Helpers;
using Client.Library.Services.Contracts;

namespace Client.Library.Services.Implementaions
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        public const string AuthURL = "api/authentication";
        public async Task<GeneralResponse?> CreateAsync(Register user)
        {
            var client = getHttpClient.GetPublicHttpClient();
            var result = await client.PostAsJsonAsync($"{AuthURL}/register",user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error Occured");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!; 
        }
        public async Task<LoginResponse?> SignInAsync(Login user)
        {
            var client = getHttpClient.GetPublicHttpClient();
            var result = await client.PostAsJsonAsync($"{AuthURL}/login", user);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error Occured");
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }
        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            var client = getHttpClient.GetPublicHttpClient();
            var result = await client.PostAsJsonAsync($"{AuthURL}/refresh-token", token);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error Occured");
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }
        public async Task<WeatherForecast[]> GetWeatherForecast()
        {
            var client = await getHttpClient.GetPrivateHttpClient();
            var result = await client.GetFromJsonAsync<WeatherForecast[]>("api/WeatherForecast");
            return result!;

        }

       
    }
}
