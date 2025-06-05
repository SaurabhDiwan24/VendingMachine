using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain.BusinessObjects
{
    public class Coin
    {

        public double Weight { get; set; }
        public double Size { get; set; }

        public Coin(double weight, double size)
        {
            Weight = weight;
            Size = size;
        }

    }
}
