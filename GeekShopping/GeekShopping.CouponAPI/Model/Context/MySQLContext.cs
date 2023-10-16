using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Model.Context
{
    public class MySQLContext : DbContext ///construtor
    {        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {} //<<-- objeto do qual estende do dbcontext
        public DbSet<Coupon> Coupons { get; set; } //adicionado de produtos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "ERUDIO_2022_10",
                DiscountAmount = 10
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "ERUDIO_2022_15",
                DiscountAmount = 15
            });
        }




    }
}
