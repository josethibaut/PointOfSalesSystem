using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;

public class LoyaltyService
{
    private readonly ApplicationDbContext _context;

    public LoyaltyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddLoyaltyPoints(int customerId, decimal purchaseAmount)
    {
        int points = (int)(purchaseAmount / 10); // Example: 1 point per $10
        var customer = _context.Customers.Find(customerId);
        if (customer != null)
        {
            customer.LoyaltyPoints += points;
            _context.LoyaltyTransactions.Add(new LoyaltyTransaction
            {
                CustomerId = customerId,
                PointsEarned = points,
                TransactionDate = DateTime.Now
            });
            _context.SaveChanges();
        }
    }

    public bool RedeemPoints(int customerId, int pointsToRedeem)
    {
        var customer = _context.Customers.Find(customerId);
        if (customer != null && customer.LoyaltyPoints >= pointsToRedeem)
        {
            customer.LoyaltyPoints -= pointsToRedeem;
            _context.LoyaltyTransactions.Add(new LoyaltyTransaction
            {
                CustomerId = customerId,
                PointsRedeemed = pointsToRedeem,
                TransactionDate = DateTime.Now
            });
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
