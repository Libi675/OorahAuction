
using ApiSale.DAL;
using ApiSale.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSale.BL
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAuthorizeDal authorizeDal;

        public AuthorizeService(IAuthorizeDal authorizeDal)
        {
            this.authorizeDal = authorizeDal;
        }


        public Task<bool> ValidateUser(string email, string password)
        {
            return authorizeDal.ValidateUser(email, password);
        }
        public async Task<string> GenerateToken(LoginModel user)
        {
            string username = user.Email;
            int id = await authorizeDal.GetUserIdByEmail(username);
            string role = await authorizeDal.GetUserRole(username);

            // יצירת המפתח הסודי והאישורים
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-long-secret-key-that-contains-more-than-32-characters-here"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // יצירת ה-Claims כולל התפקיד
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim("UserId", id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role) // שילוב התפקיד ב-Claims
        };


            // יצירת הטוקן
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:4200",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task RegisterUser(User user)
        {
          
            await authorizeDal.RegisterUser(user);
        }

        public async Task<string> GetUserRole(string username)
        {
            return await authorizeDal.GetUserRole(username);    
        }

        public async Task<string> GetRolebyToken(int userId)
        {
           return await authorizeDal.GetRolebyToken(userId);
        }
    }


}
