using Fshop.Areas.Admin.Models;
using Fshop.Models;
using Fshop.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Fshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly DataContext _context;
        public RegisterController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AdminUser user)
        {
            if (user == null)
            {
                return NotFound();
            }
            if (!IsValidEmail(user.Email))
            {
                Functions._Message = "Định dạng email không hợp lệ";
                return RedirectToAction("Index", "Register");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                ModelState.AddModelError("Email", "Vui lòng nhập địa chỉ email!");
                return View(user); // Trả về View với thông báo lỗi
            }

            var check = _context.AdminUsers.FirstOrDefault(m => m.Email == user.Email);
            if (check != null)
            {
                Functions._MessageEmail = "Email trùng lặp!";
                return RedirectToAction("Index", "Register");
            }
            Functions._MessageEmail = string.Empty;
            user.Password = Functions.MD5Password(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);

        }

    }
}
