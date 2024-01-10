using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Model.Context
{
    public class MySQLContext : DbContext 
        
        ///construtor
    {        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {} //<<-- objeto do qual estende do dbcontext
        
        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<OrderHeader> Headers { get; set; }


    }
}
