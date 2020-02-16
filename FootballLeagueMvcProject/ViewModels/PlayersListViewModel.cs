using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class PlayersListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int AtNumber { get; set; }
        public string Team { get; set; }
        public int TeamId { get; set; }
        public string Photo { get; set; }

    }
}
