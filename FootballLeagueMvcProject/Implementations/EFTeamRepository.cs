using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Implementations
{
    public class EFTeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EFTeamRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void CreateTeam(Team team)
        {
            _applicationDbContext.Add(team);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _applicationDbContext.Teams
                .Include(t => t.Players)
                .Include(t => t.ChampionshipTeams).ThenInclude(ct => ct.Championship)
                .ToList();
        }

        public Team GetTeamById(int id)
        {
            return GetAllTeams().FirstOrDefault(t => t.Id == id);
        }


        public void DeleteTeam(Team team)
        {
            _applicationDbContext.Teams.Remove(team);

            _applicationDbContext.SaveChanges();
        }

    }
}
