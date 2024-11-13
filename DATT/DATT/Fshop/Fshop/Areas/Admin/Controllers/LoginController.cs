using Fshop.Areas.Admin.Models;
using Fshop.Models;
using Fshop.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Fshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DataContext _context;
        public LoginController(DataContext context)
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
            if (!IsValidEmail(user.Email))
            {
                Functions._Message = "Định dạng email không hợp lệ";
                return RedirectToAction("Index", "Login");
            }
            if (user == null)
            {
                return NotFound();
            }
            var kt = _context.AdminUsers.FirstOrDefault(m => m.Email == user.Email && m.IsActive == false);
            if (kt != null)
            {
                Functions._Message = "Tài khoản bị vô hiệu hóa";
                return RedirectToAction("Index", "Login");
            }
            string pw = Functions.MD5Password(user.Password);
            var check = _context.AdminUsers.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Sai tài khoản, vui lòng nhập lại";
                return RedirectToAction("Index", "Login");
            }

            Functions._Message = string.Empty;
            Functions._UserID = check.UserID;
            Functions._UserName = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return RedirectToAction("Index", "Home");

            
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);

        }

        
    }
}
