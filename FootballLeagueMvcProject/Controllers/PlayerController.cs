using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using FootballLeagueMvcProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Controllers
{
    public class PlayerController : Controller

    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _env;

        public object PlayerCreateViewModel { get; private set; }

        public PlayerController(IPlayerRepository playerRepository, ITeamRepository teamRepository, IWebHostEnvironment env)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _env = env;
        }
        public IActionResult Index()
        {

            var players = _playerRepository.GetAllPlayers().Where(t => t.Team == null);
            List<PlayersListViewModel> plvm = new List<PlayersListViewModel>();
            foreach (var item in players)
            {
                if (item.Team == null)
                {
                    plvm.Add(new PlayersListViewModel
                    {
                        Id = item.Id,
                        Name = item.FirstName,
                        LastName = item.LastName,
                        Photo = item.Photo,
                        Team = "Без команды",
                        TeamId = 0
                    });
                }
                else
                {
                    plvm.Add(new PlayersListViewModel
                    {
                        Id = item.Id,
                        Name = item.FirstName,
                        LastName = item.LastName,
                        Photo = item.Photo,
                        Team = item.Team.Name,
                        TeamId = item.Team.Id
                    });
                }
            }

            return View(plvm);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Create()
        {
            var teams = _teamRepository.GetAllTeams().Select(t => new TeamViewModel
            {
                Name = t.Name,
            });

            CreatePlayerViewModel cpvm = new CreatePlayerViewModel
            {
                Roles = new List<string>
                {
                    "Вратарь",
                    "Защитник",
                    "Полузащитник",
                    "Нападающий"
                }
            };

            return View(cpvm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Create(CreatePlayerViewModel cpvm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            Player newPlayer = new Player
            {
                FirstName = cpvm.FirstName,
                LastName = cpvm.LastName,
                Birthday = cpvm.DateOfBirth,
                AtNumber = cpvm.AtNumber,
                Patronymic = cpvm.Patronymic,
                Role = cpvm.Role
            };

            // Помещение фото игрока на сервер, проверка, не превышает ли картинка допустимое ограничение по размеру.

            if (cpvm.Photo != null && cpvm.Photo.Length < 204800)
            {
                string uniqueFileName = null;
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Photos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + cpvm.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                cpvm.Photo.CopyTo(fileStream);
                fileStream.Close();
                newPlayer.Photo = uniqueFileName;
            }
            else

            // В случае, если картинка выбрана не была, устанавливаем значение фото по умолчанию.

            {
                newPlayer.Photo = "default.png";
            }

            _playerRepository.CreatePlayer(newPlayer);

            return RedirectToAction("Index", "Player");
        }


        // Присваивание конкретного игрока в команду.

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var player = _playerRepository.GetPlayerById(id);

            var teams = _teamRepository.GetAllTeams();

            var editPlayerViewModel = new EditPlayerViewModel
            {
                PlayerId = player.Id,
                FullName = player.LastName + " " + player.FirstName + " " + player.Patronymic,
                DateOfBirth = player.Birthday.ToString("dd/MM/yyyy"),
                AtNumber = player.AtNumber,
                Role = player.Role,
                TeamViewModels = teams.Select(t => new TeamViewModel { TeamId = t.Id, Name = t.Name })

            };

            return View(editPlayerViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditPlayerViewModel epvm)
        {
            var player = _playerRepository.GetPlayerById(epvm.PlayerId);
            var team = _teamRepository.GetTeamById(epvm.TeamId);
            player.Team = team;
            _playerRepository.Update();
            return RedirectToAction("Index", "Player");
        }

        public IActionResult Profile(int id)
        {
            if (id != 0)
            {
                var player = _playerRepository.GetPlayerById(id);
                int ages = DateTime.Now.Year - player.Birthday.Year;
                if(DateTime.Now.DayOfYear<player.Birthday.DayOfYear)
                {
                    ages--;
                }
                
                PlayerProfileViewModel ppvm = new PlayerProfileViewModel
                {
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Patronymic = player.Patronymic,
                    Photo = player.Photo,
                    DateOfBirth = player.Birthday,
                    Ages = ages,
                    AtNumber = player.AtNumber,
                    Role = player.Role
                };

                if (player.Team != null)
                {
                    ppvm.TeamName = player.Team.Name;
                    ppvm.TeamId = player.Team.Id;
                    ppvm.TeamLogo = player.Team.Logo;
                }
                else
                {
                    ppvm.TeamName = null;
                    ppvm.TeamId = 0;
                }

                return View(ppvm);
            }

            return RedirectToAction("Index", "Home");
        }


        // Удаление игрока

        [HttpPost]
        public IActionResult DeletePlayer(int id)
        {
            var player = _playerRepository.GetPlayerById(id);

            if (player != null)
            {
                string directoryPath = Path.Combine(_env.WebRootPath, "Photos");
                if (player.Photo != "default.png")
                {
                    string filePath = Path.Combine(directoryPath, player.Photo);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _playerRepository.DeletePlayer(player.Id);
            }
            return RedirectToAction("Index");
        }
    }
}
