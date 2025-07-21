using ApiSale.Models;

namespace ApiSale.DAL
{
    public interface IAuthorizeDal
    {
        public Task RegisterUser(User user);
        public Task LoginUser(LoginModel user);       
        public Task<bool> ValidateUser(string username, string password);
        public Task<string> GetUserRole(string username);
        public  Task<int> GetUserIdByEmail(string email);
        public Task<string> GetRolebyToken(int userId);



    }
}
