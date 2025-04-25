using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace Client.Library.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse?> CreateAsync(Register register);
        Task<LoginResponse?> SignInAsync(Login login);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        Task<WeatherForecast[]> GetWeatherForecast();
    }
}
