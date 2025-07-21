using ApiSale.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSale.DAL
{
    public class ChainaSaleDBContext: DbContext
    {
        public ChainaSaleDBContext(DbContextOptions<ChainaSaleDBContext> options) : base(options)
        {

        }
        public DbSet<Order> Order { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Gift> Gift { get; set; }
        public DbSet<Donor> Donor { get; set; }
        public DbSet<Winner> Winner { get; set; }
        public DbSet<Categorya> Categorya { get; set; }

        public DbSet<LoginModel> LoginModel { get; set; }
        //public DbSet<User> User { get; set; }
    }
}
