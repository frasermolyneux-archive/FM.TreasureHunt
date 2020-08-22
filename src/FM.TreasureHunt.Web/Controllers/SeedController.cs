using System;
using System.Linq;
using System.Threading.Tasks;
using FM.TreasureHunt.Web.Data;
using FM.TreasureHunt.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FM.TreasureHunt.Web.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> SeedTreasure()
        {
            for (var i = 0; i < 30; i++)
            {
                var treasure = new Treasure
                {
                    FriendlyName = $"Poster {i}"
                };

                if (!_context.Treasures.Any(t => t.FriendlyName == treasure.FriendlyName))
                    await _context.Treasures.AddAsync(treasure);
            }

            await _context.SaveChangesAsync();
            return Json(true);
        }
    }
}