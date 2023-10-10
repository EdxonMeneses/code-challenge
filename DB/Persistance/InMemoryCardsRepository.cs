using DB.Contracts;
using DB.Models;

namespace DB.Persistance;

public class InMemoryCardsRepository : ICardsRepository
{
    private List<Card> cards = new();

    public void Add(Card card)
    {
        Random random = new();
        card.Id = random.Next(0, 1000000000);
        cards.Add(card);
    }

    public Card GetCard(long cardNumber)
    {
        Card card = cards.FirstOrDefault(c => c.CardNumber == cardNumber)!;
        return card;
    }

    public IEnumerable<Card> GetCards()
    {
        return cards;
    }

    public void Update(Card card)
    {
        cards.RemoveAll(c => c.CardNumber == card.CardNumber);  
        cards.Add(new Card { CardNumber = card.CardNumber, Balance = card.Balance });
    }
}
