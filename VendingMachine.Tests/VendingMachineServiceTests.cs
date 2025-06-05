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
        public void SelectProduct_ShouldDispense_WhenEnoughMoney()
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
        }

        [Fact]
        public void SelectProduct_ShouldShowPrice_WhenNotEnoughMoney()
        {
            var mock = new Mock<ICoinRecognizer>();
            mock.Setup(x => x.RecognizeCoin(It.IsAny<Coin>())).Returns(CoinType.Dime);

            var service = new VendingMachineService(mock.Object);
            service.InsertCoin(new Coin(2.268, 17.91));

            service.SelectProduct("Cola");

            Assert.Equal("PRICE $1.00", service.GetDisplayMessage());
        }

    }
}