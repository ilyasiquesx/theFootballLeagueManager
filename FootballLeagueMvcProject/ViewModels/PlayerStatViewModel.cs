using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class PlayerStatViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerFullName { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerTeamName { get; set; }
        public int CountOfGoals { get; set; }
        public int CountOfAssists { get; set; }
        public int SumOfGoalsAndAssists { get; set; }

    }
}
