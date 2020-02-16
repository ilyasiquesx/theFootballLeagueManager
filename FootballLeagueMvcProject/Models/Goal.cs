using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int? FixtureId { get; set; }
        public Fixture Fixture { get; set; }
        public virtual Player Author { get; set; }
        public virtual Player Assist { get; set; }
        public int AtMinute { get; set; }
    }
}
