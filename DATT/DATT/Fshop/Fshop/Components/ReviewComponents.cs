using Fshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fshop.Components
{
	[ViewComponent(Name = "Review")]
	public class ReviewComponent : ViewComponent
	{
		private readonly DataContext _context;
		public ReviewComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync() 
		{
			var list = (from m in _context.Reviews
							  where (m.IsActive == true)
							  select m).ToList();
			return View("Default", list); 
		}
	}
}
