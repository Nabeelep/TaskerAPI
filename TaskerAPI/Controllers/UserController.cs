using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskerAPI.Model;
using TaskerAPI.Services;

namespace TaskerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<string> Register(string FirstName, string LastName, string Email, string Password)
        {
            try
            {
                var Response = await _userService.RegisterAsync(FirstName, LastName, Email, Password);
                if (Response != null)
                    return Response.ToString();
                else
                    return "Failed to register user";
            }
            catch
            {
                return "Failed to register user";
            }
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<string> Login(string Email,string Password)
        {
            try
            {
                var token = await _userService.LoginAsync(Email, Password);
                return token;
            }
            catch
            {
                return "notuser";
            }
        }
        [AllowAnonymous]
        [HttpGet("Users")]
        public async Task<ActionResult<List<Users>>> GetUsers(int ID)
        {
            try
            {
                var users = await _userService.GetUsersAsync(ID);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpGet("UpdateUserdata")]
        public async Task<string> UpdateUserdata(int ID, string firstname, string lastname, string email, string password)
        {
            try
            {
                var users = await _userService.UpdateUserdata(ID, firstname, lastname, email, password);
                if (users != null) return "OK";
                else return null;
            }
            catch (Exception ex)
            {
                return "Internal server error";
            }
        }
    }
}
