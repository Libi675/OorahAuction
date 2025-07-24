using ApiSale.DAL;
using Microsoft.EntityFrameworkCore;

namespace ApiSale.Middleware
{
    public static class DatabaseConnectionMiddleware
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
           
            var connectionString = configuration.GetConnectionString("DefaultConnection");

           
            services.AddDbContext<ChainaSaleDBContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
