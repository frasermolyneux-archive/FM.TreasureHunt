using System;

namespace FM.TreasureHunt.Web.Models
{
    public class Treasure
    {
        public Guid TreasureId { get; set; }
        public string FriendlyName { get; set; }
        public int FoundCount { get; set; }
        public DateTime? LastFound { get; set; }
    }
}