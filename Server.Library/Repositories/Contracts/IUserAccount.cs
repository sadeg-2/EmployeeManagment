
using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace Server.Library.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register register);
        Task<GeneralResponse> SignInAsync(Login login);
    }
}
