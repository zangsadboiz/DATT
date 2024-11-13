using Fshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Fshop.Controllers
{
	public class CartController : Controller
	{
		[HttpPost]
		private bool checkCart(int productId)
		{
			return _context.Carts.Any(c => c.ProductID == productId);
		}

		public IActionResult AddToCart(int productId)
		{
			if (checkCart(productId))
			{
				return View();
			}

			Product product = _context.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product != null)
			{
				Cart cartItem = new Cart
				{
					ProductID = product.ProductID,
					ProductName = product.ProductName,
					Price = product.Price,
					Discount = product.Discount,
					Images = product.Images
				};

				_context.Carts.Add(cartItem);
				_context.SaveChanges();

				return RedirectToAction("Index", "Cart"); 
			}


			return RedirectToAction("Index", "Shop"); 
		}

        public IActionResult RemoveFromCart(long cartId)
        {
            var cartItem = _context.Carts.FirstOrDefault(c => c.CartID == cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
                return RedirectToAction("Index", "Cart"); 
            }
            else
            {
                return NotFound();
            }
        }

		public IActionResult SaveQuantity(long cartId, int quantity)
		{
			var cartItem = _context.Carts.FirstOrDefault(c => c.CartID == cartId);
			if (cartItem != null)
			{
				cartItem.Quantity = quantity;
				_context.SaveChanges();
				return Ok(); 
			}
			else
			{
				return NotFound();
			}
		}


		public IActionResult Index()
		{
			var cart = _context.Carts.ToList();
			return View(cart);
		}
		[Route("/list-{slug}-{id:int}.html", Name = "List")]
		public IActionResult List(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var list = _context.Carts.Where(m => (m.MenuID == id) && (m.IsActive == true)).Take(6).ToList();
			if (list == null)
			{
				return NotFound();
			}

			return View(list);
		}
		private readonly ILogger<HomeController> _logger;
		private readonly DataContext _context;

		public CartController(ILogger<HomeController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}
	}
}
