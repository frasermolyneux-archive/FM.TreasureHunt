using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FM.TreasureHunt.Web.Data;
using FM.TreasureHunt.Web.Dto;
using FM.TreasureHunt.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FM.TreasureHunt.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Treasures = await _context.Treasures.OrderByDescending(t => t.FoundCount).ToListAsync(),
                UserScoreDtos = new List<UserScoreDto>()
            };

            var users = await _context.Users.ToListAsync();
            foreach (var identityUser in users)
            {
                var score = await _context.TreasureFinds.Where(tf => tf.User == identityUser).CountAsync();
                model.UserScoreDtos.Add(new UserScoreDto
                {
                    Username = identityUser.UserName,
                    Score = score
                });
            }

            model.UserScoreDtos = model.UserScoreDtos.OrderByDescending(us => us.Score).ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}