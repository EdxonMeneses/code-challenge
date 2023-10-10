using DB.Contracts;
using DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DB.Controllers
{
    [Route("api/card")]
    [ApiController]
    [Authorize]
    public class CardManagementController : ControllerBase
    {
        private readonly IUniversalFeesExchange _ufe;
        private readonly ICardsRepositoryAsync _cardsRepository;
        private readonly ICardCreation _cardCreation;

        public CardManagementController(IUniversalFeesExchange ufe, 
            ICardsRepositoryAsync cardsRepository, 
            ICardCreation cardCreation
            )
        {
            _ufe = ufe;
            _cardsRepository = cardsRepository;
            _cardCreation = cardCreation;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(decimal amount)
        {
            try
            {
                Card card = await _cardCreation.CreateCard(amount);
                await _cardsRepository.Add(card);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("pay")]
        public async Task<IActionResult> Pay([FromBody] Payment payment)
        {
            Card card = await _cardsRepository.GetCard(payment.CardNumber);

            if (card == null)
                return NotFound("Card not found.");

            decimal fee = _ufe.GetRandomFee();
            decimal totalAmount = payment.Amount + fee;

            if (card.Balance < totalAmount)
                return BadRequest("Insufficient balance.");

            card.Balance -= totalAmount;
            await _cardsRepository.Update(card);
            return Ok("Payment successful. Fee: " + Math.Round(fee, 2));
        }

        [HttpGet]
        public async Task<IActionResult> GetCardBalance(long cardNumber)
        {
            // Find the card with the provided card number.
            Card card = await _cardsRepository.GetCard(cardNumber);

            if (card == null)
                return NotFound("Card not found.");

            return Ok("Card balance: " + Math.Round(card.Balance, 2));
        }
    }
}
