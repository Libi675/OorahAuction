using ApiSale.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiSale.DAL
{
    public class AuthorizeDal : IAuthorizeDal
    {
        private readonly ChainaSaleDBContext dBContext;

        public AuthorizeDal(ChainaSaleDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async  Task<bool> ValidateUser(string email, string password)
                                                            {
           
           var  user = await dBContext.User.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return false;
            }
            if(user.Password!=password)
                return false;
            return true;   

        }

        public async Task<string> GetUserRole(string email)
        {
            var user= await dBContext.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user.Role == "admin")
                return "Admin";
           else
                return "User";
        }
        public async Task<int> GetUserIdByEmail(string email)
        {
            // לדוגמה: שליפת ID מבסיס הנתונים
            var user = await dBContext.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user.UserId;
        }

        public async Task LoginUser(LoginModel user)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(User user)
        {
            var userA = await dBContext.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userA == null)
            {
                dBContext.User.AddAsync(user);
                await dBContext.SaveChangesAsync();
            }
            else
            {
                throw new DuplicateNameException($"this uemail {user.Email} elready exist!");
            }
           
        }

        public async  Task<string> GetRolebyToken(int userId)
        {
            var userA = await dBContext.User.FirstOrDefaultAsync(u => u.UserId == userId);
            if (userA.Role == "admin")
                return "Admin";
            else
                return "User";

        }
    }
}
