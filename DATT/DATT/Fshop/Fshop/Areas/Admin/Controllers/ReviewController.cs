using Fshop.Models;
using Fshop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly DataContext _context;
        public ReviewController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reviewList = _context.Reviews.OrderBy(m => m.ReviewID).ToList();
            return View(reviewList);
        }

        public IActionResult Create()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var reviewList = (from m in _context.Reviews
                            select new SelectListItem()
                            {
                                Text = m.Author,
                                Value = m.ReviewID.ToString(),
                            }).ToList();
            reviewList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.reviewList = reviewList;
            return View();
        }
        [HttpPost]

        public IActionResult Create(Review rv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rv);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var rv = _context.Reviews.Find(id);
            if (rv == null)
            {
                return NotFound();
            }
            var rvList = (from m in _context.Reviews
                            select new SelectListItem()
                            {
                                Text = m.Title,
                                Value = m.ReviewID.ToString(),
                            }).ToList();
            rvList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.rvList = rvList;
            return View(rv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Review rv)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Reviews.Update(rv);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rv);
        }
        public IActionResult Delete(int? id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Reviews.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        // 
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var delerv = _context.Reviews.Find(id);
            if (delerv == null)
            {
                return NotFound();
            }
            _context.Reviews.Remove(delerv);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}