using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class StatViewModel
    {
        public int ChampionshipId { get; set; }

        public IEnumerable<PlayerStatViewModel> ListOfStrikers { get; set; }

        public IEnumerable<PlayerStatViewModel> ListOfAssistans { get; set; }

        public IEnumerable<PlayerStatViewModel> ListOfUniversals { get; set; }
    }
}
