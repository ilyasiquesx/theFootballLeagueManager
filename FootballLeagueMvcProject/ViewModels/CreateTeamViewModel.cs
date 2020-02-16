using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
