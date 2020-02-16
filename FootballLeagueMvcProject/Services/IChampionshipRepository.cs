using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Services
{
    public interface IChampionshipRepository
    {
        IEnumerable<Championship> GetAllChampionships();

        Championship GetChampionshipById(int id);
        void AddTeamToChampionship(Team team, Championship championship);
        void UntieTeam(Team team, Championship championship);

        void CreateChampionship(Championship championship);

        void DeleteChampionship(Championship championship);

        IEnumerable<Championship> GetChampionshipsName();
        //Оптимизированные запросы
        public Championship GetChampionshipByIdOptimized(int id);
        public Championship GetChampionshipTeamsByIdOptimized(int id);
    }
}
