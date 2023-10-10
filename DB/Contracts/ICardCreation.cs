using DB.Models;

namespace DB.Contracts;

public interface ICardCreation
{
    Task<Card> CreateCard(decimal amount);
}
