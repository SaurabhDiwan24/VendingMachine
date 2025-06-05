using Moq;
using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Services;
using VendingMachine.Domain.BusinessObjects;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Tests
{
    public class VendingMachineServiceTests
    {

        [Fact]
        public void InsertCoin_ShouldUpdateCurrentAmount()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.Setup(x => x.RecognizeCoin(It.IsAny<Coin>())).Returns(CoinType.Quarter);

            var service = new VendingMachineService(mock.Object);
            service.InsertCoin(new Coin(5.67, 24.26));

            Assert.Equal("$0.25", service.GetDisplayMessage());
        }

        [Fact]
        public void SelectProduct_ShouldDispense_WhenEnoughMoney_ThenResetDisplay()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.Setup(x => x.RecognizeCoin(It.IsAny<Coin>())).Returns(CoinType.Quarter);

            var service = new VendingMachineService(mock.Object);
            for (int i = 0; i < 4; i++)
            {
                service.InsertCoin(new Coin(5.67, 24.26));
            }

            service.SelectProduct("Cola");

            Assert.Equal("THANK YOU", service.GetDisplayMessage());
            Assert.Equal("INSERT COIN", service.GetDisplayMessage()); 
        }

        [Fact]
        public void SelectProduct_ShouldShowPrice_WhenNotEnoughMoney_ThenShowCurrentAmount()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.Setup(x => x.RecognizeCoin(It.IsAny<Coin>())).Returns(CoinType.Dime);

            var service = new VendingMachineService(mock.Object);
            service.InsertCoin(new Coin(2.268, 17.91)); 

            service.SelectProduct("Cola");

            Assert.Equal("PRICE $1.00", service.GetDisplayMessage());
            Assert.Equal("$0.10", service.GetDisplayMessage()); 
        }

        [Fact]
        public void SelectProduct_ShouldShowPrice_WhenNoMoney_ThenShowInsertCoin()
        {
            var mock = new Mock<ICoinRecognizer>();
            var service = new VendingMachineService(mock.Object);

            service.SelectProduct("Chips");

            Assert.Equal("PRICE $0.50", service.GetDisplayMessage());
            Assert.Equal("INSERT COIN", service.GetDisplayMessage()); 
        }

        [Fact]
        public void SelectInvalidProduct_ShouldShowInvalidProduct_ThenShowCurrentAmount()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.Setup(x => x.RecognizeCoin(It.IsAny<Coin>())).Returns(CoinType.Quarter);

            var service = new VendingMachineService(mock.Object);
            service.InsertCoin(new Coin(5.67, 24.26)); 

            service.SelectProduct("Water");

            Assert.Equal("INVALID PRODUCT", service.GetDisplayMessage());
            Assert.Equal("$0.25", service.GetDisplayMessage()); 
        }


        [Fact]
        public void InsertPenny_ShouldAddToCoinReturn()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.Setup(x => x.RecognizeCoin(It.IsAny<Coin>())).Returns(CoinType.Penny);

            var service = new VendingMachineService(mock.Object);
            var penny = new Coin(2.5, 19.05);
            service.InsertCoin(penny);

            var coinReturn = service.GetCoinReturn();

            Assert.Single(coinReturn);
            Assert.Equal(penny.Weight, coinReturn[0].Weight);
            Assert.Equal(penny.Size, coinReturn[0].Size);
        }


        [Fact]
        public void InsertQuarterAndPenny_ShouldOnlyAddPennyToCoinReturn()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.SetupSequence(x => x.RecognizeCoin(It.IsAny<Coin>()))
            .Returns(CoinType.Quarter)
            .Returns(CoinType.Penny);

            var service = new VendingMachineService(mock.Object);

            var quarter = new Coin(5.67, 24.26);
            var penny = new Coin(2.5, 19.05);

            service.InsertCoin(quarter);
            service.InsertCoin(penny);

            var coinReturn = service.GetCoinReturn();

            Assert.Single(coinReturn);
            Assert.Equal(penny.Weight, coinReturn[0].Weight);
            Assert.Equal("$0.25", service.GetDisplayMessage());
        }


    }

}
