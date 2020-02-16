using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Implementations
{
    public class EFCardRepository : ICardRepository
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public EFCardRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddCard(Card card)
        {
            _applicationDbContext.Cards.Add(card);
            _applicationDbContext.SaveChanges();
        }

    }
}
