using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Services
{
    public interface IFixtureRepository
    {
        void CreateFixture(Fixture fixture);
        Fixture GetFixtureById(int id);
        public IEnumerable<Fixture> GetAllFixtures();
        public IEnumerable<Fixture> GetFixturesForTeam(int id);
        public void DeleteFixture(Fixture fixture);
        public void DeleteFixtures(IEnumerable<Fixture> fixtures);
        public void SaveChanges();
        public void ResetGoals(int id);
        public void ResetCards(int id);
        public IEnumerable<Goal> GetGoalsTeamScored(int fixtureId, int teamId);

        public IEnumerable<Card> GetCardsTeamGot(int fixtureId, int teamId);
        public void Update();


        // ОПТИМИЗИРОВАННЫЕ ЗАПРОСЫ

        public IEnumerable<Fixture> GetFixturesInCompetitionOptimized(int id);
        public IEnumerable<Goal> GetGoalsInCompetition(int champId);
        public IEnumerable<Fixture> GetFixturesForTeamOptimized(int id);
        public Fixture GetFixtureByIdOptimized(int id);
        public int GetCountOfFixtures(int id);
        public Fixture GetFixtureToManageOptimized(int id);
    }
}
