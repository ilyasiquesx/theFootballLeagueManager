using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class TeamViewModel
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public int PlayersCount { get; set; }
        public string Logo { get; set; }
        public int CompetitionId { get; set; }
    }
}
