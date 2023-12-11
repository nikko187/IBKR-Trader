using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IBApi;

namespace IBKR_Trader
{
    public class TNS
    {
        public string Time { get; set; }
        public double Price { get; set; }
        public decimal Size { get; set; }

        public TNS(string time, double price, decimal size)
        {
            Time = time;
            Price = price;
            Size = size;
        }
    }
}
