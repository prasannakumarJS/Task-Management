using Authorization.Data.Enum;
using Authorization.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.IDAM.Entities;
using TaskManagement.IDAM.Interfaces;
using TaskManagement.IDAM.Models;

namespace TaskManagement.IDAM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private ITokenGeneratorService tokenGenratorService;
        private readonly IUserRepository userRepository;
        private readonly IUserCreationProxy userCreationProxy;
        public UserController(ITokenGeneratorService tokenGenratorService, IUserRepository userRepository, IUserCreationProxy userCreationProxy)
        {
            this.tokenGenratorService = tokenGenratorService;
            this.userRepository = userRepository;
            this.userCreationProxy = userCreationProxy;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var userExists = await userRepository.UserExistsAsync(user.UserName);
            if (userExists)
            {
                return BadRequest(new { error = "Username already exists" });
            }

            if (!Enum.IsDefined(typeof(RoleType), user.RoleType))
            {
                return BadRequest(new { error = "Invalid role" });
            }

            var createdUser = await userRepository.CreateAsync(user);
            await userCreationProxy.CreateUser(createdUser, HttpContext);
            return Ok("User registered successfully" );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest userlogin)
        {
            var token = await tokenGenratorService.GenerateToken(userlogin.UserName, userlogin.Password);
            if (token is null)
            {
                return BadRequest("Invalid creds");
            }
            var user = await userRepository.GetUserWithUserNameAsync(userlogin.UserName);

            return Ok(new UserLoginResponse() { UserId = user.Id, Token = token});
        }
    }
}
