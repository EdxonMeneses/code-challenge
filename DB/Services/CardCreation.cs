using DB.Contracts;
using DB.Models;

namespace DB.Services;

public class CardCreation : ICardCreation
{
    private readonly ICardsRepositoryAsync _cardsRepository;

    public CardCreation(ICardsRepositoryAsync cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }
    public async Task<Card> CreateCard(decimal amount)
    {
        long min_number = 100000000000000;
        long max_number = 999999999999999;
        Random random = new();
        long cardNumber = 0;
        Card card = new();
        int counter = 0;
        int tries = 5;
        while (card != null)
        {
            cardNumber = random.NextInt64(min_number, max_number);
            card = await _cardsRepository.GetCard(cardNumber);
            counter++;
            if (counter >= tries)
            {
                Console.WriteLine($"More than {tries} tries. Out of card numbers.");
                throw new Exception("An unexpected error occurred.");
            }
        }
        card = new Card
        {
            CardNumber = cardNumber,
            Balance = amount
        };
        return card;
    }
}
