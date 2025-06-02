
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace Server.Library.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register register);
        Task<LoginResponse> SignInAsync(Login login);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        Task<List<ManageUser>> GetUsers();
        Task<GeneralResponse> UpdateUser(ManageUser user);
        Task<List<SystemRole>> GetRoles();
        Task<GeneralResponse> DeleteUser(int id);

        Task<List<string>> GetUserImage(int id);
        Task<bool> UpdateProfile(UserProfile userProfile);
    }
}
