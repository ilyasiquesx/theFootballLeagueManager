using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Implementations
{
    public class EFChampionshipRepository : IChampionshipRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EFChampionshipRepository(ApplicationDbContext applicationDbContext)

        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Championship> GetAllChampionships()
        {
            return _applicationDbContext.Championships
                .Include(c => c.ChampionshipTeams).ThenInclude(ct => ct.Team).ThenInclude(t => t.Players)
                .Include(c => c.Fixtures).ThenInclude(f => f.AwayTeam).ThenInclude(t => t.Players)
                .Include(c => c.Fixtures).ThenInclude(f => f.HomeTeam).ThenInclude(t => t.Players).ToList();
        }

        public Championship GetChampionshipById(int id)
        {
            return GetAllChampionships().FirstOrDefault(c => c.Id == id);
        }


        public void AddTeamToChampionship(Team team, Championship championship)
        {
            championship.ChampionshipTeams.Add(new ChampionshipTeam
            {
                ChampionshipId = championship.Id,
                TeamId = team.Id
            });

            _applicationDbContext.SaveChanges();
        }

        public void UntieTeam(Team team, Championship championship)
        {
            var ct = championship.ChampionshipTeams.FirstOrDefault(c => c.ChampionshipId == championship.Id && c.TeamId == team.Id);
            championship.ChampionshipTeams.Remove(ct);
            _applicationDbContext.SaveChanges();
        }

        public void CreateChampionship(Championship championship)
        {
            _applicationDbContext.Championships.Add(championship);
            _applicationDbContext.SaveChanges();
        }

        public void DeleteChampionship(Championship championship)
        {
            _applicationDbContext.Championships.Remove(championship);
            _applicationDbContext.SaveChanges();
        }

        // Оптимизированные запросы: 

        public IEnumerable<Championship> GetChampionshipsName()
        {
            return _applicationDbContext.Championships.ToList();
        }

        public Championship GetChampionshipByIdOptimized(int id)
        {
            return _applicationDbContext.Championships.FirstOrDefault(c => c.Id == id);
        }

        public Championship GetChampionshipTeamsByIdOptimized(int id)
        {
            return _applicationDbContext.Championships.Include(c => c.ChampionshipTeams).ThenInclude(ct=>ct.Team).ThenInclude(t=>t.Players).FirstOrDefault(c=>c.Id == id);
        }
    }
}
