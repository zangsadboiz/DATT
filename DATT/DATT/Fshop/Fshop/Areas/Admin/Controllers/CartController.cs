using Fshop.Models;
using Fshop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CartController : Controller
    {
        private readonly DataContext _context;
        public CartController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");


            var cartList = _context.Carts.OrderBy(m => m.CartID).ToList();
            return View(cartList);
        }

        public IActionResult Edit(long? id)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.Products.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            var prdList = (from m in _context.Products
                           select new SelectListItem()
                           {
                               Text = m.ProductName,
                               Value = m.ProductID.ToString(),
                           }).ToList();
            prdList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.postList = prdList;
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public IActionResult Delete(long? id)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Products.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        // 
        [HttpPost]
        public IActionResult Delete(long id)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var deleprd = _context.Products.Find(id);
            if (deleprd == null)
            {
                return NotFound();
            }
            _context.Products.Remove(deleprd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}