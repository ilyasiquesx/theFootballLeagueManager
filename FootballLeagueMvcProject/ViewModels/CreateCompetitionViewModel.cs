using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class CreateCompetitionViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
