using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Implementations
{
    public class EFFixtureRepository : IFixtureRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EFFixtureRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
        public void CreateFixture(Fixture fixture)
        {
            _applicationDbContext.Fixtures.Add(fixture);
            _applicationDbContext.SaveChanges();
        }


        public void DeleteFixtures(IEnumerable<Fixture> fixtures)
        {
            _applicationDbContext.RemoveRange(fixtures);
            _applicationDbContext.SaveChanges();
        }
        public Fixture GetFixtureById(int id)
        {
            return GetAllFixtures()
                .FirstOrDefault(f => f.Id == id);
        }


        public void Update()
        {
            _applicationDbContext.SaveChanges();
        }
        public IEnumerable<Fixture> GetFixturesForTeam(int id)
        {
            var homeFixtures = GetAllFixtures().Where(f => f.HomeTeam.Id == id);
            var awayFixtures = GetAllFixtures().Where(f => f.AwayTeam.Id == id);
            return homeFixtures.Concat(awayFixtures);
        }

        public IEnumerable<Fixture> GetAllFixtures()
        {
            return _applicationDbContext.Fixtures
                .Include(f => f.HomeTeam).ThenInclude(ht => ht.Players).ThenInclude(p => p.Team)
                .Include(f => f.AwayTeam).ThenInclude(at => at.Players).ThenInclude(p => p.Team)
                .Include(f => f.Goals).ThenInclude(g => g.Author).ThenInclude(a => a.Team)
                .Include(f => f.Goals).ThenInclude(g => g.Assist).ThenInclude(a => a.Team)
                .Include(f => f.Cards).ThenInclude(c => c.Player).ThenInclude(p => p.Team)
                .ToList();
        }

        public void ResetGoals(int id)
        {
            var goals = GetFixtureById(id).Goals;
            _applicationDbContext.Goals.RemoveRange(goals);
            _applicationDbContext.SaveChanges();
        }

        public void ResetCards(int id)
        {
            var cards = GetFixtureById(id).Cards;
            _applicationDbContext.Cards.RemoveRange(cards);
            _applicationDbContext.SaveChanges();
        }
        public void DeleteFixture(Fixture fixture)
        {
            _applicationDbContext.Fixtures.Remove(fixture);
            _applicationDbContext.SaveChanges();
        }

        // Оптимзированные запросы :


        public IEnumerable<Fixture> GetFixturesInCompetitionOptimized(int id)
        {
            var fixtures = _applicationDbContext.Fixtures.Where(f => f.ChampionshipId == id)
                .Include(f=>f.AwayTeam)
                .Include(f=>f.HomeTeam)
                .Include(f=>f.Goals).ToList();

            return fixtures;
        }
        public Fixture GetFixtureByIdOptimized(int id)
        {
            return _applicationDbContext.Fixtures.Include(f=>f.Goals).Include(f=>f.Cards).FirstOrDefault(f => f.Id == id);
        }

        public Fixture GetFixtureToManageOptimized(int id)
        {
            return _applicationDbContext.Fixtures.Include(f => f.AwayTeam).Include(f => f.HomeTeam).FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Goal> GetGoalsInCompetition(int champId)
        {
            return _applicationDbContext.Fixtures.Include(f=>f.Goals).ThenInclude(g=>g.Author).ThenInclude(a=>a.Team).Where(f => f.ChampionshipId == champId).SelectMany(f => f.Goals);
        }

        public IEnumerable<Fixture> GetFixturesForTeamOptimized(int id)
        {
            var homeFixtures = _applicationDbContext.Fixtures.Include(c=>c.Goals).Where(f => f.HomeTeam.Id == id);
            var awayFixtures = _applicationDbContext.Fixtures.Include(c => c.Goals).Where(f => f.AwayTeam.Id == id);
            return homeFixtures.Concat(awayFixtures);
        }

        public IEnumerable<Goal> GetGoalsTeamScored(int fixtureId, int teamId)
        {
            var fixture = _applicationDbContext.Fixtures.Include(f => f.Goals).ThenInclude(g => g.Author).FirstOrDefault(f => f.Id == fixtureId);

            if (fixture.Goals == null)
            {
                return new List<Goal>();
            }

            var team = _applicationDbContext.Teams.FirstOrDefault(t => t.Id == teamId);

            var goals = fixture.Goals.Where(g => g.Author != null && g.Author.Team.Id == team.Id);

            return goals;
        }


        public IEnumerable<Card> GetCardsTeamGot(int fixtureId, int teamId)
        {
            var fixture = _applicationDbContext.Fixtures.Include(g => g.Cards).ThenInclude(c => c.Player).FirstOrDefault(f => f.Id == fixtureId);

            if (fixture.Cards == null)
            {
                return new List<Card>();
            }

            var team = _applicationDbContext.Teams.FirstOrDefault(t => t.Id == teamId);

            var cards = fixture.Cards.Where(c => c.Player != null && c.Player.Team.Id == team.Id);

            return cards;
        }

        public int GetCountOfFixtures(int id)
        {
            return _applicationDbContext.Fixtures.Where(f => f.ChampionshipId == id).Count();
        }

    }
}
