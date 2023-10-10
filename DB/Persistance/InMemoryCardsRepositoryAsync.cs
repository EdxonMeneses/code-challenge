using DB.Contracts;
using DB.Models;

namespace DB.Persistance;

public class InMemoryCardsRepositoryAsync : ICardsRepositoryAsync
{
    private List<Card> cards = new List<Card>();

    public async Task Add(Card card)
    {
        Random random = new();
        card.Id = random.Next(0, 1000000000);
        await Task.Run(() => cards.Add(card));
    }

    public async Task<Card> GetCard(long cardNumber)
    {
        return await Task.FromResult(cards.FirstOrDefault(c => c.CardNumber == cardNumber)!);
    }

    public async Task<IEnumerable<Card>> GetCards()
    {
        return await Task.FromResult(cards);
    }

    public async Task Update(Card card)
    {
        await Task.Run(() =>
        {
            cards.RemoveAll(c => c.CardNumber == card.CardNumber);
            cards.Add(new Card { CardNumber = card.CardNumber, Balance = card.Balance });
        });
    }
}
