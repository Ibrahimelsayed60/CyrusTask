using CyrusTask.DTOs.Users;
using CyrusTask.Extensions.UserDtos;
using CyrusTask.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CyrusTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var userDto = await _userService.FindUserByEmailAsync(registerDto.Email);

            if (userDto is not null)
                return BadRequest("THis Email is already exist");

            var userAddedDto = await _userService.CreateUserAsync(userDto);

            userAddedDto.Token = _userService.CreateToken(userAddedDto.ToModel());

            Response.Cookies.Append("access_token", userAddedDto.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(userAddedDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.FindUserByEmailAsync(loginDto.Email);

            if (user is null || !await _userService.CheckUserPasswordAsync(user, loginDto.Password))
            {
                return BadRequest ("Email or Password is incorrect");
            }

            user.Token = _userService.CreateToken(user.ToModel());

            Response.Cookies.Append("access_token", user.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(user);

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok( await _userService.GetUsers());
        }

    }
}
