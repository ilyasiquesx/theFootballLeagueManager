using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Implementations
{
    public class EFGoalRepository : IGoalRepository
    {
        private readonly ApplicationDbContext _applictionDbContext;

        public EFGoalRepository(ApplicationDbContext applictionDbContext)
        {
            _applictionDbContext = applictionDbContext;
        }

        public Goal GetGoalById(int id)
        {
            return GetAllGoals().FirstOrDefault(g => g.Id == id);
        }


        public IEnumerable<Goal> GetFixtureGoals(int fixtureId)
        {
            return GetAllGoals().Where(g => g.Fixture.Id == fixtureId);
        }


        public IEnumerable<Goal> GetAllGoals()
        {
            return _applictionDbContext.Goals
                .Include(g => g.Author).ThenInclude(p => p.Team)
                .Include(g => g.Assist)
                .Include(g => g.Fixture).ToList();
        }

        public void AddGoal(Goal goal)
        {
            _applictionDbContext.Goals.Add(goal);
            _applictionDbContext.SaveChanges();
        }

        public void DeleteGoals(IEnumerable<Goal> goals)
        {
            _applictionDbContext.RemoveRange(goals);
            _applictionDbContext.SaveChanges();
        }
    }
}
