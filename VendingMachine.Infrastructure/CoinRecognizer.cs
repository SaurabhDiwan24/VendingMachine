
using VendingMachine.Application.Interfaces;
using VendingMachine.Domain.Enums;
using VendingMachine.Domain.BusinessObjects;


namespace VendingMachine.Infrastructure
{
    public class CoinRecognizer : ICoinRecognizer
    {
        public CoinType RecognizeCoin(Coin coin)
        {

            if (coin.Weight == 5.0 && coin.Size == 21.21) 
                return CoinType.Nickel;
            if (coin.Weight == 2.268 && coin.Size == 17.91) 
                return CoinType.Dime;
            if (coin.Weight == 5.67 && coin.Size == 24.26)
                return CoinType.Quarter;

            return CoinType.Penny;
        }

    }
}
