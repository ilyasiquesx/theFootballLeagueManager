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
    public class FixtureController : Controller

    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IChampionshipRepository _championshipRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICardRepository _cardRepository;

        public FixtureController(IFixtureRepository fixtureRepository, ITeamRepository teamRepository,
            IChampionshipRepository championshipRepository, IGoalRepository goalRepository, IPlayerRepository playerRepository, ICardRepository cardRepository)
        {
            _fixtureRepository = fixtureRepository;
            _teamRepository = teamRepository;
            _championshipRepository = championshipRepository;
            _goalRepository = goalRepository;
            _playerRepository = playerRepository;
            _cardRepository = cardRepository;
        }

        // Метод построения модели для отображения подробностей проведенной или запланированной встречи.
        public IActionResult Details(int id)
        {
            var fixture = _fixtureRepository.GetFixtureByIdOptimized(id); // Получаем встречу.

            var techDefeatedTeam = _teamRepository.GetTeamById(fixture.TechDefeatedTeamId); // Получаем команду, получишвую техническое поражение.

            var goalsHomeTeam = _fixtureRepository.GetGoalsTeamScored(fixture.Id, fixture.HomeTeam.Id).Select(g => new GoalViewModel  // Подготавливаем последовательность из моделей для отображения голов ДОМАШНЕЙ команды.
            {
                AssistId = g.Assist == null ? 0 : g.Assist.Id,
                AssistName = g.Assist == null ? null : g.Assist.FirstName + " " + g.Assist.LastName,
                AuthorId = g.Author.Id,
                AuthorName = g.Author.FirstName + " " + g.Author.LastName,
                AtMinute = g.AtMinute

            }).ToList();

            var goalsAwayTeam = _fixtureRepository.GetGoalsTeamScored(fixture.Id, fixture.AwayTeam.Id).Select(g => new GoalViewModel // Подготавливаем последовательность из моделей для отображения голов ГОСТЕВОЙ команды.
            {
                AssistId = g.Assist == null ? 0 : g.Assist.Id,
                AssistName = g.Assist == null ? null : g.Assist.FirstName + " " + g.Assist.LastName,
                AuthorId = g.Author.Id,
                AuthorName = g.Author.FirstName + " " + g.Author.LastName,
                AtMinute = g.AtMinute

            }).ToList();


            var cardsHomeTeam = _fixtureRepository.GetCardsTeamGot(fixture.Id, fixture.HomeTeam.Id).Select(c => new CardViewModel // Аналогия с карточками.
            {
                AtMinute = c.AtMinute,
                PlayerId = c.Player.Id,
                PlayerName = c.Player.FirstName + " " + c.Player.LastName,
                Type = c.Type

            }).ToList();

            var cardsAwayTeam = _fixtureRepository.GetCardsTeamGot(fixture.Id, fixture.AwayTeam.Id).Select(c => new CardViewModel // Аналогия с карточками.
            {
                AtMinute = c.AtMinute,
                PlayerId = c.Player.Id,
                PlayerName = c.Player.FirstName + " " + c.Player.LastName,
                Type = c.Type

            }).ToList();


            FixtureViewModel fvm = new FixtureViewModel
            {
                AwayTeam = fixture.AwayTeam.Name,
                HomeTeam = fixture.HomeTeam.Name,
                AwayTeamId = fixture.AwayTeam.Id,
                HomeTeamId = fixture.HomeTeam.Id,
                Status = fixture.Status,
                FixtureId = fixture.Id,
                HomeTeamGoals = goalsHomeTeam.OrderBy(g => g.AtMinute),
                AwayTeamGoals = goalsAwayTeam.OrderBy(g => g.AtMinute),
                AwayTeamCards = cardsAwayTeam.OrderBy(g => g.AtMinute),
                HomeTeamCards = cardsHomeTeam.OrderBy(g => g.AtMinute),
                AppointedTime = fixture.AppointedTime,
                IsTechDefeat = fixture.IsTechDefeat,
                TechDefeatedTeamId = techDefeatedTeam != null ? techDefeatedTeam.Id : 0, // Проверяем, существует ли команда, получившая техническое поражение. 
                AwayTeamLogo = fixture.AwayTeam.Logo,
                HomeTeamLogo = fixture.HomeTeam.Logo

            };

            if (fixture.Status) // В случае, если Status == true (маркер о том, что встреча была сыграна и данные о ней существуют) подготавливаем инфомрацию о голах.
            {
                fvm.HomeTeamScore = fixture.Goals == null ? 0 : fixture.Goals.Where(g => g.Author.Team.Id == fixture.HomeTeam.Id).Count();
                fvm.AwayTeamScore = fixture.Goals == null ? 0 : fixture.Goals.Where(g => g.Author.Team.Id == fixture.AwayTeam.Id).Count();
                return View(fvm);
            }
            else
            {
                return View(fvm);
            }

        }


        // Назначем встречу между двумя командами, УЧАВСТВУЮЩИХ В СОРЕВНОВАНИИ.

        [Authorize(Roles = "Admin")]
        public IActionResult Create(int id)
        {

            var competition = _championshipRepository.GetChampionshipTeamsByIdOptimized(id);

            var teams = competition.ChampionshipTeams.Select(t => t.Team);

            var teamViewModels = teams
                 .Select(t => new TeamViewModel
                 {
                     Name = t.Name,
                     TeamId = t.Id
                 }).ToList();

            CreateFixtureViewModel fvm = new CreateFixtureViewModel
            {
                TeamViewModels = teamViewModels,
                CompetitionId = id
            };


            return View(fvm);
        }

        [HttpPost]
        public IActionResult Create(CreateFixtureViewModel cfvm)
        {
            if ((cfvm.AwayTeamId == 0 || cfvm.HomeTeamId == 0) || (cfvm.HomeTeamId == cfvm.AwayTeamId))
            {
                return RedirectToAction("Index", "Competition", new { id = cfvm.CompetitionId });
            }
            else
            {
                var homeTeam = _teamRepository.GetTeamById(cfvm.HomeTeamId);
                var awayTeam = _teamRepository.GetTeamById(cfvm.AwayTeamId);
                var competition = _championshipRepository.GetChampionshipByIdOptimized(cfvm.CompetitionId);

                Fixture newFixture = new Fixture
                {
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    Championship = competition,
                    CreatedAt = DateTime.Now,
                    Status = false,
                    IsTechDefeat = false,
                    AppointedTime = cfvm.AppointedTime
                };

                _fixtureRepository.CreateFixture(newFixture);

                return RedirectToAction("Results", "Competition", new { id = cfvm.CompetitionId });
            }

        }
        [Authorize(Roles = "Admin")]

        // Метод, необходимый для того, чтобы внести данные о количестве забитых голов и количестве полученных карточек.
        public IActionResult Manage(int id)
        {
            var fixture = _fixtureRepository.GetFixtureToManageOptimized(id);

            ManageFixtureViewModel mfvm = new ManageFixtureViewModel
            {
                FixtureId = fixture.Id,
                HomeTeamName = fixture.HomeTeam.Name,
                AwayTeamName = fixture.AwayTeam.Name,
                TeamViewModels = new List<TeamViewModel>
                {
                    new TeamViewModel
                    {
                        TeamId = fixture.HomeTeam.Id,
                        Name = fixture.HomeTeam.Name
                    },
                    new TeamViewModel
                    {
                        TeamId = fixture.AwayTeam.Id,
                        Name = fixture.AwayTeam.Name
                    }
                }
            };

            return View(mfvm);
        }


        [HttpPost]
        public IActionResult Manage(ManageFixtureViewModel mfvm)
        {

            var fixture = _fixtureRepository.GetFixtureByIdOptimized(mfvm.FixtureId);

            if (mfvm.IsTechDefeat && mfvm.TechDefeatedTeamId != 0)
            {
                fixture.IsTechDefeat = true;
                fixture.Status = true;
                fixture.TechDefeatedTeamId = mfvm.TechDefeatedTeamId;
                var goals = fixture.Goals;
                _goalRepository.DeleteGoals(goals);
                _fixtureRepository.Update();
                return RedirectToAction("Details", "Fixture", new { id = fixture.Id });
            }
            else if (mfvm.AwayTeamScores == 0 && mfvm.HomeTeamScores == 0 && mfvm.HomeTeamCards == 00 && mfvm.AwayTeamCards == 0)
            {
                fixture.Status = true;
                fixture.IsTechDefeat = false;
                _fixtureRepository.Update();

                return RedirectToAction("Details", "Fixture", new { id = fixture.Id });
            }
            fixture.IsTechDefeat = false;
            _fixtureRepository.Update();
            return Redirect($"~/Fixture/ManageGoals/{mfvm.FixtureId}?hts={mfvm.HomeTeamScores}&ats={mfvm.AwayTeamScores}&htc={mfvm.HomeTeamCards}&atc={mfvm.AwayTeamCards}");
        }


        // Метод, позволяюший внести подробные данные об авторах голов, ассистов и о игроках, получивших карточки.

        [Authorize(Roles = "Admin")]
        public IActionResult ManageGoals(int id, int hts, int ats, int htc, int atc)
        {
            var fixture = _fixtureRepository.GetFixtureToManageOptimized(id);

            var homeTeam = _teamRepository.GetTeamById(fixture.HomeTeam.Id).Players.Select(p => new PlayersListViewModel
            {
                Id = p.Id,
                Name = p.FirstName,
                LastName = p.LastName
            });

            var awayTeam = _teamRepository.GetTeamById(fixture.AwayTeam.Id).Players.Select(p => new PlayersListViewModel
            {
                Id = p.Id,
                Name = p.FirstName,
                LastName = p.LastName
            });


            ManageGoalsViewModel mgvm = new ManageGoalsViewModel
            {
                FixtureId = fixture.Id,
                AwayTeamScore = ats, // здесь
                HomeTeamScore = hts,
                PlayersListViewModelsAwayTeam = awayTeam,
                PlayersListViewModelsHomeTeam = homeTeam,
                AwayTeamName = fixture.AwayTeam.Name,
                HomeTeamName = fixture.HomeTeam.Name,
                AwayTeamCards = atc,
                HomeTeamCards = htc,
                CardTypes = new List<string> { "Красная", "Желтая", "ЖК" }

            };

            return View(mgvm);
        }

        [HttpPost]
        public IActionResult ManageGoals(ManageGoalsViewModel mgvm)
        {
            var fixture = _fixtureRepository.GetFixtureByIdOptimized(mgvm.FixtureId);


            // Если в форме переданны новые данные, то сбрасываем старые, если они имеются и устанавливаем новые.

            if (mgvm.GoalsViewModels != null)
            {

                if (fixture.Goals.Count != 0)
                {
                    _fixtureRepository.ResetGoals(fixture.Id);
                }


                foreach (var goal in mgvm.GoalsViewModels)
                {
                    _goalRepository.AddGoal(new Goal
                    {
                        Fixture = fixture,
                        Assist = _playerRepository.GetPlayerById(goal.AssistId),
                        Author = _playerRepository.GetPlayerById(goal.AuthorId),
                        AtMinute = goal.AtMinute
                    });
                }
            }

            if (mgvm.CardViewModel != null)
            {

                if (fixture.Cards.Count != 0)
                {
                    _fixtureRepository.ResetCards(fixture.Id);
                }

                foreach (var card in mgvm.CardViewModel)
                {
                    _cardRepository.AddCard(new Card
                    {
                        Fixture = fixture,
                        AtMinute = card.AtMinute,
                        Type = card.Type,
                        Player = _playerRepository.GetPlayerById(card.PlayerId)
                    });
                }
            }

            fixture.Status = true;
            _fixtureRepository.SaveChanges();
            return RedirectToAction("Details", "Fixture", new { id = mgvm.FixtureId });
        }


        // Удаление встречи.

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(DeleteFixtureViewModel dfvm)
        {
            var fixture = _fixtureRepository.GetFixtureByIdOptimized(dfvm.FixtureId);
            _fixtureRepository.DeleteFixture(fixture);

            return RedirectToAction("Results", "Competition", new { id = dfvm.CompetitionId });
        }
    }
}
