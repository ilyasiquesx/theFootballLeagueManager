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
    public class CompetitionController : Controller
    {
        private readonly IChampionshipRepository _championshipRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IFixtureRepository _fixtureRepository;

        public CompetitionController(IChampionshipRepository championshipRepository, ITeamRepository teamRepository, IFixtureRepository fixtureRepository)
        {
            _championshipRepository = championshipRepository;
            _teamRepository = teamRepository;
            _fixtureRepository = fixtureRepository;
        }

        public IActionResult Index(int id)
        {
            var competition = _championshipRepository.GetChampionshipsName().FirstOrDefault(c => c.Id == id);

            CompetitionViewModel cvm = new CompetitionViewModel
            {
                Name = competition.Name,
                Id = competition.Id
            };

            return View(cvm);
        }


        // Функционал добавления команды в участие в соревновании. (Отношение многие ко многим). Для участия в соревновании команде необходимо стать участником этого соревнования.

        [Authorize(Roles = "Admin")]
        public IActionResult AddTeamToCompetition(int id)
        {
            var competition = _championshipRepository.GetChampionshipTeamsByIdOptimized(id);
            var teams = competition.ChampionshipTeams.Select(c => c.Team);

            var teamsExcept = _teamRepository.GetAllTeams().Except(teams).Select(t => new TeamViewModel // В данном методе формируется список, в котором отсутствуют команды, которые УЖЕ являются участником данного соревнования.
            {
                Name = t.Name,
                TeamId = t.Id
            });

            AddTeamToCompetitionViewModel attcvm = new AddTeamToCompetitionViewModel
            {
                CompetitionId = competition.Id,
                TeamViewModels = teamsExcept
            };

            return View(attcvm);
        }

        [HttpPost]
        public IActionResult AddTeamToCompetition(AddTeamToCompetitionViewModel tvm)
        {

            if (tvm.TeamId == 0)
            {
                return RedirectToAction("Index", new { id = tvm.CompetitionId });
            }
            else
            {
                var championship = _championshipRepository.GetChampionshipByIdOptimized(tvm.CompetitionId);
                var team = _teamRepository.GetTeamById(tvm.TeamId);
                _championshipRepository.AddTeamToChampionship(team, championship);
                return RedirectToAction("Teams", "Competition", new { id = tvm.CompetitionId });
            }

        }

        //Метод, позволяющий отзаявить команду от участия в соревновании.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Untie(UntieViewModel uvm)
        {
            var competition = _championshipRepository.GetChampionshipByIdOptimized(uvm.CompetitionId);
            var team = _teamRepository.GetTeamById(uvm.TeamId);
            var fixtures = _fixtureRepository.GetFixturesForTeamOptimized(team.Id);
            _fixtureRepository.DeleteFixtures(fixtures);
            _championshipRepository.UntieTeam(team, competition);
            return RedirectToAction("Teams", "Competition", new { id = uvm.CompetitionId });
        }


        //Метод, строющий турнирную таблицу на основе уже сыгранных встреч.

        public IActionResult Table(int id)
        {
            var championship = _championshipRepository.GetChampionshipTeamsByIdOptimized(id);
            var teams = championship.ChampionshipTeams.Select(g => g.Team);
            var leaderboardViewModel = BuildTableViewModel(teams, championship);
            return View(leaderboardViewModel);
        }


        /*Метод, подготавливающий список из команд и их статистикой 
         
             -За победу команде начисляется 3 очка, за поржание 0, за ничью 1, за наличие технического поражение снимается 1 очко.

             */
        IList<TableViewModel> BuildTableViewModel(IEnumerable<Team> teams, Championship championship) //Метод принимает последовательность из команд, учавствующих в соревновании и само соревнование.
        {
            IList<TableViewModel> tableViews = new List<TableViewModel>();

            foreach (var t in teams)  //Для каждой команды мы просматриваем каждую встречу, где она является либо гостевым, либо домашним участниокм.
            {
                var fixtures = _fixtureRepository.GetFixturesInCompetitionOptimized(championship.Id).Where(f => f.Status && (f.HomeTeam==t || f.AwayTeam==t)).OrderByDescending(f => f.AppointedTime);

                var wins = 0;
                var loses = 0;
                var draws = 0;

                var teamScoredTotal = 0;
                var teamMissedTotal = 0;

                var points = 0;

                IList<LastMatchViewModel> lastFive = new List<LastMatchViewModel>(); //Лист элементов, который небходим для того, чтобы отобразить ФОРМУ команды (результаты последних 5 матчей).

                foreach (var fixture in fixtures) // Просмотр каждой встречи.
                {
                    var teamScored = 0;
                    var opponentScored = 0;

                    if (fixture.HomeTeam == t) // Случай, если данная команда является домашней командой данной встречи.
                    {
                        teamScored = _fixtureRepository.GetGoalsTeamScored(fixture.Id, t.Id).Count();
                        opponentScored = _fixtureRepository.GetGoalsTeamScored(fixture.Id, fixture.AwayTeam.Id).Count();

                        if (fixture.IsTechDefeat && t.Id == fixture.TechDefeatedTeamId) // Снимаем одно очко в случае технического поражения.
                        {
                            loses++;
                            points -= 1;
                            opponentScored = 5;
                            teamScored = 0;

                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'П'
                                });
                            }

                        }
                        else if (fixture.IsTechDefeat && fixture.TechDefeatedTeamId == fixture.AwayTeam.Id) // Оппонент выбранной команды является виновником технического поражения.
                        {
                            wins++;
                            points += 3;
                            opponentScored = 0;
                            teamScored = 5;

                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'В'
                                });
                            }
                        }

                        else if (teamScored > opponentScored) // Начисляем победу, так как забитых командой больше, чем пропущенных.
                        {
                            wins++;
                            points += 3;

                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'В'
                                });
                            }
                        }
                        else if (teamScored < opponentScored) // Начисляем поражение, так как забитых командой меньше, чем пропущеннхы.
                        {
                            loses++;
                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'П'
                                });
                            }
                        }
                        else // Ничья.
                        {
                            draws++;
                            points += 1;
                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'Н'
                                });
                            }
                        }
                    }
                    else if (fixture.AwayTeam == t) // Данная ветка кода имеет идентичный фукнционал, как и весь код выше, только рассматривается случай, если наша команда является ДОМАШНИМ участником встречи.
                    {
                        teamScored = _fixtureRepository.GetGoalsTeamScored(fixture.Id, t.Id).Count();
                        opponentScored = _fixtureRepository.GetGoalsTeamScored(fixture.Id, fixture.HomeTeam.Id).Count();

                        if (fixture.IsTechDefeat && t.Id == fixture.TechDefeatedTeamId)
                        {
                            loses++;
                            points -= 1;
                            opponentScored = 5;
                            teamScored = 0;

                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'П'
                                });
                            }
                        }
                        else if (fixture.IsTechDefeat && fixture.TechDefeatedTeamId == fixture.HomeTeam.Id)
                        {
                            wins++;
                            points += 3;
                            opponentScored = 0;
                            teamScored = 5;

                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'В'
                                });
                            }
                        }

                        else if (teamScored > opponentScored)
                        {
                            wins++;
                            points += 3;
                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'В'
                                });
                            }
                        }
                        else if (teamScored < opponentScored)
                        {
                            loses++;
                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'П'
                                });
                            }
                        }
                        else
                        {
                            draws++;
                            points += 1;
                            if (lastFive.Count < 5)
                            {
                                lastFive.Add(new LastMatchViewModel
                                {
                                    FixtureId = fixture.Id,
                                    Result = 'Н'
                                });
                            }
                        }
                    }

                    teamScoredTotal += teamScored;
                    teamMissedTotal += opponentScored;
                }

                tableViews.Add(new TableViewModel
                {
                    TeamName = t.Name,
                    TeamId = t.Id,
                    Matches = fixtures.Count(),
                    Wins = wins,
                    Draws = draws,
                    Losses = loses,
                    TotalGoals = teamScoredTotal,
                    TotalMisses = teamMissedTotal,
                    Points = points,
                    LastFive = lastFive.Reverse()
                });
            }
            return tableViews.OrderByDescending(tv => tv.Points)
                .ThenByDescending(tv => tv.TotalGoals - tv.TotalMisses)
                .ThenByDescending(tv => tv.TotalGoals)
                .ThenByDescending(tv => tv.Wins)
                .ToList();  // В случае равенства очков, команды сортируются по разнице мячей, а затем по забитым голам, затем по количествам побед.

        }

        // Метод построения списка встреч между командами в данном соревновании.
        public IActionResult Results(int id, int pageId = 1)
        {
            var championship = _championshipRepository.GetChampionshipByIdOptimized(id);

            int pageSize = 5;

            int countOfFixtures = _fixtureRepository.GetCountOfFixtures(id);

            int countOfPages = countOfFixtures % pageSize == 0 ? countOfFixtures / pageSize : (countOfFixtures / pageSize) + 1;

            if (pageId > countOfPages)
            {
                return RedirectToAction("Results", "Competition", new { id = id });
            }

            var fixtures = _fixtureRepository.GetFixturesInCompetitionOptimized(id).OrderByDescending(f => f.AppointedTime).Skip(pageSize * (pageId - 1)).Take(pageSize).Select(f => new FixtureViewModel
            {
                AwayTeam = f.AwayTeam.Name,
                HomeTeam = f.HomeTeam.Name,
                Status = f.Status,
                FixtureId = f.Id,
                AwayTeamScore = _fixtureRepository.GetGoalsTeamScored(f.Id, f.AwayTeam.Id).Count(),
                HomeTeamScore = _fixtureRepository.GetGoalsTeamScored(f.Id, f.HomeTeam.Id).Count(),
                AwayTeamLogo = f.AwayTeam.Logo,
                HomeTeamLogo = f.HomeTeam.Logo,
                HomeTeamId = f.HomeTeam.Id,
                AwayTeamId = f.AwayTeam.Id,
                CompetitionId = championship.Id,
                AppointedTime = f.AppointedTime,
                IsTechDefeat = f.IsTechDefeat,
                TechDefeatedTeamId = f.TechDefeatedTeamId
            });

            return View(new ResultsViewModel
            {
                ChampionshipId = championship.Id,
                FixtureViewModels = fixtures.OrderByDescending(f => f.AppointedTime).ThenByDescending(f => f.FixtureId),
                countOfPages = countOfPages
            });
        }


        // Метод построения списка команд, учавствующих в данном соревновании.

        public IActionResult Teams(int id)
        {
            var championship = _championshipRepository.GetChampionshipTeamsByIdOptimized(id);

            var teams = championship.ChampionshipTeams.Select(ct => ct.Team).Select(t => new TeamViewModel
            {
                Name = t.Name,
                TeamId = t.Id,
                CompetitionId = championship.Id,
                Logo = t.Logo
            });

            return View(new TeamsViewModel
            {

                ChampionshipId = championship.Id,
                TeamViewModels = teams

            });
        }


        // Метод построения статистики игроков в данном соревновании.

        public IActionResult Stats(int id)
        {
            var championship = _championshipRepository.GetChampionshipTeamsByIdOptimized(id);
            var players = championship.ChampionshipTeams.Select(ct => ct.Team).Where(t => t.Players != null).SelectMany(t => t.Players);
            var goals = _fixtureRepository.GetGoalsInCompetition(id); // Получаем ВСЕ голы, забитые во ВСЕХ встречах в данном соревновании.

            IList<PlayerStatViewModel> psvm = new List<PlayerStatViewModel>();

            /*
             
             Для каждого игрока мы просматриваем всю последовательность голов, принадлежащих этому соревнованию. 
             В случае, если авторство игрока подтверждается, он получает одно очко в аккумулятор голов (goalsScored).
             По аналогии и для результативных передач (assistsDone).
             
             */


            foreach (var p in players)
            {
                int goalsScored = 0;
                int assistsDone = 0;
                foreach (var g in goals)
                {
                    if (g.Author == p)
                    {
                        goalsScored++;
                    }

                    if (g.Assist == p)
                    {
                        assistsDone++;
                    }
                }
                psvm.Add(new PlayerStatViewModel
                {
                    PlayerId = p.Id,
                    PlayerFullName = p.FirstName + " " + p.LastName,
                    PlayerTeamId = p.Team.Id,
                    PlayerTeamName = p.Team.Name,
                    CountOfGoals = goalsScored,
                    CountOfAssists = assistsDone,
                    SumOfGoalsAndAssists = goalsScored + assistsDone
                });

            }

            return View(new StatViewModel
            {
                ChampionshipId = championship.Id,
                ListOfStrikers = psvm.OrderByDescending(p => p.CountOfGoals).Take(10),
                ListOfAssistans = psvm.OrderByDescending(p => p.CountOfAssists).Take(10),
                ListOfUniversals = psvm.OrderByDescending(p => p.SumOfGoalsAndAssists).Take(10)
            });
        }

    }
}
