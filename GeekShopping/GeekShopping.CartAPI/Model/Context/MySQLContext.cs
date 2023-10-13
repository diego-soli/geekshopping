using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class MySQLContext : DbContext ///construtor
    {        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {} //<<-- objeto do qual estende do dbcontext
        public DbSet<Product> Products { get; set; } //adicionado de produtos

        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }


    }
}
