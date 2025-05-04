using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Library.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController(IUserAccount accountInterface) : ControllerBase
    {
        
        [AllowAnonymous,HttpPost("register")]
        public async Task<IActionResult> CreateAsync (Register user)
        {
            if (user == null) return BadRequest("Model is Empty");
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);
        }
        [AllowAnonymous,HttpPost("login")]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null) return BadRequest("Model is Empty");
            var result = await accountInterface.SignInAsync(user);
            return Ok(result);
        }
        [AllowAnonymous,HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if(token == null) return BadRequest("Model is Empty");
            var result = await accountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }
        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            var users = await accountInterface.GetUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }
        [HttpGet("roles")]
        public async Task<IActionResult> Roles()
        {
            var users = await accountInterface.GetRoles();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ManageUser user)
        {
            if (user == null) return BadRequest("Model is Empty");
            var result = await accountInterface.UpdateUser(user);
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id");
            var result = await accountInterface.DeleteUser(id);
            return Ok(result);
        }
        







    }
}
