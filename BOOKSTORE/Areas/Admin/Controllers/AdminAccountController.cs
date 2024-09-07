using BOOKSTORE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BOOKSTORE.Models;
using System.Security.Cryptography;
using System.Text;

namespace BOOKSTORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly QlbhContext _context;

        public AdminAccountController(QlbhContext context)
        {
            _context = context;
        }
        
        [Route("Admin/AdminAccount/")]
        [Route("Admin/AdminAccount/Register/")]
        [Route("Admin/AdminAccount/Login/")]
        [Route("Admin/AdminAccount/Index/")]
        [Route("Admin/AdminAccount/ManageAccounts/")]


        // Hiển thị danh sách tài khoản
        public async Task<IActionResult> ManageAccounts()
        {
            var accounts = await _context.Accounts.Select(a => new AccountViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Role = a.Role
            }).ToListAsync();

            return View(accounts);
        }

        [Route("Admin/AdminAccount/ManageAccounts/5")]

        // Xoá tài khoản
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ManageAccounts");
        }
        [Route("Admin/AdminAccount/EditRole/5")]

        // Sửa vai trò của tài khoản
        [HttpPost]
        public async Task<IActionResult> EditRole(string id, string newRole)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                account.Role = newRole;
                _context.Update(account);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ManageAccounts");
        }



        [Route("Admin/AdminAccount/Index/5")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        

        // Helper methods for hashing and verifying passwords
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashOfInput = HashPassword(inputPassword);
            return hashOfInput.Equals(storedHash);
        }

        
        // Register page
        public IActionResult Register()
        {
            return View();
        }
        [Route("Admin/AdminAccount/Register/5")]

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == model.Email);
                if (existingAccount == null)
                {
                    // Hash the password before storing
                    string hashedPassword = HashPassword(model.Password);

                    var account = new Account
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = model.Email,
                        Password = hashedPassword, // Store hashed password
                        Role = "User" // Default role
                    };
                    _context.Accounts.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Tài Khoản Đã Tồn Tại.");
            }
            return View(model);
        }

      

        // Login page
        public IActionResult Login()
        {
            return View();
        }
        [Route("Admin/AdminAccount/Login/5")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == model.Email);

                if (account != null)
                {
                    Console.WriteLine($"Stored password (hashed): {account.Password}");
                    Console.WriteLine($"Input password (raw): {model.Password}");

                    // Verify the password using the hash
                    if (VerifyPassword(model.Password, account.Password))
                    {
                        // Kiểm tra vai trò của tài khoản
                        if (account.Role == "Admin")
                        {
                            // Chuyển hướng đến trang Admin nếu là Admin
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (account.Role == "User")
                        {
                            // Chuyển hướng đến trang Home nếu là User
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Xử lý nếu vai trò không rõ ràng
                            ModelState.AddModelError("", "Vai trò của tài khoản không hợp lệ.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
            }
            return View(model);
        }




    }

}
