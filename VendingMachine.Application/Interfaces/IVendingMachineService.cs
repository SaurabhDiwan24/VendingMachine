using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.BusinessObjects;

namespace VendingMachine.Application.Interfaces
{

    public interface IVendingMachineService
    {
        void InsertCoin(Coin coin);
        void SelectProduct(string productName);
        string GetDisplayMessage();
    }

}
