using Fshop.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Fshop.Models
{
	public class DataContext : DbContext
	{
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<AdminMenu> AdminMenus { get; set; }
		public DbSet<AdminUser> AdminUsers { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<About> Abouts { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category> Categories { get; set; }

		internal void UpdateCartItem(int cartItemId, int quantity)
		{
			throw new NotImplementedException();
		}


	}
}


