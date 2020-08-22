using System.Collections.Generic;
using FM.TreasureHunt.Web.Dto;
using FM.TreasureHunt.Web.Models;

namespace FM.TreasureHunt.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<Treasure> Treasures { get; set; }
        public List<UserScoreDto> UserScoreDtos { get; set; }
    }
}