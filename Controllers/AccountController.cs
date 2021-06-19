using System.Security.Claims;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI_Detailed.DTO;

namespace RestAPI_Detailed.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            this._tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded)
            {
                return CreateUser(user);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (await userManager.Users.AnyAsync(x => x.Email == register.Email))
            {
                return BadRequest("User already exists!!");
            }
            if (await userManager.Users.AnyAsync(x => x.UserName == register.UserName))
            {
                return BadRequest("User name taken!!");
            }

            var user = new AppUser
            {
                DisplayName = register.UserName,
                Email = register.Email,
                UserName = register.UserName
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                return CreateUser(user);
            }
            return BadRequest("Problem creating user");

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {

            var user = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return CreateUser(user);

        }

        private UserDTO CreateUser(AppUser user)
        {
            return new UserDTO
            {
                DisplayName = user.DisplayName,
                Image = null,
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}