using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.IdentityServer.Model.Context
{
    public class MySQLContext : IdentityDbContext<ApplicationUser> ///construtor
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { } //<<-- objeto do qual estende do dbcontext
 

    }
}
