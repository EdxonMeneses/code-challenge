using DB.Models;

namespace DB.Contracts;

public interface ICardsRepository
{
    void Add(Card card);
    Card GetCard(long cardNumber);
    IEnumerable<Card> GetCards();
    void Update(Card card);
}
