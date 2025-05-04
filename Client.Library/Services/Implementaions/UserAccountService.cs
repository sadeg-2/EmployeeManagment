using System.Net.Http.Json;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
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
            var response = await client.PostAsJsonAsync($"{AuthURL}/refresh-token", token);
            if (!response.IsSuccessStatusCode) return new LoginResponse(false, "Error Occured");
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>()!;
            return result!;
        }
        public async Task<WeatherForecast[]> GetWeatherForecast()
        {
            var client = await getHttpClient.GetPrivateHttpClient();
            var result = await client.GetFromJsonAsync<WeatherForecast[]>("api/WeatherForecast");
            return result!;

        }

        public async Task<List<ManageUser>> GetUsers()
        {
            var client = await getHttpClient.GetPrivateHttpClient();
            var result = await client.GetFromJsonAsync<List<ManageUser>>($"{AuthURL}/users");
            return result!;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var client = await getHttpClient.GetPrivateHttpClient();
            var response = await client.PutAsJsonAsync($"{AuthURL}/update",user);
            if (!response.IsSuccessStatusCode)
            {
                return new GeneralResponse(false, "Error occured");
            }
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<List<SystemRole>> GetRoles()
        {
            var client = await getHttpClient.GetPrivateHttpClient();
            var result = await client.GetFromJsonAsync<List<SystemRole>>($"{AuthURL}/roles");
            return result!;
        }

        public async Task<GeneralResponse> DeleteUser(int id)
        {
            var client = await getHttpClient.GetPrivateHttpClient();
            var response = await client.DeleteAsync($"{AuthURL}/delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return new GeneralResponse(false, "Error occured");
            }
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
