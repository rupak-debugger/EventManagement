using EventManagement.Areas.Identity.Data;
using EventManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [Authorize(Roles = "admin")]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public AccountsController(ApplicationDbContext context, RoleManager<IdentityRole> rolemanager)
        {
            _context = context;
            _rolemanager = rolemanager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAppRoles()
        {
            var roles = _rolemanager.Roles;
            return View(roles);
        }
        public IActionResult GetAllUsers()
        {
            var userwithroles = (from user in _context.Users
                                 join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                 join role in _context.Roles on userRole.RoleId equals role.Id
                                 select new ApplicationUserViewModel()
                                 {
                                     Email = user.Email,
                                     FullName = user.FullName,
                                     Role = role.Name
                                 }
                                ).ToList();

            //return View(_context.Users.ToList());
            return View(userwithroles);
        }
    }
}
