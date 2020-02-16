using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public List<ChampionshipTeam> ChampionshipTeams { get; set; }
        public string Logo { get; set; }
        public Team()
        {
            ChampionshipTeams = new List<ChampionshipTeam>();
        }
    }
}
