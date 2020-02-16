using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int? FixtureId { get; set; }
        public Fixture Fixture { get; set; }
        public int? PlayerId { get; set; }
        public Player Player { get; set; }
        public string Type { get; set; }
        public int AtMinute { get; set; }
    }
}
