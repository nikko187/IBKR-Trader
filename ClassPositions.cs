using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IBKR_Trader
{
    internal class ClassPositions
    {
        public List<string> GetPNLData(double tick_price, int my_position, double total_realized, double my_average)
        {
            try
            {
                if (my_position > 0)
                {
                    double total_unrealized = Math.Round((tick_price - my_average) * my_position, 2);
                    double total_marked = Math.Round(total_realized + total_unrealized, 2);

                    List<string> ret = new List<string>();
                    ret.Add(Convert.ToString(total_unrealized));
                    ret.Add(total_marked.ToString());

                    return ret;
                }
                else if (my_position < 0)
                {
                    my_position = Math.Abs(my_position);
                    double total_unrealized = Math.Round((my_average - tick_price) * my_position, 2);
                    double total_marked = Math.Round(total_realized + total_unrealized, 2);

                    List<string> ret = new List<string>();
                    ret.Add(Convert.ToString(total_unrealized));  // convert to string variable
                    ret.Add(total_marked.ToString());   // convert to string variable also

                    return ret;
                }
            }
            catch (Exception)
            {

            }
            throw new NotImplementedException();
        }
    }
}
