using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace Client.Library.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse?> CreateAsync(Register register);
        Task<LoginResponse?> SignInAsync(Login login);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        Task<List<ManageUser>> GetUsers();
        Task<GeneralResponse> UpdateUser(ManageUser user);
        Task<List<SystemRole>> GetRoles();
        Task<GeneralResponse> DeleteUser(int id);

    }
}
