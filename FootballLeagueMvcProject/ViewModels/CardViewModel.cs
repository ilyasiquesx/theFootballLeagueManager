using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class CardViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }

        public string Type { get; set; }
        public int AtMinute { get; set; }
    }
}
