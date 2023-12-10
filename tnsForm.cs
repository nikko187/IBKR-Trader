using IBApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace IBKR_Trader
{
    public partial class tnsForm : Form
    {
        public static tnsForm instance;

        double _bid = 0;
        double _ask = 0;

        public tnsForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void tnsForm_Load(object sender, EventArgs e)
        {
            // ibClient.form2 = (tnsForm)Application.OpenForms[0];
        }


        delegate void TimeAndSalesCallback(string tickstring);
        public void TimeAndSalesTickString(string tickstring)
        {
            if (datagridviewTns.InvokeRequired)
            {
                try
                {
                    TimeAndSalesCallback d = new TimeAndSalesCallback(TimeAndSalesTickString);
                    Invoke(d, new object[] { tickstring });
                }
                catch (Exception) { }

            }
            else
            {
                try
                {
                    _bid = Convert.ToDouble(Form1.instance.strBid);
                    _ask = Convert.ToDouble(Form1.instance.strAsk);

                    string[] timeandsales = tickstring.Split(';');

                    double price = Convert.ToDouble(timeandsales[0]);
                    double size = Convert.ToDouble(timeandsales[1]) * 100;
                    double time = Convert.ToDouble(timeandsales[2]);

                    string strSize = size.ToString("#,##0");

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    epoch = epoch.AddMilliseconds(time);
                    epoch = epoch.AddHours(-5);

                    string strTime = epoch.ToString("H:mm:ss");

                    datagridviewTns.Rows.Insert(0, strTime, price, strSize);

                    if (price >= _ask)
                    {
                        datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(0, 160, 0);
                        datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.Gainsboro;
                    }
                    else if (price <= _bid)
                    {
                        datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.DarkRed;
                        datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.Gainsboro;
                    }
                    else
                    {
                        datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.Black;
                        datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.LightGray;
                    }
                }
                catch (Exception) { }
            }
        }

    }
}
