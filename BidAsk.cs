using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBKR_Trader
{
    internal class BidAsk
    {
        public double _bid { get; set; }
        public double _ask { get; set; }
        public BidAsk(double bid, double ask)
        {
            _bid = bid;
            _ask = ask;
        }
    }
}
