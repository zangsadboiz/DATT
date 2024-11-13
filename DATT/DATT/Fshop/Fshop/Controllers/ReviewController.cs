using Fshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fshop.Controllers
{
	public class ReviewController : Controller
	{
		public IActionResult Index()
		{
			var rv = _context.Reviews.ToList();
			return View(rv);
		}
		[Route("/list-{slug}-{id:int}.html", Name = "List")]
		public IActionResult List(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var list = _context.Reviews.Where(m => (m.ReviewID == id) && (m.IsActive == true)).Take(6).ToList();
			if (list == null)
			{
				return NotFound();
			}

			return View(list);
		}
		private readonly ILogger<HomeController> _logger;
		private readonly DataContext _context;

		public ReviewController(ILogger<HomeController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}
	}
}
