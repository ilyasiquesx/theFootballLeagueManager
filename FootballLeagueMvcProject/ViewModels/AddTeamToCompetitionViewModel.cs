using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class AddTeamToCompetitionViewModel
    {
        public int CompetitionId { get; set; }
        public IEnumerable<TeamViewModel> TeamViewModels { get; set; }
        
        [Required]
        public int TeamId { get; set; }

    }
}
