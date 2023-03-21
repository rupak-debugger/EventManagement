using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagement.Areas.Identity.Data;
using EventManagement.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Controllers
{
    public class EventInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventInformationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        // GET: EventInformations
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("manager"))
            {
                return RedirectToAction(nameof(ManagerIndex));
            }
            var eventinfos = _context.EventInformations.Include(e => e.EventCategories).Include(e => e.User).Include(e => e.Venues);
            return View(await eventinfos.ToListAsync());
        }

        //POST: For search by attendee number
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            Int32 key = Convert.ToInt32(form["Key"].ToString());
            var eventinfos = _context.EventInformations.Where(e => e.AttendeeNumber == key).Include(e => e.EventCategories).Include(e => e.User).Include(e => e.Venues);
            return View(eventinfos);
        }

        // GET: EventInformations/Details/5
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventInformations == null)
            {
                return NotFound();
            }

            var eventInformation = await _context.EventInformations
                .Include(e => e.EventCategories)
                .Include(e => e.User)
                .Include(e => e.Venues)
                .FirstOrDefaultAsync(m => m.EventInfoId == id);
            if (eventInformation == null)
            {
                return NotFound();
            }

            return View(eventInformation);
        }

        // GET: EventInformations/Create
        [Authorize(Roles = "admin, manager")]
        public IActionResult Create()
        {
            var userwithroles = (from user in _context.Users
                                 join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                 join role in _context.Roles on userRole.RoleId equals role.Id
                                 where role.Name == "Manager"
                                 select user);
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryName");
            ViewData["UserId"] = new SelectList(userwithroles, "Id", "FullName");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        // POST: EventInformations/Create
        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventInfoId,EventTime,AttendeeNumber,UserId,EventCategoryId,VenueId,BookedOn")] EventInformation eventInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventInformation);
                await _context.SaveChangesAsync();
                if(User.IsInRole("admin"))
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(ManagerIndex));
            }
            var userwithroles = (from user in _context.Users
                                 join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                 join role in _context.Roles on userRole.RoleId equals role.Id
                                 where role.Name == "Manager"
                                 select user);
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryName", eventInformation.EventCategoryId);
            ViewData["UserId"] = new SelectList(userwithroles, "Id", "FullName", eventInformation.UserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", eventInformation.VenueId);
            return View(eventInformation);
        }

        // GET: EventInformations/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventInformations == null)
            {
                return NotFound();
            }

            var eventInformation = await _context.EventInformations.FindAsync(id);
            if (eventInformation == null)
            {
                return NotFound();
            }
            var userwithroles = (from user in _context.Users
                                 join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                 join role in _context.Roles on userRole.RoleId equals role.Id
                                 where role.Name == "Manager"
                                 select user);
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryName", eventInformation.EventCategoryId);
            ViewData["UserId"] = new SelectList(userwithroles, "Id", "FullName", eventInformation.UserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", eventInformation.VenueId);
            return View(eventInformation);
        }

        // POST: EventInformations/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventInfoId,EventTime,AttendeeNumber,UserId,EventCategoryId,VenueId,BookedOn")] EventInformation eventInformation)
        {
            if (id != eventInformation.EventInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventInformationExists(eventInformation.EventInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var userwithroles = (from user in _context.Users
                                 join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                 join role in _context.Roles on userRole.RoleId equals role.Id
                                 where role.Name == "Manager"
                                 select user);
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryName", eventInformation.EventCategoryId);
            ViewData["UserId"] = new SelectList(userwithroles, "Id", "FullName", eventInformation.UserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", eventInformation.VenueId);
            return View(eventInformation);
        }

        // GET: EventInformations/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventInformations == null)
            {
                return NotFound();
            }

            var eventInformation = await _context.EventInformations
                .Include(e => e.EventCategories)
                .Include(e => e.User)
                .Include(e => e.Venues)
                .FirstOrDefaultAsync(m => m.EventInfoId == id);
            if (eventInformation == null)
            {
                return NotFound();
            }

            return View(eventInformation);
        }

        // POST: EventInformations/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventInformations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventInformations'  is null.");
            }
            var eventInformation = await _context.EventInformations.FindAsync(id);
            if (eventInformation != null)
            {
                _context.EventInformations.Remove(eventInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET: Events scheduled today
        [Authorize(Roles = "admin")]
        public IActionResult EventToday()
        {
            var currentDate = DateTime.Now;
            var eventinfos = _context.EventInformations.Where(e => e.EventTime.Date == currentDate.Date).Include(e => e.EventCategories).Include(e => e.User).Include(e => e.Venues);
            return View(eventinfos);
        }

        private bool EventInformationExists(int id)
        {
          return _context.EventInformations.Any(e => e.EventInfoId == id);
        }
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> ManagerIndex()
        {
            string userid = await GetCurrentUserId();
            var eventinfos = _context.EventInformations.Where(e => e.UserId == userid).Include(e => e.EventCategories).Include(e => e.User).Include(e => e.Venues);
            return View(await eventinfos.ToListAsync());
        }

        private async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr.Id;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
