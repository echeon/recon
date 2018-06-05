namespace recon
{
    public class Stock
    {
        public string Symbol { get; }
        public double NumShares { get; private set; }

        public Stock(string symbol, double numShares)
        {
            Symbol = symbol;
            NumShares = numShares;
        }

        public void Buy(double numShares)
        {
            this.NumShares += numShares;
        }

        public void Sell(double numShares)
        {
            this.NumShares -= numShares;
        }
    }
}