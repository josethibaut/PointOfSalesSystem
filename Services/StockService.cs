namespace PointOfSalesSystem.Services { public class StockService { public int CalculateStockAfterSale(int stock, int quantitySold) => stock - quantitySold; } }