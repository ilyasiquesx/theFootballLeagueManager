using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class FixtureViewModel
    {
        public int CompetitionId { get; set; }
        public string HomeTeam { get; set; }
        public  int FixtureId { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamLogo { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public bool Status { get; set; }

        public IEnumerable<GoalViewModel> HomeTeamGoals{ get; set; }
        public IEnumerable<GoalViewModel> AwayTeamGoals { get; set; }
        public IEnumerable<CardViewModel> HomeTeamCards { get; set; }
        public IEnumerable<CardViewModel> AwayTeamCards { get; set; }
        public DateTime AppointedTime { get; set; }
        public bool IsTechDefeat { get; set; }
        public int TechDefeatedTeamId { get; set; }
    }
}
