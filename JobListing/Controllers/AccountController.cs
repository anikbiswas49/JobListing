using JobListing.Models;
using JobListing.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace JobListing.Controllers
{
    public class AccountController : Controller
    {
        private readonly JobDbContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly HashPasswordUtil _passwordUtil;
        public AccountController(JobDbContext dbContext,ILogger<AccountController> logger,
           HashPasswordUtil passwordUtil )
        {
            _dbContext = dbContext;
            _logger = logger;
            _passwordUtil = passwordUtil;
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Profile()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("LogOut");
            }
            User? user = await _dbContext.Users.FindAsync(Convert.ToInt32(userId));
            if(user == null)
            {
                return RedirectToAction("LogOut");
            }
            var userCompany = await _dbContext.Companies.FirstOrDefaultAsync(x => x.User.Id == user.Id);
            var jobs = await _dbContext.JobInfos.FirstOrDefaultAsync(x=> x.User.Id == user.Id);
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var existUser = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email == user.Email);
            if (existUser == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect email or Password");
                return View();
            }
            if (!_passwordUtil.VerifyPassword(existUser.Password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect email or Password");
                return View();
            }

            var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(existUser.Id)),
                        new Claim(ClaimTypes.Name, existUser.Name),
                        new Claim(ClaimTypes.Role, existUser.UserType ?? "User"),
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            {
                IsPersistent = true
            });
            return RedirectToAction("Index","Home");
        }


        public IActionResult SignUp()
        {
            _logger.LogInformation("SignUp ");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (user == null) return View();

            var existUser = _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (existUser != null)
            {
                ModelState.AddModelError("Email", "Email Taken");
                return View();
            }

            user.Password = _passwordUtil.HashPassword(user.Password);
            user.UserType = "User";
            _logger.LogInformation(JsonConvert.SerializeObject(user));
            _dbContext.Add(user);
           int affectedRow =  await _dbContext.SaveChangesAsync();
            if (affectedRow > 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

    }
}
