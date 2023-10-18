using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Drawing.Text;
using IBApi;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Xml.Linq;

/**** Simplified TickByTick Time and Sales ONLY ****/

namespace IBKR_Trader
{
    public partial class Form1 : Form
    {
        // ENABLES ABILITY TO SET WINDOW AS "ALWAYS ON TOP" OF OTHER WINDOWS.
        // METHOD WAY DOWN BELOW AS: cbAlwaysOnTop_CheckedChanged
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        // DELEGATE ENABLES ASYNCHRONOUS CALLS FOR SETTING TEXT PROPERTY ON LISTBOX
        delegate void SetTextCallback(string text);
        delegate void SetTextCallbackTickPrice(string _tickPrice);

        // Create ibClient object to represent the connection
        EWrapperImpl ibClient;

        public Form1()
        {
            InitializeComponent();

            // Instantiate the ibClient
            ibClient = new EWrapperImpl();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Auto-Click connect on launch - DISABLED because app does not launch if the port # is incorrect.
            // btnConnect.PerformClick();

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            numPort.ReadOnly = true;

            // fixes crash on clicking connect when already connected.
            if (ibClient.ClientSocket.IsConnected())
            {
                btnConnect.Text = "Connected";
                btnConnect.BackColor = Color.LightGreen;
                return;
            }
            else
            {
                try
                {
                    // Parameters to connect to TWS are:
                    // host       - IP address or host name of the host running TWS
                    // port       - listening port 7496 or 7497
                    // clientId   - client application identifier can be any number
                    int port = (int)numPort.Value;
                    ibClient.ClientSocket.eConnect("", port, 11);

                    var reader = new EReader(ibClient.ClientSocket, ibClient.Signal);
                    reader.Start();

                    new Thread(() =>
                    {
                        while (ibClient.ClientSocket.IsConnected())
                        {
                            ibClient.Signal.waitForSignal();
                            reader.processMsgs();
                        }
                    })
                    { IsBackground = true }.Start();

                    // Wait until the connection is completed
                    while (ibClient.NextOrderId <= 0) { }

                    // Set up the form object in the ewrapper
                    ibClient.myform = (Form1)Application.OpenForms[0];


                    // Subscribe to Group 4 within TWS
                    ibClient.ClientSocket.subscribeToGroupEvents(9002, 4);

                    getData();

                }
                catch (Exception)
                {
                    MessageBox.Show("Failure to connect.\r\nIn TWS API settings, please make sure ActiveX and Socket Clients is enabled, and the Port number is correct. Disable Read-Only to trade.");
                }
            }
        }

        private void getData()
        {
            if (ibClient.ClientSocket.IsConnected() == true)
            {
                btnConnect.Text = "Connected";
                btnConnect.BackColor = Color.LightGreen;
            }
            else if (ibClient.ClientSocket.IsConnected() == false)
            {
                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.Gainsboro;
            }
            ibClient.ClientSocket.cancelMktData(1);
            // clears contents of TnS when changing tickers
            listViewTns.Items.Clear();

            // Create a new contract to specify the security we are searching for
            IBApi.Contract contract = new IBApi.Contract();
            // Create a new TagValueList object (for API version 9.71 and later) 
            List<IBApi.TagValue> mktDataOptions = new List<IBApi.TagValue>();

            // Set the underlying stock symbol fromthe cbSymbol combobox            
            contract.Symbol = cbSymbol.Text;
            // Set the Security type to STK for a Stock, FUT for Futures
            contract.SecType = "STK";
            // Use "SMART" as the general exchange, for Futures use "GLOBEX"
            contract.Exchange = "SMART";
            // Set the primary exchange (sometimes called Listing exchange)
            // Use either NYSE or ISLAND. For futures use "GLOBEX"
            contract.PrimaryExch = "ISLAND";
            // Set the currency to USD
            contract.Currency = "USD";

            // If using delayed market data subscription un-comment 
            // the line below to request delayed data
            // ibClient.ClientSocket.reqMarketDataType(1);  // delayed data = 3 live = 1
            ibClient.ClientSocket.reqMktData(1, contract, "375", false, false, mktDataOptions);

            // request contract details based on contract that was created above
            // ibClient.ClientSocket.reqContractDetails(88, contract);

        }
        double Bid = 0;
        double Ask = 0;
        public void AddTextBoxItemTickPrice(string _tickPrice)
        {
            /*if (tbLast.InvokeRequired)
            {
                SetTextCallbackTickPrice d = new SetTextCallbackTickPrice(AddTextBoxItemTickPrice);
                try
                {
                    this.Invoke(d, new object[] { _tickPrice });
                }
                catch (Exception e)
                {
                    Console.WriteLine("This is from tickPrice", e);
                }
            }
            else*/
            {
                string[] tickerPrice = new string[] { _tickPrice };
                tickerPrice = _tickPrice.Split(',');

                if (Convert.ToInt32(tickerPrice[0]) == 1)
                {
                    if (Convert.ToInt32(tickerPrice[1]) == 2)  // Delayed Ask 67, realtime is tickerPrice == 2
                    {
                        // Add the text string to the list box
                        Ask = Convert.ToDouble(tickerPrice[2]);
                    }
                    else if (Convert.ToInt32(tickerPrice[1]) == 1)  // Delayed Bid 66, realtime is tickerPrice == 1
                    {
                        // Add the text string to the list box
                        Bid = Convert.ToDouble(tickerPrice[2]);
                    }
                }
            }
        }
        delegate void SetTextCallbackTickString(string _tickString);

