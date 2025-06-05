
using VendingMachine.Application.Interfaces;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;
using VendingMachine.Domain.BusinessObjects;


namespace VendingMachine.Application.Services
{

    public class VendingMachineService : IVendingMachineService
    {
        private readonly ICoinRecognizer _coinRecognizer;
        private decimal _currentAmount;
        private string _displayMessage;
        private readonly Dictionary<string, Product> _products;

        public VendingMachineService(ICoinRecognizer coinRecognizer)
        {
            _coinRecognizer = coinRecognizer;
            _currentAmount = 0;
            _displayMessage = "INSERT COIN";
            _products = new(){
                     { "Cola", new Product("Cola", 1.00m) },
                     { "Chips", new Product("Chips", 0.50m) },
                     { "Candy", new Product("Candy", 0.65m) }
            };
        }

        public void InsertCoin(Coin coin)
        {
            var coinType = _coinRecognizer.RecognizeCoin(coin);
            switch (coinType)
            {
                case CoinType.Nickel: 
                    _currentAmount += 0.05m; 
                    break;
                case CoinType.Dime: 
                    _currentAmount += 0.10m; 
                    break;
                case CoinType.Quarter: 
                    _currentAmount += 0.25m; 
                    break;
                case CoinType.Penny: 
                    break; 
            }
            _displayMessage = _currentAmount > 0 ? _currentAmount.ToString("C") : "INSERT COIN";
        }

        public void SelectProduct(string productName)
        {
            if (_products.TryGetValue(productName, out var product))
            {
                if (_currentAmount >= product.Price)
                {
                    _currentAmount -= product.Price;
                    _displayMessage = "THANK YOU";
                }
                else
                {
                    _displayMessage = $"PRICE {product.Price:C}";
                }
            }
            else
            {
                _displayMessage = "INVALID PRODUCT";
            }
        }

        public string GetDisplayMessage() => _displayMessage;
    }

}
