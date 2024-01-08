using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBKR_Trader
{
    internal class tnsData
    {
        public string Time {  get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public tnsData(string time, double price, string size)
        {
            Time = time;
            Price = price;
            Size = size;
        }
    }
}
