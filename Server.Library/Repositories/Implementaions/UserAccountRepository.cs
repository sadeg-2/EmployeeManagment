using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user is null) return new LoginResponse(false, "Model is empty");

            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser is null) return new LoginResponse(false, "User Not Found");

            //Verify Password
            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
                return new LoginResponse(false, "Email/Password Not Valid");

            var getUserRole = await FindUserRole(applicationUser.Id);
            if (getUserRole is null) return new LoginResponse(false, "User Role Not Found");
            
            SystemRole? getRoleName = await FindRoleName(getUserRole);
            
            if (getRoleName is null) return new LoginResponse(false, "User Role Not Found");

            string jwtToken = GenerateToken(applicationUser, getRoleName!.Name!);
            string refreshToken = GenerateRefreshToken();

            var findUser = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(
                _=> _.Id == applicationUser.Id);
            if (findUser is not null)
            {
                findUser.Token = refreshToken;
                await appDbContext.SaveChangesAsync();
            }
            else {
                await AddToDataBase(new RefreshTokenInfo() { Token = refreshToken , UserId = applicationUser.Id});
            }

                return new LoginResponse(true, "Login Successfully", jwtToken, refreshToken);

        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null) return new LoginResponse(false, "Model is Empty");
            
            var findToken = await FindToken(token);
            if (findToken is null) return new LoginResponse(false,"Refresh Token is Required");

            var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Id == findToken.UserId);
            if (user is null) return new LoginResponse(false,"Refresh Token Cann't be generated because User not found");
            
            var getUserRole = await FindUserRole(user.Id);
            SystemRole? getRoleName = await FindRoleName(getUserRole);
            string jwtToken = GenerateToken(user, getRoleName!.Name!);
            string refreshToken = GenerateRefreshToken();

            var updatedRefreshToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(
                _ => _.UserId == user.Id
                );
            if (updatedRefreshToken is null)
                return new LoginResponse(false, "Refresh Token could not be generated because user has not signed in");

            updatedRefreshToken.Token = refreshToken; 
            await appDbContext.SaveChangesAsync();

            return new LoginResponse(true,"Token refreshed successfully",jwtToken,refreshToken);
        }

        private async Task<RefreshTokenInfo?> FindToken(RefreshToken token) 
            => await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(
                    _ => _.Token!.Equals(token.Token)
                );
        

        private async Task<SystemRole?> FindRoleName(UserRole getUserRole)=>
             await appDbContext.SystemRoles.
                FirstOrDefaultAsync(_ => _.Id == getUserRole.RoleId);        
        private async Task<UserRole?> FindUserRole(int userId) => 
            await appDbContext.UserRoles.FirstOrDefaultAsync(
                _ => _.UserId == userId); 
        private string GenerateToken(ApplicationUser user, string role) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                                                                            
            var userClaims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(
                issuer:config.Value.Issuer,
                audience:config.Value.Audience,
                claims:userClaims,
                expires:DateTime.Now.AddSeconds(20),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
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
