using ApiSale.BL;
using ApiSale.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;

namespace ApiSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : Controller
    {
        private readonly IAuthorizeService authorizeService;

        public AuthorizeController(IAuthorizeService authorizeService)
        {
            this.authorizeService = authorizeService;
        }
        [HttpPost("/Register")]
        public async Task<ActionResult> addUser(User user)
        {
            try
            { 
                await authorizeService.RegisterUser(user);
              
                return Ok(user);

            }
            catch(DuplicateNameException ex)
            {
                return BadRequest(new { message = "it is need to be uniq!" });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPost("/Login")]
        public async Task<ActionResult> Login([FromBody] LoginModel user  )
        {
           
              
            if ( await authorizeService.ValidateUser(user.Email, user.Password))
            {
                    
                var token =await authorizeService.GenerateToken(user);
                if (token == null)
                {
                    return BadRequest(new { message = "Token generation failed" });
                }
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // הגנה מפני גישה דרך JavaScript
                    Secure = false,  // דורש חיבור HTTPS
                    SameSite = SameSiteMode.Lax, // הגנה מפני CSRF
                    Expires = DateTime.UtcNow.AddHours(1) // תוקף הקוקי
                };
                Response.Cookies.Append("AuthToken", token, cookieOptions);
                return Ok("text");
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }
        [HttpPost("/Logout")]
        public async Task<IActionResult> Logout()
        {
           // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("AuthToken");
            return Ok(new { message = "Logout successful" });
        }

        ////////////////
        ///
        [HttpGet]

        public IActionResult VerifyToken()
        {
            // שליפת ה-Claim של UserId מהטוקן
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null)
            {
                return BadRequest("UserId is missing in the token.");
            }

            // הצגת ה-UserId
            int userId = int.Parse(userIdClaim.Value);
            return Ok($"UserId found in token: {userId}");
        }
        [HttpGet("/getRoleByToken")]
        public async Task<string> GetUserRoleInToken()
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return "noToken";
            }
            else
            {
                int userId = int.Parse(userIdClaim.Value);
                return await authorizeService.GetRolebyToken(userId);
            }
        }


    }
}

