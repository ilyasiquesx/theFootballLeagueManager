using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class ResultsViewModel
    {

        public int ChampionshipId { get; set; }
        public IEnumerable<FixtureViewModel> FixtureViewModels { get; set; }

        public int countOfPages { get; set; }
    }
}
