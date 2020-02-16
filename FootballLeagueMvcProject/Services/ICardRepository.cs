using FootballLeagueMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Services
{
    public interface ICardRepository
    {
        void AddCard(Card card);
    }
}
