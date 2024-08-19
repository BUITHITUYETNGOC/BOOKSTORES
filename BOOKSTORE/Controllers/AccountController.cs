using BOOKSTORE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BOOKSTORE.Models;


public class AccountController : Controller
{
    private readonly QlbhContext _context;

    public AccountController(QlbhContext context)
    {
        _context = context;

    }

    // Register page
    public IActionResult Register()
    {
      
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == model.Name);
            if (existingAccount == null)
            {
                var account = new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Account already exists.");
        }
        return View(model);
    }

    // Login page
    public IActionResult Login()
    {
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == model.Name);
            if (account != null)
            {
                // Perform login actions
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }
    //
    [HttpGet]
    public IActionResult Index()
    {
        
        return View();
    }
}
