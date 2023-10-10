using DB.Models;

namespace DB.Contracts;

public interface ICardsRepositoryAsync
{ 
    Task Add(Card card);
    Task<Card> GetCard(long cardNumber);
    Task<IEnumerable<Card>> GetCards();
    Task Update(Card card);
}
