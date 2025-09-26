public static class Stock
{
    public static void Main(string[] args)
    {
        // Create a new stock instance
        StockExchange appleStock = new StockExchange("AAPL", 150.00m);

        // Subscribe multiple investors to the stock price change event
        appleStock.StockPriceChanged += Investor1_StockPriceChanged;
        appleStock.StockPriceChanged += Investor2_StockPriceChanged;

        // Simulate stock price changes
        appleStock.Price = 155.00m;
        appleStock.Price = 160.00m;
    }

    // Event handler method for Investor 1
    private static void Investor1_StockPriceChanged(object sender, StockPriceChangedEventArgs e)
    {
        Console.WriteLine($"Investor 1: The price of {e.StockSymbol} changed to {e.NewPrice:C}");
    }

    // Event handler method for Investor 2
    private static void Investor2_StockPriceChanged(object sender, StockPriceChangedEventArgs e)
    {
        Console.WriteLine($"Investor 2: The price of {e.StockSymbol} changed to {e.NewPrice:C}");
    }
}

public class StockExchange(string symbol, decimal initialPrice)
{
    // Delegate for stock price change event handler
    public delegate void StockPriceChangedHandler(object sender, StockPriceChangedEventArgs e);

    // Event for notifying stock price changes
    public event StockPriceChangedHandler StockPriceChanged = delegate { };

    private decimal price = initialPrice;
    public string Symbol { get; } = symbol;

    // Property for updating the stock price and raising the event
    public decimal Price
    {
        get { return price; }
        set
        {
            if (price != value)
            {
                price = value;
                var stockPriceChangedEvent = new StockPriceChangedEventArgs(Symbol, value);
                OnStockPriceChanged(stockPriceChangedEvent);
            }
        }
    }

    // Method for raising the stock price change event
    protected virtual void OnStockPriceChanged(StockPriceChangedEventArgs e)
    {
        StockPriceChanged.Invoke(this, e);
    }
}

public class StockPriceChangedEventArgs : EventArgs
{
    public string StockSymbol { get; }
    public decimal NewPrice { get; }

    public StockPriceChangedEventArgs(string stockSymbol, decimal newPrice)
    {
        StockSymbol = stockSymbol;
        NewPrice = newPrice;
    }
    
}
