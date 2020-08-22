using System;
using System.Linq;
using System.Threading.Tasks;
using FM.TreasureHunt.Web.Data;
using FM.TreasureHunt.Web.Models;
using FM.TreasureHunt.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FM.TreasureHunt.Web.Controllers
{
    [Authorize]
    public class TreasureHuntController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TreasureHuntController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var treasureFinds = await _context.TreasureFinds.Include(tf => tf.Treasure)
                .Where(tf => tf.User == loggedInUser).ToListAsync();

            return View(treasureFinds);
        }

        [HttpGet]
        public IActionResult RegisterFind(string id)
        {
            return View(new RegisterFindViewModel
            {
                ReferenceNumber = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFind(RegisterFindViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!Guid.TryParse(model.ReferenceNumber, out var treasureId))
            {
                ModelState.AddModelError(nameof(model.ReferenceNumber),
                    "Treasure reference number was not in the correct format!");
                return View(model);
            }

            var treasure = await _context.Treasures.SingleOrDefaultAsync(t => t.TreasureId == treasureId);

            if (treasure == null)
            {
                ModelState.AddModelError(nameof(model.ReferenceNumber), "Nice try! That treasure doesn't exist!");
                return View(model);
            }

            var loggedInUser = await _userManager.GetUserAsync(User);

            if (_context.TreasureFinds.Any(tf => tf.Treasure == treasure && tf.User == loggedInUser))
            {
                ModelState.AddModelError(nameof(model.ReferenceNumber), "Nice try! You have already claimed that treasure!");
                return View(model);
            }

            var treasureFind = new TreasureFind
            {
                Treasure = treasure,
                User = loggedInUser,
                DateFound = DateTime.UtcNow
            };

            await _context.TreasureFinds.AddAsync(treasureFind);

            treasure.FoundCount++;
            treasure.LastFound = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}