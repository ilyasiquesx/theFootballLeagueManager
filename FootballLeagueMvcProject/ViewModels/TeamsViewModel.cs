using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class TeamsViewModel
    {
        public int ChampionshipId { get; set; }
        public IEnumerable<TeamViewModel> TeamViewModels { get; set; }

    }
}
