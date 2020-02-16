using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class TeamProfileViewModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public IEnumerable<PlayersListViewModel> PlayersListViewModels { get; set; }
        public IEnumerable<FixtureViewModel> FixtureViewModels { get; set; }
        public int TeamId { get; set; }
    }
}
