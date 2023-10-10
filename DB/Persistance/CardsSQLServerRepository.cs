using DB.Contracts;
using DB.Models;
using DB.Persistance.SQLSever;
using Microsoft.EntityFrameworkCore;

namespace DB.Persistance;

public class CardsSQLServerRepository : ICardsRepositoryAsync
{
    private readonly ApplicationDBContext _context;

    public CardsSQLServerRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task Add(Card card)
    {
        _context.Add(card);
        await _context.SaveChangesAsync();
    }

    public async Task<Card> GetCard(long cardNumber)
    {
        return await _context.Cards.FirstOrDefaultAsync(card => card.CardNumber == cardNumber);
    }

    public async Task<IEnumerable<Card>> GetCards()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task Update(Card card)
    {
        _context.Entry(card).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
