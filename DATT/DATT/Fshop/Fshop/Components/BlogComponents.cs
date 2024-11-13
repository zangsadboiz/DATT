using Fshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fshop.Components
{
	[ViewComponent(Name = "Blog")]
	public class BlogComponents : ViewComponent
	{
		private readonly DataContext _context;
		public BlogComponents(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var list = (from m in _context.Posts
						where (m.IsActive == true)
						select m).ToList();
			return View("Default", list);
		}
	}
}
