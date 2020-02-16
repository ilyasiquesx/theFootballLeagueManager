using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class EditPlayerViewModel
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public int AtNumber { get; set; }
        public string Role { get; set; }
        public int TeamId { get; set; }
        public IEnumerable<TeamViewModel> TeamViewModels { get; set; }
    }
}
