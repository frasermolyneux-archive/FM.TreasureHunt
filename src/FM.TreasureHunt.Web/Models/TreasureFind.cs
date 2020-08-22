using System;
using Microsoft.AspNetCore.Identity;

namespace FM.TreasureHunt.Web.Models
{
    public class TreasureFind
    {
        public Guid TreasureFindId { get; set; }
        public Treasure Treasure { get; set; }
        public IdentityUser User { get; set; }
        public DateTime DateFound { get; set; }
    }
}