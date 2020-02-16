using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.ViewModels
{
    public class CreatePlayerViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int AtNumber { get; set; }
        public List<string> Roles { get; set; }
        public IFormFile Photo { get; set; }
    }
}