        // TIME AND SALES CONFIG
        public void AddListViewItemTickString(string _tickString)
        {
            if (listViewTns.InvokeRequired)
            {
                try
                {
                    SetTextCallbackTickString d = new SetTextCallbackTickString(AddListViewItemTickString);
                    this.Invoke(d, new object[] { _tickString });
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
                    string[] listTimeSales = _tickString.Split(';');

                    // get the first value form the list convert it to a double this value is the last price
                    double last_price = Convert.ToDouble(listTimeSales[0]);

                    // Proper way to adapt SIZE from tickstring data value and get rid of trailing zeroes.
                    double size = Convert.ToDouble(listTimeSales[1]);
                    string strShareSize = size.ToString("#0");

                    // TIME from tickstring data value
                    double trade_time = Convert.ToDouble(listTimeSales[2]);

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    epoch = epoch.AddMilliseconds(trade_time);
                    epoch = epoch.AddHours(-4);   //Daylight saving time use -4 Summer otherwise use -5 Winter

                    string strSaleTime = epoch.ToString("HH:mm:ss");  // formatting for time

                    ListViewItem lx = new ListViewItem();

                    // if the last price is the same as the ask
                    if (last_price >= Ask)
                    {
                        lx.BackColor = Color.SeaGreen; // listview foreground color
                        lx.Text = listTimeSales[0]; // last price
                        lx.SubItems.Add(strShareSize); // share size
                        lx.SubItems.Add(strSaleTime); // time
                        listViewTns.Items.Insert(0, lx); // use Insert instead of Add listView.Items.Add(li); 
                    }
                    // if the last price is the same as the bid
                    else if (last_price <= Bid)
                    {
                        lx.BackColor = Color.DarkRed;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        // lbData.Items.Insert(0, strSaleTime);
                    }
                    // if the last price in between the bid and ask.
                    else if (last_price > Bid && last_price < Ask)
                    {
                        lx.ForeColor = Color.Silver;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        // lbData.Items.Add(epoch);
                    }
                    else
                    {
                        lx.ForeColor = Color.White;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);
                    }
                }
                catch (Exception)
                {
                    // Debug.WriteLine("TnS error: " + e);
                }
            }
        }
        /* DISABLED TICK BY TICK
        double theBid = 0;
        double theAsk = 0;
        delegate void SetTextCallbackBidAskTicks(double bidTick, double askTick);

        
        public void BidAskTick(double bidTick, double askTick)
        {
            theAsk = askTick;
            theBid = bidTick;

        }

        delegate void SetTextCallbackTickByTick(string time, double price, decimal size);
        public void TickByTick(string time, double price, decimal size)
        {
            if (listViewTns.InvokeRequired)
            {
                try
                {
                    SetTextCallbackTickByTick d = new SetTextCallbackTickByTick(TickByTick);
                    this.Invoke(d, new object[] { time, price, size });
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    // Proper way to adapt SIZE from tickstring data value and get rid of trailing zeroes.
                    string strShareSize = size.ToString("#,##0");

                    string strSaleTime = time; //epoch.ToString("h:mm:ss:ff");  // formatting for time

                    ListViewItem lx = new ListViewItem();

                    // if the last price is the same as the ask change the color to lime
                    if (price >= theAsk)
                    {
                        lx.BackColor = Color.FromArgb(0, 170, 0); // listview foreground color
                        lx.Text = price.ToString(); // last price
                        lx.SubItems.Add(strShareSize); // share size
                        lx.SubItems.Add(strSaleTime); // time
                        listViewTns.Items.Insert(0, lx); // use Insert instead of Add listView.Items.Add(li); 
                    }
                    // if the last price is the same as the bid change the color to red
                    else if (price <= theBid)
                    {
                        lx.BackColor = Color.FromArgb(150, 0, 0);
                        lx.Text = price.ToString();
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        // lbData.Items.Insert(0, strSaleTime);
                    }
                    // if the last price in greater than the mean price and
                    // less than the ask price change the color to lime green
                    else if (price > theBid && price < theAsk)
                    {
                        lx.ForeColor = Color.Silver;
                        lx.Text = price.ToString();
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        // lbData.Items.Add(epoch);
                    }
                    else
                    {
                        lx.ForeColor = Color.White;
                        lx.Text = price.ToString();
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);
                    }
                }
                catch (Exception)
                {
                    // lbData.Items.Insert(0, "TnS error: " + g);
                }
            }
        }
        TICK BY TICK DISABLED */
        private void cbSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            getData();
        }

        private void cbSymbol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            ibClient.ClientSocket.eDisconnect();
            btnConnect.Text = "Connect";
            btnConnect.BackColor = Color.Gainsboro;
            numPort.ReadOnly = false;
        }

        private void cbSymbol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbSymbol.SelectionStart = 0;
                cbSymbol.SelectionLength = cbSymbol.Text.Length;

                e.SuppressKeyPress = true;

                string name = cbSymbol.Text;

                // adds the security symbol to the dropdown list in the symbol combobox
                if (!cbSymbol.Items.Contains(name))
                {
                    cbSymbol.Items.Add(name);
                }
                cbSymbol.SelectAll();

                getData();
            }
        }

        private void ToolstripBorderToggle_Click(object sender, EventArgs e)
        {
            if (toolstripBorderToggle.Checked)
            {
                FormBorderStyle = FormBorderStyle.None;
            }
            else { FormBorderStyle = FormBorderStyle.Sizable; }
        }

        private void cbSymbol_DragDrop(object sender, DragEventArgs e)
        {
            cbSymbol.SelectionStart = 0;
            cbSymbol.SelectionLength = cbSymbol.Text.Length;

            string name = cbSymbol.Text;

            // adds the security symbol to the dropdown list in the symbol combobox
            if (!cbSymbol.Items.Contains(name))
            {
                cbSymbol.Items.Add(name);
            }
            cbSymbol.SelectAll();

            getData();
        }
        private void ToolstripTickByTick(object sender, EventArgs e)
        {

        }
    }
}