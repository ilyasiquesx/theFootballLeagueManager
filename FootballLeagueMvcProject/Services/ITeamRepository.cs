using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Services
{
    public interface ITeamRepository
    {

        IEnumerable<Team> GetAllTeams();
        Team GetTeamById(int id);
        void CreateTeam(Team team);

        void DeleteTeam(Team team);
    }
}
