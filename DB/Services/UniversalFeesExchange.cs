using DB.Contracts;

namespace DB.Services;

public class UniversalFeesExchange : IUniversalFeesExchange
{
    private List<decimal> feeHistory;
    private readonly Timer feeUpdateTimer;
    private readonly Random random;

    public UniversalFeesExchange()
    {
        random = new Random();
        feeHistory = new List<decimal>() { (decimal)random.NextDouble() };
        feeUpdateTimer = new Timer(UpdateFee, null, 0, 3600000);
    }

    private void UpdateFee(object state)
    {
        // Update the fee every hour by selecting a new random decimal between 0 and 2.
        decimal randomDecimal = (decimal)random.NextDouble() * 2;
        decimal newFee = feeHistory.Last() * randomDecimal;
        feeHistory.Add(newFee);
    }
    public decimal GetRandomFee()
    {
        return feeHistory.Last();
    }
}
