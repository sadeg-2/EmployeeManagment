using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Server.Library.Data;
using Server.Library.Helpers;
using Server.Library.Repositories.Contracts;
using Constants = Server.Library.Helpers.Constants;

namespace Server.Library.Repositories.Implementaions
{
    public class UserAccountRepository(IOptions<JWTSection> config, AppDbContext appDbContext) : IUserAccount
    { 
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) 
                return new GeneralResponse(false, "Model is Empty");
            
            var checkUser = await FindUserByEmail(user.Email!);
            
            if (checkUser is not null) 
                return new GeneralResponse(false, "User Registered Already");
            
            // Save User 
            var applicationUser = await AddToDataBase(new ApplicationUser() { 
                Email = user.Email,
                FullName = user.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            });
            // check, create and assign Role

            var chekAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(
                _ => _.Name!.Equals(Constants.Admin));
            if (chekAdminRole is null) {
                var createAdminRole = await AddToDataBase(new SystemRole() { 
                    Name = Constants.Admin
                });
                await AddToDataBase(new UserRole() { 
                    RoleId = createAdminRole.Id,
                    UserId = applicationUser.Id,
                });
                return new GeneralResponse(true,"Account Created");
            }
            
            var chekUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(
                _ => _.Name!.Equals(Constants.User));
            SystemRole response = new();
            if (chekUserRole is null) {
                response = await AddToDataBase(new SystemRole()
                {
                    Name = Constants.User
                });
                await AddToDataBase(new UserRole()
                {
                    RoleId = response.Id,
                    UserId = applicationUser.Id,
                });
            }
            else {
                await AddToDataBase(new UserRole()
                {
                    RoleId = chekUserRole.Id,
                    UserId = applicationUser.Id,
                });
            }
            return new GeneralResponse(true, "Account Created");      
        }

        public async Task<GeneralResponse> SignInAsync(Login user)
        {
            throw new NotImplementedException();
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) => 
            await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));
        





        private  async Task<T> AddToDataBase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }












    }
}
