using FootballLeagueMvcProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class TableViewModel
    {
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int TotalGoals { get; set; }
        public int TotalMisses { get; set; }
        public int Points { get; set; }
        public IEnumerable<LastMatchViewModel> LastFive { get; set; }
    }
}
