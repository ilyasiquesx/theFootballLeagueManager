using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class GoalViewModel
    {
        public int AuthorId { get; set; }
        public int AssistId { get; set; }

        public string AuthorName { get; set; }
        public string AssistName { get; set; }
        public int AtMinute { get; set; }
    }
}
