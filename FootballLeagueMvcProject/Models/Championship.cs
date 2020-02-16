using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class Championship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Fixture> Fixtures { get; set; }

        public List<ChampionshipTeam> ChampionshipTeams { get; set; }

        public Championship()
        {
            ChampionshipTeams = new List<ChampionshipTeam>();
        }
    }
}
