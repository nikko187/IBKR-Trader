using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBApi;
using IBKR_Trader;

namespace IBKR_Trader
{
    public partial class TimeAndSales : Form
    {
        private Connection twsConnection;

        private DataTable timeandsalesTable = new DataTable();

        public TimeAndSales(Connection connection)
        {
            InitializeComponent();
            twsConnection = connection;


            Func<string, DataColumn> toDataColumn = i => new DataColumn { ColumnName = i };
            timeandsalesTable.Columns.AddRange(new[] { "Time", "Price", "Size" }.Select(toDataColumn).ToArray());

        }

        private void TimeAndSales_Load(object sender, EventArgs e)
        {

        }

        delegate void SetTextCallbackTickString(string tickString);

        // TIME AND SALES CONFIG
        public void DataGridViewTickString(string tickString)
        {
            if (datagridviewTns.InvokeRequired)
            {
                try
                {
                    SetTextCallbackTickString d = new SetTextCallbackTickString(DataGridViewTickString);
                    this.Invoke(d, new object[] { tickString });
                }
                catch (Exception)
                {
                    // lbData.Items.Insert(0, "TickString Invoke error: " + f);
                }
            }
            else
            {
                try
                {

                    // Contains Last Price, Trade Size, Trade Time, Total Volume, VWAP, 
                    // single trade flag true, or false.
                    // 6 items all together
                    // example 701.28;1;1348075471534;67854;701.46918464;true
                    // extract each value from string and store it in a string list
                    string[] listTimeSales = tickString.Split(';');

                    // get the first value form the list convert it to a double this value is the last price
                    double price = Convert.ToDouble(listTimeSales[0]);

                    // Proper way to adapt SIZE from tickstring data value and get rid of trailing zeroes.
                    double size = Convert.ToDouble(listTimeSales[1]) * 100;
                    string strSize = size.ToString("#,##0");

                    // TIME from tickstring data value
                    double time = Convert.ToDouble(listTimeSales[2]);

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    epoch = epoch.AddMilliseconds(time);
                    epoch = epoch.AddHours(-5);   //Daylight saving time use -4 Summer otherwise use -5 Winter

                    string strSaleTime = epoch.ToString("HH:mm:ss");  // formatting for time

                    DataRow row = timeandsalesTable.NewRow();
                    row[0] = strSaleTime;
                    row[1] = price;
                    row[2] = strSize;
                    timeandsalesTable.Rows.InsertAt(row, 0);

                    /*
                    if (price >= Ask)
                    {
                        datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(0, 160, 0);
                        datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.White;

                    }
                    else if (price <= Bid)
                    {
                        datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.DarkRed;
                        datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.Black;
                        datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.LightGray;
                    }*/
                }
                catch (Exception h)
                {
                    MessageBox.Show(h.Message);
                }
            }
        }
    }
}
