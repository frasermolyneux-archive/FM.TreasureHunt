using FM.TreasureHunt.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FM.TreasureHunt.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Treasure> Treasures { get; set; }
        public virtual DbSet<TreasureFind> TreasureFinds { get; set; }
    }
}