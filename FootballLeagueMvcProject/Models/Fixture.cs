using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class Fixture
    {
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
        public IList<Goal> Goals { get; set; }
        public IList<Card> Cards { get; set; }
        public int? ChampionshipId { get; set; }
        public Championship Championship { get; set; }
        public DateTime AppointedTime { get; set; }
        public bool IsTechDefeat { get; set; }
        public int TechDefeatedTeamId { get; set; }
    }
}
