using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Services
{
    public interface IGoalRepository
    {
        void AddGoal(Goal goal);
        public Goal GetGoalById(int id);
        
        public IEnumerable<Goal> GetFixtureGoals(int fixtureId);


        public IEnumerable<Goal> GetAllGoals();
        public void DeleteGoals(IEnumerable<Goal> goals);
    }
}
