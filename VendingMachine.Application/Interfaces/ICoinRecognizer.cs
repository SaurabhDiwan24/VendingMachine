using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Enums;
using VendingMachine.Domain.BusinessObjects;


namespace VendingMachine.Application.Interfaces
{
    public interface ICoinRecognizer
    {
        CoinType RecognizeCoin(Coin coin);
    }
}
