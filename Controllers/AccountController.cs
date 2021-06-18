using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Detailed.DTO;

namespace RestAPI_Detailed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login){
            var user = await userManager.FindByEmailAsync(login.Email);
            if(user ==null){
                return Unauthorized();
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, login.Password,false);
           if(result.Succeeded){
               return new UserDTO{
                   DisplayName = user.DispalyName,
                   Image=null,
                   Token = "This is a new token",
                   UserName = user.UserName
               };
           }

           return Unauthorized();
        }
        
    }
}