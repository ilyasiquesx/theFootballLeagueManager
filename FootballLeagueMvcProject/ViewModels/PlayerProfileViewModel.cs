using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class PlayerProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public string TeamLogo { get; set; }
        public string Photo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Ages { get; set; }
        public int AtNumber { get; set; }
        public string Role { get; set; }
    }
}
