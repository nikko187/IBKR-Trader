using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBKR_Trader
{
    public partial class tnsForm : Form
    {
        public static tnsForm instance;
        double _bid = 0;
        double _ask = 0;
        double price;
        public tnsForm()
        {
            InitializeComponent();
            instance = this;
        }

        BindingList<tnsData> _tns;

        private void tnsForm_Load(object sender, EventArgs e)
        {
            _tns = new BindingList<tnsData>();
            datagridviewTns.DataSource = _tns;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        delegate void TimeAndSalesCallback(string tickstring);
        public void TimeAndSales(string tickstring)
        {
            if (datagridviewTns.InvokeRequired)
            {
                try
                {
                    TimeAndSalesCallback d = new TimeAndSalesCallback(TimeAndSales);
                    this.Invoke(d, new object[] { tickstring });
                }
                catch (Exception ex) { MessageBox.Show("Err: " + ex.Message); }
            }
            else
            {
                try
                {
                    _bid = Convert.ToDouble(Form1.instance.strBid);
                    _ask = Convert.ToDouble(Form1.instance.strAsk);

                    string[] timeandsales = tickstring.Split(";");
                    price = Convert.ToDouble(timeandsales[0]);
                    double size = Convert.ToDouble(timeandsales[1]) * 100;
                    double time = Convert.ToDouble(timeandsales[2]);

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    epoch = epoch.AddMilliseconds(time);
                    epoch = epoch.AddHours(-5);
                    string strTime = epoch.ToString("HH:mm:ss");

                    string strSize;
                    if (!toolStripSizeFilter.Checked)
                    {
                        strSize = size.ToString("#,##0");
                        _tns.Insert(0, new tnsData(strTime, price, strSize));
                    }
                    else
                    {
                        if (size > Convert.ToDouble(toolStripSizeValue.Text))
                        {
                            strSize = size.ToString("#,##0");
                            _tns.Insert(0, new tnsData(strTime, price, strSize));
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void datagridviewTns_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (price >= _ask)
            {
                datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(0, 150, 10);
                datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.Gainsboro;
            }
            else if (price <= _bid)
            {
                datagridviewTns.Rows[0].DefaultCellStyle.BackColor = Color.DarkRed;
                datagridviewTns.Rows[0].DefaultCellStyle.ForeColor = Color.Gainsboro;
            }
        }

        public void clearTnsList()
        {
            if (_tns != null)
            {
                _tns.Clear();
            }
            else
                return;
        }

        private void tnsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1.instance.tnsFormIsOpen = false;
        }
    }
}
