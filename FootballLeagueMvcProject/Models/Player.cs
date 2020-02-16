using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public string Photo { get; set; }
        public string Role { get; set; }
        public int AtNumber { get; set; }



    }
}
