using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KingsStoreApi.Model.Entities
{
    public class KingsStoreContext : IdentityDbContext<User>
    {
        public KingsStoreContext(DbContextOptions<KingsStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Cart> Carts{get;  set;}
        public DbSet<CartItem> CartItems{get;  set;}
        public DbSet<Order> Orders{get;  set;}
        public DbSet<OrderItem> OrderItems{get;  set;}
        public DbSet<Discount> Discounts{get;  set;}
    }
}
