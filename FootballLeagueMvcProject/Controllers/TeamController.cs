using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using FootballLeagueMvcProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IFixtureRepository _fixtureRepository;
        public TeamController(ITeamRepository teamRepository, IWebHostEnvironment env, IFixtureRepository fixtureRepository)
        {
            _teamRepository = teamRepository;
            _env = env;
            _fixtureRepository = fixtureRepository;
        }
        public IActionResult Index()
        {
            var teams = _teamRepository.GetAllTeams();

            IEnumerable<TeamViewModel> tvms = teams.Select(t => new TeamViewModel

            {
                Name = t.Name,
                TeamId = t.Id,
                PlayersCount = t.Players.Count(),
                Logo = t.Logo
            });

            return View(tvms);
        }

        public IActionResult Profile(int id)
        {
            if (id == 0)

            {
                return RedirectToAction("Index");
            }

            var team = _teamRepository.GetTeamById(id);
            var players = team.Players.Select(p => new PlayersListViewModel
            {

                Id = p.Id,
                Name = p.FirstName,
                LastName = p.LastName,
                Patronymic = p.Patronymic,
                AtNumber = p.AtNumber,
                Photo = p.Photo

            });

            var fixtures = _fixtureRepository.GetFixturesForTeamOptimized(team.Id).Select(f => new FixtureViewModel
            {

                HomeTeam = f.HomeTeam.Name,
                AwayTeam = f.AwayTeam.Name,
                FixtureId = f.Id

            });

            TeamProfileViewModel tpvm = new TeamProfileViewModel
            {
                Name = team.Name,
                Logo = team.Logo,
                PlayersListViewModels = players,
                FixtureViewModels = fixtures,
                TeamId = team.Id
            };
            return View(tpvm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTeamViewModel ctvm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            Team newTeam = new Team
            {
                Name = ctvm.Name
            };

            if (ctvm.Logo != null && ctvm.Logo.Length < 204800)
            {
                string uniqueFileName = null;
                string uploadsFolder = Path.Combine(_env.WebRootPath, "teamlogos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ctvm.Logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                ctvm.Logo.CopyTo(fileStream);
                fileStream.Close();
                newTeam.Logo = uniqueFileName;
            }
            else
            {
                newTeam.Logo = "default.png";
            }
            _teamRepository.CreateTeam(newTeam);
            return RedirectToAction("Profile", new { id = newTeam.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTeam(int id)
        {
            var team = _teamRepository.GetTeamById(id);
            string directoryPath = Path.Combine(_env.WebRootPath, "teamlogos");
            if (team.Logo != "default.png")
            {
                string filePath = Path.Combine(directoryPath, team.Logo);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            var fixtures = _fixtureRepository.GetFixturesForTeam(team.Id);
            _fixtureRepository.DeleteFixtures(fixtures);
            _teamRepository.DeleteTeam(team);
            return RedirectToAction("Index");
        }
    }
}
