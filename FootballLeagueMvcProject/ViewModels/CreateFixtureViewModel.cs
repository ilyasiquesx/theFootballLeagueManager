using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class CreateFixtureViewModel
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public int CompetitionId { get; set; }
        public DateTime AppointedTime { get; set; }
        public IEnumerable<TeamViewModel> TeamViewModels { get; set; }

    }
}
