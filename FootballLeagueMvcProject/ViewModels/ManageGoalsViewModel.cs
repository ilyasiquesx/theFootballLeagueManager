using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class ManageGoalsViewModel
    {
        public int FixtureId { get; set; }
        public int HomeTeamScore { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamCards { get; set; }
        public int AwayTeamScore { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamCards { get; set; }
        public IEnumerable<PlayersListViewModel> PlayersListViewModelsHomeTeam { get; set; }
        public IEnumerable<PlayersListViewModel> PlayersListViewModelsAwayTeam { get; set; }
        public IList<string> CardTypes { get; set; }
        public IList<GoalViewModel> GoalsViewModels { get; set; }
        public IList<CardViewModel> CardViewModel { get; set; }
    }
}
