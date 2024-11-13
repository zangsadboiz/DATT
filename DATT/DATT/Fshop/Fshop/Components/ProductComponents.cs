using Fshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fshop.Components
{
	[ViewComponent(Name = "Product")]
	public class ProductComponents : ViewComponent
	{
		private readonly DataContext _context;
		public ProductComponents(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var list = (from m in _context.Products
						where (m.IsActive == true)
						select m).ToList();
			return View("Default", list);
		}
	}
}
