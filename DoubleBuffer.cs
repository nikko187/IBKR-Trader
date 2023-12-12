using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IBKR_Trader
{
    internal class DoubleBuffered
    {
        //Set Double buffering on the Grid using reflection and the bindingflags enum.
        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | 
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        DataGridViewControlName, new object[] { true });
    }
}
