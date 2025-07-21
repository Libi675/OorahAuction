using ApiSale.Models;

namespace ApiSale.BL
{
    public interface IAuthorizeService
    {
        public Task RegisterUser(User user);
        public Task<string> GenerateToken(LoginModel user);
        public Task<bool> ValidateUser(string email, string password);

        public Task<string> GetUserRole(string username);
        public Task<string> GetRolebyToken(int userId);


    }
}
