using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using FootballLeagueMvcProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Controllers
{
    public class ChampionshipController : Controller
    {
        private readonly IChampionshipRepository _championshipRepository;
        public ChampionshipController(IChampionshipRepository championshipRepository)
        {
            _championshipRepository = championshipRepository;
        }
        public IActionResult Index()
        {
            var listOfCompetitions = _championshipRepository.GetChampionshipsName().Select(c => new ChampionshipViewModel
            {
                Name = c.Name,
                Id = c.Id
            });

            return View(listOfCompetitions);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateCompetitionViewModel ccvm)
        {
            if (ModelState.IsValid)
            {
                var newChampionship = new Championship
                {
                    Name = ccvm.Name
                };

                _championshipRepository.CreateChampionship(newChampionship);
            }

            return RedirectToAction("Index", "Championship");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var championship = _championshipRepository.GetChampionshipByIdOptimized(id);
            _championshipRepository.DeleteChampionship(championship);
            return RedirectToAction("Index");
        }
    }
}
