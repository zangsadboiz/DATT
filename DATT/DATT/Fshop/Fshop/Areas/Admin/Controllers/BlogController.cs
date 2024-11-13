using Fshop.Models;
using Fshop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly DataContext _context;
        public BlogController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var postList = _context.Posts.OrderBy(m => m.PostID).ToList();
            return View(postList);
        }
        public IActionResult Create()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var postList = (from m in _context.Menus
                            select new SelectListItem()
                            {
                                Text = m.MenuName,
                                Value = m.MenuID.ToString(),
                            }).ToList();
            postList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.postList = postList;
            return View();
        }

        [HttpPost]

        public IActionResult Create(Post post)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Add(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(long? id)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            var postList = (from m in _context.Posts
                            select new SelectListItem()
                            {
                                Text = m.Title,
                                Value = m.PostID.ToString(),
                            }).ToList();
            postList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.postList = postList;
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Posts.Update(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public IActionResult Delete(long? id)
        {

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Posts.Find(id);
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

            var delePost = _context.Posts.Find(id);
            if (delePost == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(delePost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}