using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIDotNet.DTO;
using WebAPIDotNet.Model;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        public AuthController(UserManager<ApplicationUser> userManger, IConfiguration config)
        {
            _userManager = userManger;
            _config = config;
        }

        [HttpPost("Register")]//post --> هبعت داتا التسجيل
        public async Task<IActionResult> Register(RegisterDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                //SaveDB
                ApplicationUser user = new ApplicationUser();
                user.UserName=UserFromRequest.UserName;
                user.Email = UserFromRequest.Email;
                IdentityResult result=  await _userManager.CreateAsync(user,UserFromRequest.Password);//put pass to hash  take care y must awit to end
                if (result.Succeeded)
                {
                    return Ok("Created");
                }

                foreach (var item in result.Errors) 
                {
                    ModelState.AddModelError("Password", item.Description);
                }
                
            }
            return BadRequest(ModelState); //400 error and Return in requestbady modelstate 
        
        }
        [HttpPost("Login")]//لان هبعت email and passw
        public async Task< IActionResult> Login(LoginDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                //check user
                ApplicationUser userInDB = await _userManager.FindByNameAsync(UserFromRequest.UserName);
                if (userInDB == null)
                {
                    return Unauthorized("Invalid Username or Password");
                }
                //check pass
                bool found=await _userManager.CheckPasswordAsync(userInDB,UserFromRequest.Password);
                if (!found)
                {
                    return Unauthorized("Invalid Username or Password");
                     
                }

                //generate token and return it 
                List<Claim> UserClaims = new List<Claim>();
                //Token Genrated id change (JWT Predefind Claims JIT)//Uniqe token
                UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                UserClaims.Add(new Claim(ClaimTypes.NameIdentifier,userInDB.Id));
                UserClaims.Add(new Claim(ClaimTypes.Name,userInDB.UserName));


                var UserRoles= await _userManager.GetRolesAsync(userInDB);
                foreach(var roleName in UserRoles)
                {
                    UserClaims.Add(new Claim(ClaimTypes.Role, roleName));

                }

                //sign
                SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                SigningCredentials signingCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);//alg to encode  +key to create signture 


                //design token
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    expires:DateTime.Now.AddHours(1),
                    claims: UserClaims,
                    signingCredentials: signingCredentials//trust and verify

                    );

                //generate token in response

                return Ok(new
                {
                   Token= new JwtSecurityTokenHandler().WriteToken(token),
                   expiration=token.ValidTo,
                });

            }
            //if not return bad request
            return BadRequest(ModelState);
        }
    }
}
