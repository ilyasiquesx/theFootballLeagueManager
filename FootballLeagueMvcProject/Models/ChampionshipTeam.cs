using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class ChampionshipTeam
    {
        public Championship Championship { get; set; }
        public Team Team { get; set; }
        public int ChampionshipId { get; set; }
        public int TeamId { get; set; }
    }
}
