using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Implementations
{
    public class EFPlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EFPlayerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _applicationDbContext.Players.Include(p => p.Team);
        }

        public Player GetPlayerById(int id)
        {
            return _applicationDbContext.Players.Include(p=> p.Team).FirstOrDefault(p=>p.Id==id);
        }
        public void CreatePlayer(Player player)
        {
            _applicationDbContext.Players.Add(player);
            _applicationDbContext.SaveChanges();
        }

        public void Update()
        {
            _applicationDbContext.SaveChanges();
        }
        public void DeletePlayer(int id)
        {
            var player = _applicationDbContext.Players.FirstOrDefault(p => p.Id == id);
            var events = _applicationDbContext.Goals.Where(g => g.Author == player || g.Assist == player);
            _applicationDbContext.Goals.RemoveRange(events);
            _applicationDbContext.Players.Remove(player);
            _applicationDbContext.SaveChanges();
        }
    }
}
