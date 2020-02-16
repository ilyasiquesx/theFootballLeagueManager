using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class ManageFixtureViewModel
    {
        public int FixtureId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        public int HomeTeamScores { get; set; }
        public int AwayTeamScores { get; set; }

        public int HomeTeamCards { get; set; }
        public int AwayTeamCards { get; set; }
        public bool IsTechDefeat { get; set; }
        public int TechDefeatedTeamId { get; set; }
        public IEnumerable<TeamViewModel> TeamViewModels { get; set; }
    }
}
