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
using System.Security.Policy;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;


/****** PROPOSED ADDITIONS, REVISIONS, AND FIXES ******/


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

        int order_id = 0;
        int timer1_counter = 6;
        int myContractId;


        public void AddListBoxItem(string text)
        {
            // See if a new invocation is required from a different thread            
            if (this.lbData.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddListBoxItem);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                // Add the text string to the list box
                this.lbData.Items.Add(text);
            }

        }

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


            // dataGridView3 Properties
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.BackgroundColor = SystemColors.AppWorkspace;
            dataGridView1.DefaultCellStyle.BackColor = Color.Black;
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;

            // Columns
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[8].Width = 60;
            dataGridView1.Columns[9].Width = 60;
            dataGridView1.Columns["colCancel"].DefaultCellStyle.BackColor = Color.DodgerBlue; // or SystemColors.Highlight;
            dataGridView1.Columns["colCancel"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 192, 192);
            dataGridView1.Columns["colCancel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

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
                    ibClient.ClientSocket.eConnect("", port, 0);

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

                    // Update order ID value
                    order_id = ibClient.NextOrderId;

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

        delegate void SetTextCallbackContractId(int contractId);

        public void AddTextBoxItemConId(int contractId)
        {
            if (this.cbSymbol.InvokeRequired)
            {
                try
                {
                    SetTextCallbackContractId d = new SetTextCallbackContractId(AddTextBoxItemConId);
                    this.Invoke(d, new object[] { contractId });
                }
                catch (Exception e)
                {
                    Console.WriteLine("this is from _tickString time and sales ", e);
                }
            }
            else
            {
                myContractId = contractId;
            }
        }
        public void AddTextBoxItemTickPrice(string _tickPrice)
        {
            if (this.tbLast.InvokeRequired)
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
            else
            {
                string[] tickerPrice = new string[] { _tickPrice };
                tickerPrice = _tickPrice.Split(',');

                if (Convert.ToInt32(tickerPrice[0]) == 1)
                {
                    if (Convert.ToInt32(tickerPrice[1]) == 4)// Delayed Last 68, realtime is tickerPrice == 4
                    {
                        // Add the text string to the list box
                        this.tbLast.Text = tickerPrice[2];
                    }
                    else if (Convert.ToInt32(tickerPrice[1]) == 2)  // Delayed Ask 67, realtime is tickerPrice == 2
                    {
                        // Add the text string to the list box
                        this.tbAsk.Text = tickerPrice[2];
                    }
                    else if (Convert.ToInt32(tickerPrice[1]) == 1)  // Delayed Bid 66, realtime is tickerPrice == 1
                    {
                        // Add the text string to the list box
                        this.tbBid.Text = tickerPrice[2];
                    }
                    double spread = Math.Round(Convert.ToDouble(tbAsk.Text) - Convert.ToDouble(tbBid.Text), 2);
                    labelSpread.Text = spread.ToString("#0.00");
                    PercentChange(null, null);
                    UpdateRiskQty(null, null);
                }

                switch (Convert.ToInt32(tickerPrice[0]))
                {
                    case 20:
                        if (Convert.ToInt32(tickerPrice[1]) == 4)
                        {
                            try
                            {
                                // Create an object from a class
                                ClassPositions myList20 = new ClassPositions();

                                double tick_price = Convert.ToDouble(tickerPrice[2]);
                                // round the tick_price to 2 decimal places
                                tick_price = Math.Round(tick_price, 2);
                                // gets the position value from the data grid and converts it to an integer
                                int my_position = Convert.ToInt32(dataGridView4[1, 0].Value);
                                // gets the realized value from the data grid and convert it to a double variable
                                double total_realized = Convert.ToDouble(dataGridView4[4, 0].Value);
                                // gets the average price from the data grid convert it to a double variable my_average
                                double my_average = Convert.ToDouble(dataGridView4[2, 0].Value);

                                // call a method within a class
                                List<string> help = myList20.GetPNLData(tick_price, my_position, total_realized, my_average);

                                // returned help string list values from our class
                                dataGridView4[3, 0].Value = help[0]; // unrealized
                                dataGridView4[5, 0].Value = help[1]; // marked

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception " + e);
                            }
                            // calls the method to calculate the position total 
                            positionTotal();
                            break;
                        }
                        break;
                    case 21:
                        if (Convert.ToInt32(tickerPrice[1]) == 4)
                        {
                            try
                            {
                                // Create an object from a class
                                ClassPositions myList21 = new ClassPositions();

                                double tick_price = Convert.ToDouble(tickerPrice[2]);
                                // round the tick_price to 2 decimal places
                                tick_price = Math.Round(tick_price, 2);
                                // gets the position value from the data grid and converts it to an integer
                                int my_position = Convert.ToInt32(dataGridView4[1, 1].Value);
                                // gets the realized value from the data grid and convert it to a double variable
                                double total_realized = Convert.ToDouble(dataGridView4[4, 1].Value);
                                // gets the average price from the data grid convert it to a double variable my_average
                                double my_average = Convert.ToDouble(dataGridView4[2, 1].Value);

                                // call a method within a class
                                List<string> help = myList21.GetPNLData(tick_price, my_position, total_realized, my_average);

                                // returned help string list values from our class
                                dataGridView4[3, 1].Value = help[0]; // unrealized
                                dataGridView4[5, 1].Value = help[1]; // marked

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception " + e);
                            }
                            // calls the method to calculate the position total 
                            positionTotal();
                            break;
                        }
                        break;
                    case 22:
                        if (Convert.ToInt32(tickerPrice[1]) == 4)
                        {
                            try
                            {
                                // Create an object from a class
                                ClassPositions myList22 = new ClassPositions();

                                double tick_price = Convert.ToDouble(tickerPrice[2]);
                                // round the tick_price to 2 decimal places
                                tick_price = Math.Round(tick_price, 2);
                                // gets the position value from the data grid and converts it to an integer
                                int my_position = Convert.ToInt32(dataGridView4[1, 2].Value);
                                // gets the realized value from the data grid and convert it to a double variable
                                double total_realized = Convert.ToDouble(dataGridView4[4, 2].Value);
                                // gets the average price from the data grid convert it to a double variable my_average
                                double my_average = Convert.ToDouble(dataGridView4[2, 2].Value);

                                // call a method within a class
                                List<string> help = myList22.GetPNLData(tick_price, my_position, total_realized, my_average);

                                // returned help string list values from our class
                                dataGridView4[3, 2].Value = help[0]; // unrealized
                                dataGridView4[5, 2].Value = help[1]; // marked

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception " + e);
                            }
                            // calls the method to calculate the position total 
                            positionTotal();
                            break;
                        }
                        break;
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

            // clears contents of TnS when changing
            listViewTns.Items.Clear();
            DateTime now = DateTime.Now;

            // account info and request account updates and current positions.
            string account_number = "D005";
            ibClient.ClientSocket.reqAccountUpdates(true, account_number);
            ibClient.ClientSocket.reqPositions();

            // ibClient.ClientSocket.cancelTickByTickData(1);
            ibClient.ClientSocket.cancelMktData(1); // cancel market data
            ibClient.ClientSocket.cancelRealTimeBars(0);  // not needed yet.

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
            ibClient.ClientSocket.reqMarketDataType(1);  // delayed data = 3 live = 1

            // For API v9.72 and higher, add one more parameter for regulatory snapshot
            ibClient.ClientSocket.reqMktData(1, contract, "233, 236, 165", false, false, mktDataOptions);

            // Tick by tick TESTING -- SUCCESS!
            //ibClient.ClientSocket.reqTickByTickData(1, contract, "AllLast", 200,false);

            // request contract details based on contract that was created above
            ibClient.ClientSocket.reqContractDetails(88, contract);

            // requests all open order in account
            ibClient.ClientSocket.reqAllOpenOrders();

            timer1.Start();

        }

        /*
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
                catch (Exception f)
                {
                    lbData.Items.Insert(0, "TickByTick Invoke error: " + f);
                }
            }
            else
            {
                try
                {
                    // get the bid price from the textbox Bid
                    double theBid = Convert.ToDouble(tbBid.Text);
                    // gets the ask price from the textbox Ask
                    double theAsk = Convert.ToDouble(tbAsk.Text);

                    // get the first value form the list convert it to a double this value is the last price

                    // Proper way to adapt SIZE from tickstring data value and get rid of trailing zeroes.
                    string strShareSize = size.ToString("0.##");

                    // TIME from tickstring data value

                    //DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    //epoch = epoch.AddMilliseconds(time);
                    //epoch = epoch.AddHours(-4);   //Daylight saving time use -4 Summer otherwise use -5 Winter

                    string strSaleTime = time; //epoch.ToString("h:mm:ss:ff");  // formatting for time

                    // used to get midprice, was previously used for Time and Sales coloring. not anymore.
                    double myMeanPrice = ((theAsk - theBid) / 2);
                    double myMean = (theBid + myMeanPrice);

                    ListViewItem lx = new ListViewItem();

                    // if the last price is the same as the ask change the color to lime
                    if (price >= theAsk)
                    {
                        lx.BackColor = Color.OliveDrab; // listview foreground color
                        lx.Text = price.ToString(); // last price
                        lx.SubItems.Add(strShareSize); // share size
                        lx.SubItems.Add(strSaleTime); // time
                        listViewTns.Items.Insert(0, lx); // use Insert instead of Add listView.Items.Add(li); 
                    }
                    // if the last price is the same as the bid change the color to red
                    else if (price <= theBid)
                    {
                        lx.BackColor = Color.DarkRed;
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
                        lx.ForeColor = Color.LightGray;
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
        */

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
                catch (Exception f)
                {
                    lbData.Items.Insert(0, "TickString Invoke error: " + f);
                }
            }
            else
            {
                try
                {
                    // get the bid price from the textbox Bid
                    double theBid = Convert.ToDouble(tbBid.Text);
                    // gets the ask price from the textbox Ask
                    double theAsk = Convert.ToDouble(tbAsk.Text);

                    // Contains Last Price, Trade Size, Trade Time, Total Volume, VWAP, 
                    // single trade flag true, or false.
                    // 6 items all together
                    // example 701.28;1;1348075471534;67854;701.46918464;true
                    // extract each value from string and store it in a string list
                    string[] listTimeSales = _tickString.Split(';');

                    // get the first value form the list convert it to a double this value is the last price
                    double last_price = Convert.ToDouble(listTimeSales[0]);

                    // Proper way to adapt SIZE from tickstring data value and get rid of trailing zeroes.
                    double shares = Convert.ToDouble(listTimeSales[1]);
                    string strShareSize = shares.ToString("##0");

                    // TIME from tickstring data value
                    double trade_time = Convert.ToDouble(listTimeSales[2]);

                    // Current traded volume for the day.
                    double volume = Convert.ToDouble(listTimeSales[3]) * 100;
                    string voll = volume.ToString("#,##0");
                    labelVolume.Text = "Vol: " + voll;

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    epoch = epoch.AddMilliseconds(trade_time);
                    epoch = epoch.AddHours(-4);   //Daylight saving time use -4 Summer otherwise use -5 Winter

                    string strSaleTime = epoch.ToString("HH:mm:ss:ff");  // formatting for time

                    // used to get midprice, was previously used for Time and Sales coloring. not anymore.
                    //double myMeanPrice = ((theAsk - theBid) / 2);
                    //double myMean = (theBid + myMeanPrice);

                    ListViewItem lx = new ListViewItem();

                    // if the last price is the same as the ask change the color to lime
                    if (last_price >= theAsk)
                    {
                        lx.BackColor = Color.OliveDrab; // listview foreground color
                        lx.Text = listTimeSales[0]; // last price
                        lx.SubItems.Add(strShareSize); // share size
                        lx.SubItems.Add(strSaleTime); // time
                        listViewTns.Items.Insert(0, lx); // use Insert instead of Add listView.Items.Add(li); 
                    }
                    // if the last price is the same as the bid change the color to red
                    else if (last_price <= theBid)
                    {
                        lx.BackColor = Color.DarkRed;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        // lbData.Items.Insert(0, strSaleTime);
                    }
                    // if the last price in greater than the mean price and
                    // less than the ask price change the color to lime green
                    else if (last_price > theBid && last_price < theAsk)
                    {
                        lx.ForeColor = Color.LightGray;
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
                    // lbData.Items.Insert(0, "TnS error: " + g);
                }
            }
        }

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

        private void btnSell_Click(object sender, EventArgs e)
        {
            string side = "Sell";

            if (cbOrderType.Text is "SNAP MKT" or "MKT")
                numPrice.Value = Convert.ToDecimal(tbBid.Text);
            else if (cbOrderType.Text == "SNAP MID")
                numPrice.Value = (Convert.ToDecimal(tbAsk.Text) + Convert.ToDecimal(tbBid.Text)) / 2;
            else if (cbOrderType.Text == "SNAP PRIM")
                numPrice.Value = Convert.ToDecimal(tbAsk.Text);

            if (chkBracket.Checked)
            {
                send_bracket_order(side);
            }
            else
            {
                send_order(side);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            string side = "Buy";

            if (cbOrderType.Text is "SNAP MKT" or "MKT")
                numPrice.Value = Convert.ToDecimal(tbAsk.Text);
            else if (cbOrderType.Text == "SNAP MID")
                numPrice.Value = (Convert.ToDecimal(tbAsk.Text) + Convert.ToDecimal(tbBid.Text)) / 2;
            else if (cbOrderType.Text == "SNAP PRIM")
                numPrice.Value = Convert.ToDecimal(tbBid.Text);

            if (chkBracket.Checked)
            {
                send_bracket_order(side);
            }
            else
            {
                send_order(side);
            }

        }

        private bool BracketOrderExecuted = false;
        private bool takeProfitEnabled = false;

        public void send_bracket_order(string side)
        {
            // create contract
            IBApi.Contract contract = new IBApi.Contract();
            // set underlying stock symbol from cb Symbol combobox
            contract.Symbol = cbSymbol.Text;
            // Set security type to STK for Stock
            contract.SecType = "STK";
            // use SMART as general exchange
            contract.Exchange = cbMarket.Text;
            // Set primary exchange. Use either NYSE or ISLAND
            contract.PrimaryExch = "ISLAND";
            // sets currency to USD
            contract.Currency = "USD";

            // orderid, action, qty, entryprice, targetprice, stoploss
            string order_type = cbOrderType.Text;   // sets LMT or STP from box
            string action = side;   // sets BUY or SELL from button click
            double lmtPrice = Convert.ToDouble(numPrice.Text); // limit price from box
            double takeProfit = Convert.ToDouble(tbTakeProfit.Text);    // tp amount from text box
            double stopLoss = Convert.ToDouble(tbStopLoss.Text);    // stop loss from text box

            // Number of Share automatically calculated per $ Risk and Stop Loss distance.
            double quantity = Math.Floor((Convert.ToDouble(numRisk.Value)) / Math.Abs(lmtPrice - stopLoss));

            // side is either buy or sell. calls bracketorder function and stores results in list variable called bracket
            List<Order> bracket = BracketOrder(order_id++, action, quantity, lmtPrice, takeProfit, stopLoss, order_type, takeProfitEnabled);
            foreach (Order o in bracket)    // loops through each order in the list
                ibClient.ClientSocket.placeOrder(o.OrderId, contract, o);

            // increase order id by 2, for parent and stop loss, as to not use same order id twice and get an error
            if (takeProfitEnabled)
            {
                order_id += 3;
                string printBox = action + " " + quantity + " " + contract.Symbol + " at " + order_type + " " + lmtPrice + " Stop " + stopLoss + " and take profit " + takeProfit;
                lbData.Items.Insert(0, printBox);

            }
            else
            {
                order_id += 2;
                string printBox = action + " " + quantity + " " + contract.Symbol + " at " + order_type + " " + lmtPrice + " Stop " + stopLoss + " and take profit " + takeProfit;
                lbData.Items.Insert(0, printBox);
            }

            BracketOrderExecuted = true;
            chkBracket.Checked = false;
        }

        public static List<Order> BracketOrder(int parentOrderId, string action, double quantity, double limitPrice, double takeProfitLimitPrice, double stopLossPrice, string order_type, bool takeProfitEnabled)
        {
            //This will be our main or "parent" order
            Order parent = new Order();
            parent.OrderId = parentOrderId;
            parent.Action = action;  // "BUY" or "SELL"
            parent.OrderType = order_type;  // "LMT", "STP", or "STP LMT"
            parent.TotalQuantity = (decimal)quantity;
            parent.LmtPrice = limitPrice;
            parent.AuxPrice = 0.00;     // Sets Aux price to 0.00 (offset) for any of the SNAP orders.
            //The parent and children orders will need this attribute set to false to prevent accidental executions.
            //The last child (STP) will have it set to true
            parent.Transmit = false;


            // Profit Target order
            if (takeProfitEnabled)
            {
                Order takeProfit = new Order();
                takeProfit.OrderId = parent.OrderId + 1;
                takeProfit.Action = action.Equals("Buy") ? "Sell" : "Buy"; // if statement
                takeProfit.OrderType = "LMT";
                takeProfit.TotalQuantity = (decimal)quantity;
                takeProfit.LmtPrice = takeProfitLimitPrice;
                takeProfit.ParentId = parentOrderId;
                takeProfit.Transmit = false;

                // Stop loss order
                Order stopLoss = new Order();
                stopLoss.OrderId = parent.OrderId + 2;
                stopLoss.Action = action.Equals("Buy") ? "Sell" : "Buy";
                stopLoss.OrderType = "STP";     // or "STP LMT", then use the field below...
                                                // Stop trigger price
                                                // add stopLoss.LmtPrice here if you are going to use a stop limit order
                stopLoss.AuxPrice = stopLossPrice;
                stopLoss.TotalQuantity = (decimal)quantity;
                stopLoss.ParentId = parentOrderId;
                //In this case, the low side order will be the last child being sent. Therefore, it needs to set this attribute to true
                //to activate all its predecessors
                stopLoss.Transmit = true;

                List<Order> bracketOrder = new List<Order>();
                bracketOrder.Add(parent);
                bracketOrder.Add(takeProfit);
                bracketOrder.Add(stopLoss);
                return bracketOrder;
            }
            else
            {
                // Stop loss order
                Order stopLoss = new Order();
                stopLoss.OrderId = parent.OrderId + 2;
                stopLoss.Action = action.Equals("Buy") ? "Sell" : "Buy";
                stopLoss.OrderType = "STP";     // or "STP LMT", then use the field below...
                                                // Stop trigger price
                                                // add stopLoss.LmtPrice here if you are going to use a stop limit order
                stopLoss.AuxPrice = stopLossPrice;
                stopLoss.TotalQuantity = (decimal)quantity;
                stopLoss.ParentId = parentOrderId;
                //In this case, the low side order will be the last child being sent. Therefore, it needs to set this attribute to true
                //to activate all its predecessors
                stopLoss.Transmit = true;

                List<Order> bracketOrder = new List<Order>();
                bracketOrder.Add(parent);
                bracketOrder.Add(stopLoss);
                return bracketOrder;
            }

        }

        public void send_order(string side)
        {
            // Create a new contract to specify the security we are searching for
            IBApi.Contract contract = new IBApi.Contract();

            // Set the underlying stock symbol from the cbSymbol combobox
            contract.Symbol = cbSymbol.Text;
            // Set the Security type to STK for a Stock
            contract.SecType = "STK";
            // Use "SMART" as the general exchange
            contract.Exchange = cbMarket.Text;
            // Set the primary exchange (sometimes called Listing exchange)
            // Use either NYSE or ISLAND
            contract.PrimaryExch = "ISLAND";
            // Set the currency to USD
            contract.Currency = "USD";

            IBApi.Order order = new IBApi.Order();
            // gets the next order id from the text box
            order.OrderId = order_id;
            // gets the side of the order (BUY, or SELL)
            order.Action = side;
            // gets order type from combobox market or limit order(MKT, or LMT)
            order.OrderType = cbOrderType.Text;

            // number of shares from Quantity
            order.TotalQuantity = numQuantity.Value;

            // Value from limit price
            order.LmtPrice = Convert.ToDouble(numPrice.Value);

            // Sets AuxPrice to 0.00 for the SNAP type orders. 0.00 offset. if it's a STP order, use the price from price box.
            order.AuxPrice = 0.00;
            if (cbOrderType.Text == "STP")
                order.AuxPrice = Convert.ToDouble(numPrice.Value);

            // Visible shares to the market // ***** DISABLED ******
            // order.DisplaySize = Convert.ToInt32(tbVisible.Text);

            // checks if Outside RTH is checked, then apply outsideRTH to the order
            order.OutsideRth = chkOutside.Checked;

            // Place the order
            ibClient.ClientSocket.placeOrder(order_id, contract, order);

            string printBox = order.Action + " " + order.TotalQuantity + " " + contract.Symbol + " at " + order.OrderType + " " + order.LmtPrice;
            lbData.Items.Insert(0, printBox);

            // increase the order id value
            order_id++;

        }

        private void tbBid_Click(object sender, EventArgs e)
        {
            numPrice.Value = Convert.ToDecimal(tbBid.Text);
        }

        private void tbAsk_Click(object sender, EventArgs e)
        {
            numPrice.Value = Convert.ToDecimal(tbAsk.Text);
        }

        private void tbLast_Click(object sender, EventArgs e)
        {
            numPrice.Value = Convert.ToDecimal(tbLast.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1_counter == 0)
            {
                try
                {
                    timer1.Stop();  // stop the timer

                    // Add Last price to limit box
                    numPrice.Value = Convert.ToDecimal(tbLast.Text);

                    timer1_counter = 6; // reset time counter back to 5

                    // convert contract id from an int to a strong and add exchange
                    string strGroup = myContractId.ToString() + "@SMART";
                    // update the display group which will change the symbol
                    ibClient.ClientSocket.updateDisplayGroup(9002, strGroup);

                }
                catch (Exception) { }

            }
            timer1_counter--;   // subtract 1 every time there is a tick
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

        private void btnCancelLast_Click(object sender, EventArgs e)
        {
            if (BracketOrderExecuted && cbTakeProfit.Checked)
            {
                ibClient.ClientSocket.cancelOrder(order_id - 4, "");

                string printBox = "Cancelled full bracket order";
                lbData.Items.Insert(0, printBox);
            }
            else if (BracketOrderExecuted)
            {
                ibClient.ClientSocket.cancelOrder(order_id - 3, "");

                string printBox = "Cancelled parent and/or stop loss";
                lbData.Items.Insert(0, printBox);
            }
            else
            {
                ibClient.ClientSocket.cancelOrder(order_id - 1, "");
                string printBox = "Cancelled order";
                lbData.Items.Insert(0, printBox);
            }
            BracketOrderExecuted = false;
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            ibClient.ClientSocket.reqGlobalCancel();
            string printBox = "All open orders cancelled";
            lbData.Items.Insert(0, printBox);
        }

        // THE PURPOSE OF THIS IS TO KEEP THE RISK-CALCULATED QTY UPDATED WITH LIVE PRICE CHANGES, SO USER CAN SEE VARIABLE QTY //
        private void UpdateRiskQty(object sender, EventArgs e)
        {
            if (chkBracket.Checked)
            {
                if (cbOrderType.Text is "MKT" or "SNAP MKT" or "SNAP MID" or "SNAP PRIM")
                {
                    // numPrice.ReadOnly = true;
                    numQuantity.Value = Math.Abs(Math.Floor(numRisk.Value / (Convert.ToDecimal(tbLast.Text) - decimal.Parse(tbStopLoss.Text))));
                }

                else if (cbOrderType.Text is "LMT" or "STP")
                {
                    // numPrice.ReadOnly = false;
                    try
                    {
                        numQuantity.Value = Math.Abs(Math.Floor(numRisk.Value / (decimal.Parse(numPrice.Text) - decimal.Parse(tbStopLoss.Text))));
                    }
                    catch (Exception) { }
                }
            }
            else { }
        }

        private void chkBracket_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBracket.Checked)
            {
                tbStopLoss.ReadOnly = false;
                numQuantity.ReadOnly = true;
                numRisk.ReadOnly = false;
            }
            else
            {
                tbStopLoss.ReadOnly = true;
                numQuantity.ReadOnly = false;
                numRisk.ReadOnly = true;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome and thank you for trying IBKR Trader. This is intended for use with the TWS API and an IBKR Pro account with real-time data.\r\n\r\nPlease check Global Configuration > API > Settings. Enable ActiveX and Socket Clients, uncheck \"Read-Only\". Set the Socket Port #, then Apply and \"OK\".\r\nIn IBKR Trader app, set the Port # first to the same as you set in TWS.\r\nThen press Connect. Make sure the Bid/Ask/Last prices are being updated in real-time.\r\n\r\nQUICKLY SET LIMIT PRICE:\r\nYou may click the current Bid, Ask, or Last to set that price in the Price box.\r\n\r\nORDER TYPES:\r\nSNAP MKT will get you in automatically at the curret ASK for a Buy, and at the current BID for a Sell.\r\nSNAP MID will put you in the middle of the bid/ask.\r\nSNAP PRIM will put you at the current BID for a Buy, and at the current ASK for a Sell (for adding liquidity).\r\n\r\nROUTING:\r\nYou may leave the Route as SMART (default) or direct route to ISLAND (NSDQ) or EDGX.\r\n\r\nUSING $ RISK:\nIf you check the box \"$ Risk + SL,\" the Qty box will be disabled. Input the $ amount you wish to risk in the $ Risk box, and the Stop Loss price for the bracket order, after which the amount of shares will be automatically calculated once you click Buy or Sell, and will update in real-time with the approximate quantity.\r\nNOTE: The immediate calculation on clicking Buy or Sell is correct and accurate, but the Qty shown changing in the box in real-time is approximated.\r\nActivate Take Profit to also attach a take profit limit order.\r\n\r\nNEW STUFF:\r\nClose Pos with close your position at market. 50% will close half at market. 25% will close 25% of pos at market. Each will attempt to auto-adjust your stop loss quantity to your remaining qty. Stop to BE button will move your stop loss to your entry price.\r\n\r\nLINK/SYNC:\r\nThis tool is linked to TWS link Group 4, and will change the tickers within TWS windows on group 4.\r\n\r\nDISCLAIMER: I AM NOT RESPONSIBLE FOR FINANCIAL LOSS/GAIN YOU MAY INCUR DUE TO MISCLICK, MISUSE, OR MALFUNCTION OF THE TRADING APP. USE AT YOUR OWN RISK. PRACTICE IN A PAPER TRADING ACCOUNT TO VERIFY ALL FUNCTIONS BEFORE USING IN A LIVE ACCOUNT.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // If you click in column 10, the X
            if (e.ColumnIndex == 10)
            {
                if (e.RowIndex == -1) return;
                if (e.RowIndex >= 0)
                {
                    // represents the selected row
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    if (row.Cells[1].Value != null)
                    {
                        // gets order id from data grid in second column and conerts to an integer, cancels the order
                        ibClient.ClientSocket.cancelOrder(Convert.ToInt32(row.Cells["colid"].Value), "");

                        // removes the row from the grid after a second click on it.
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        // show a message box if no value is in the order id cell
                        MessageBox.Show("No ID Number in ID Cell ");
                    }
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                // adds an X to colume 10 which is colCancel column
                e.Value = "X";
            }
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[6].Value != null && !string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) && dataGridView1.Rows[e.RowIndex].Cells[9].Value != null)
                {
                    // creates a fill status variable and gets value of column 9
                    string fillstatus = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();


                    // checks if cell valye is SELL and changes color to Red and Bold
                    if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Trim() == "Sell")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        dataGridView1.Columns[6].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    // checks if cell value is BUY and changes color to green and bold
                    if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Trim() == "Buy")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[6].Style.BackColor = System.Drawing.Color.Green;
                        dataGridView1.Columns[6].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    // checks if value in cell column 8 if Filled and if it is changes fore color to Green
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Trim() == "Filled")
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Green };

                    //check if value in cell 8 is canceled and if so changes fore color text to green
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Trim() == "Canceled")
                    {
                        if (fillstatus == "0.00")
                        {
                            dataGridView1.Rows[e.RowIndex].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Green };
                        }
                    }
                    // checks for partial fill in column 8 status column and changes text to yellow
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Trim() == "Submitted")
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString().Trim() != "0.00")
                        {
                            dataGridView1.Rows[e.RowIndex].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Yellow };
                        }
                    }

                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[6].Style = dataGridView1.DefaultCellStyle;
                }
            }
            catch (Exception) { }
        }

        delegate void SetTextCallbackOrderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice);

        public void AddDataGridViewItemOrderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
        {
            // see if a new invocation is required from a diff thread
            if (dataGridView1.InvokeRequired)
            {
                SetTextCallbackOrderStatus d = new SetTextCallbackOrderStatus(AddDataGridViewItemOrderStatus);
                this.Invoke(d, new object[] { orderId, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld, mktCapPrice });
            }
            else
            {
                string myStatus = status;
                if (myStatus == "Cancelled")
                {
                    myStatus = "Canceled";
                }

                // order status
                // 0 orderid + 1 status + 2 filled 3 reamining: 4 remaining 5 avgfillprice 6 permid 7 parentid 8 lastfillprice 9 clientid 10 whyheld 11 mktcapprice

                string searchValue = Convert.ToString(orderId);     // 0 represents order id
                int countRow = 0;
                Boolean wasFound = false;

                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value != null && !string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString()))
                        {
                            if (row.Cells[1].Value.ToString().Equals(searchValue))      // order id searchvalue
                            {
                                // modify value in the first cell of second row
                                dataGridView1.Rows[countRow].Cells[1].Value = orderId;          // order number
                                dataGridView1.Rows[countRow].Cells[5].Value = filled + "/" + remaining;     // filled vs remaining
                                dataGridView1.Rows[countRow].Cells[8].Value = myStatus;         // Status of order
                                dataGridView1.Rows[countRow].Cells[9].Value = lastFillPrice.ToString("N2");     // e.lastFillPrice
                                dataGridView1.Rows[countRow].Cells[10].Value = "X";     // cancel

                                wasFound = true;
                                break;
                            }
                            countRow++;
                        }

                    }
                }
                catch (Exception) { }

                if (wasFound)
                {
                    int rowCount = 0;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(searchValue))
                        {
                            dataGridView1.Rows[countRow].Cells[5].Value = filled + "/" + remaining;
                            dataGridView1.Rows[countRow].Cells[8].Value = myStatus;
                            dataGridView1.Rows[countRow].Cells[9].Value = lastFillPrice.ToString("N2");
                            dataGridView1.Rows[countRow].Cells[10].Value = "X";

                            break;
                        }
                        rowCount++;
                    }
                }
                else if (!wasFound)     // was not found in data grid view
                {
                    int n = dataGridView1.Rows.Add();
                    {
                        dataGridView1.Rows[n].Cells[1].Value = orderId;
                        dataGridView1.Rows[n].Cells[2].Value = cbSymbol.Text;
                        dataGridView1.Rows[n].Cells[3].Value = numPrice.Text;
                        dataGridView1.Rows[n].Cells[5].Value = filled + "/" + remaining;
                        dataGridView1.Rows[n].Cells[8].Value = myStatus;
                        dataGridView1.Rows[n].Cells[9].Value = lastFillPrice.ToString("N2");
                        dataGridView1.Rows[n].Cells[10].Value = "X";
                    }
                }

                else
                {
                    int n = dataGridView1.Rows.Add();       // not yet added in the data grid view
                    {
                        dataGridView1.Rows[n].Cells[1].Value = orderId;
                        dataGridView1.Rows[n].Cells[5].Value = filled + "/" + remaining;
                        dataGridView1.Rows[n].Cells[8].Value = myStatus;
                        dataGridView1.Rows[n].Cells[9].Value = lastFillPrice.ToString("N2");
                        dataGridView1.Rows[n].Cells[10].Value = "X";

                    }
                }
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }
        }

        delegate void SetTextCallbackOpenOrder(string open_order);

        public void AddListBoxItemOpenOrder(string open_order)
        {
            if (lbData.InvokeRequired)
            {
                SetTextCallbackOpenOrder d = new SetTextCallbackOpenOrder(AddListBoxItemOpenOrder);
                this.Invoke(d, new object[] { open_order });
            }
            else
            {
                lbData.Items.Add(open_order);

                string[] myOpenOrder = new string[] { open_order };
                myOpenOrder = open_order.Split(',');

                string searchValue = Convert.ToString(myOpenOrder[2]);      // 2 = order id
                int countRow = 0;
                Boolean wasFound = false;

                double myLimitPrice = Convert.ToDouble(myOpenOrder[11]);    // 11 = lmtPrice
                double myAuxPrice = Convert.ToDouble(myOpenOrder[12]);      // 12 = auxPrice

                if (dataGridView1.Rows.Count < 0)
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[0].Value?.ToString().Equals(searchValue))
                        {
                            dataGridView1.Rows[countRow].Cells[1].Value = myOpenOrder[2];   // order number
                            dataGridView1.Rows[countRow].Cells[2].Value = myOpenOrder[4];   // stock symbol
                            dataGridView1.Rows[countRow].Cells[3].Value = myLimitPrice.ToString("N2");  // limit price to 2 decimals
                            dataGridView1.Rows[countRow].Cells[4].Value = myAuxPrice.ToString("N2");  // Aux price to 2 decimals
                            dataGridView1.Rows[countRow].Cells[6].Value = myOpenOrder[7];   // Action (buy or sell)
                            dataGridView1.Rows[countRow].Cells[7].Value = myOpenOrder[8];   // order type LMT, MKT....
                            dataGridView1.Rows[countRow].Cells[10].Value = "X";   // cancel button

                            wasFound = true;
                            break;
                        }
                        countRow++;
                    }
                }
                catch
                {
                    if (wasFound)
                    {
                        int rowCount = 0;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value.ToString().Equals(searchValue))
                            {
                                // Check video on lots of commented out values for Cells[1,2,3,4,5,6,7]
                                dataGridView1.Rows[countRow].Cells[10].Value = "X";
                                break;
                            }
                            rowCount++;
                        }
                    }
                    else if (!wasFound)
                    {
                        int n = dataGridView1.Rows.Add();

                        dataGridView1.Rows[n].Cells[0].Value = "Time";          // time submitted
                        dataGridView1.Rows[n].Cells[1].Value = myOpenOrder[2];  // order id number
                        dataGridView1.Rows[n].Cells[2].Value = myOpenOrder[4];  // stock symbol
                        dataGridView1.Rows[countRow].Cells[3].Value = myLimitPrice.ToString("N2");  // limit price to 2 decimals
                        dataGridView1.Rows[countRow].Cells[4].Value = myAuxPrice.ToString("N2");    // aux price to 2 decimals

                        dataGridView1.Rows[n].Cells[6].Value = myOpenOrder[7];      // shares remaining
                        dataGridView1.Rows[n].Cells[7].Value = myOpenOrder[8];      // order type
                        dataGridView1.Rows[n].Cells[10].Value = "X";                // cancel order
                    }
                    else
                    {
                        int n = dataGridView1.Rows.Add();           // not yet added in the data grid view

                        dataGridView1.Rows[n].Cells[1].Value = myOpenOrder[2];      // order id
                        dataGridView1.Rows[n].Cells[2].Value = myOpenOrder[4];      // stock symbol
                        dataGridView1.Rows[n].Cells[6].Value = myOpenOrder[7];      // shares remaining
                        dataGridView1.Rows[n].Cells[7].Value = myOpenOrder[8];      // status of order

                    }
                }
            }
        }

        // USED TO SET WINDOW AS "ALWAYS ON TOP" OF OTHER WINDOWS
        private void cbAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAlwaysOnTop.Checked == true)
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            else
                SetWindowPos(this.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        delegate void SetTextCallbackShortable(string shortable);
        public void BtnShortable(string shortable)
        {
            // See if a new invocation is required from a different thread            
            if (tbShortable.InvokeRequired)
            {
                SetTextCallbackShortable d = new SetTextCallbackShortable(BtnShortable);
                Invoke(d, new object[] { shortable });
            }
            else
            {
                // change the text box to light green if over 1000 shares shortable. green if locate needed
                // and red if security is not shortable. Similar to IB TWS.
                double shortableshares = Convert.ToDouble(shortable);
                if (shortableshares > 2.5)
                {
                    tbShortable.BackColor = Color.LightGreen;
                    tbShortable.Text = "Yes";
                }
                else if (shortableshares > 1.5 && shortableshares < 2.5)
                {
                    tbShortable.BackColor = Color.Green;
                    tbShortable.Text = "Locate";
                }
                else
                {
                    tbShortable.BackColor = Color.Red;
                    tbShortable.Text = "No";
                }
            }
        }

        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView4.Rows[e.RowIndex].Cells[5].Value != null && !string.IsNullOrWhiteSpace(dataGridView4.Rows[e.RowIndex].Cells[5].Value.ToString()))
            {
                double markedField = Convert.ToDouble(dataGridView4.Rows[e.RowIndex].Cells[5].Value.ToString().Trim());
                double closedField = Convert.ToDouble(dataGridView4.Rows[e.RowIndex].Cells[4].Value.ToString().Trim());
                double openField = Convert.ToDouble(dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
                int positionField = int.Parse(dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString().Trim());

                if (positionField > 0)
                    dataGridView4.Rows[e.RowIndex].Cells[1].Style.ForeColor = Color.Green;

                if (positionField < 0)
                    dataGridView4.Rows[e.RowIndex].Cells[1].Style.ForeColor = Color.Red;

                if (markedField > 0)
                    dataGridView4.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.Lime;

                if (markedField < 0)
                    dataGridView4.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.Red;

                if (closedField > 0)
                    dataGridView4.Rows[e.RowIndex].Cells[4].Style.ForeColor = Color.Lime;

                if (closedField < 0)
                    dataGridView4.Rows[e.RowIndex].Cells[4].Style.ForeColor = Color.Red;

                if (openField > 0)
                    dataGridView4.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Lime;

                if (openField < 0)
                    dataGridView4.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Red;

                dataGridView4.Columns[3].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }
            else
            {
                dataGridView4.Rows[e.RowIndex].Cells[5].Style = dataGridView1.DefaultCellStyle;
            }
        }

        delegate void SetTextCallbackUpdatePortfolio(string symbol, decimal position, double marketPrice, double averageCost, double unrealizedPNL, double realizedPNL);

        public void AddTextBoxItemUpdatePortfolio(string symbol, decimal position, double marketPrice, double averageCost, double unrealizedPNL, double realizedPNL)
        {
            // See if a new invocation is required from a different thread
            if (this.dataGridView4.InvokeRequired)
            {
                SetTextCallbackUpdatePortfolio d = new SetTextCallbackUpdatePortfolio(AddTextBoxItemUpdatePortfolio);
                this.Invoke(d, new object[] { symbol, position, marketPrice, averageCost, unrealizedPNL, realizedPNL });
            }
            else
            {

                string searchValue = symbol;
                int countRow2 = 0;
                Boolean wasFound2 = false;
                double myMarkedPNL = unrealizedPNL + realizedPNL;
                string iMarkedPNL = Convert.ToString(myMarkedPNL);


                if (dataGridView4.Rows.Count < 0)
                    // sets the selection mode
                    dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                try
                {
                    // searches for the symbol and counts the rows and set the wasFound2 to true if found
                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {

                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            wasFound2 = true;
                            break;
                        }
                        countRow2++;
                    }
                }
                catch (Exception)
                {
                }

                // if found and there is not position
                if (wasFound2 && position == 0)
                {
                    dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        //seaches for the security symbol

                        if (row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            // Modify the values in the row based on the current stock symbols values.
                            dataGridView4.Rows[countRow2].Cells[1].Value = position;  // Postion
                            dataGridView4.Rows[countRow2].Cells[2].Value = averageCost;    // average cost Price
                            dataGridView4.Rows[countRow2].Cells[3].Value = unrealizedPNL;    // unrealized
                            dataGridView4.Rows[countRow2].Cells[4].Value = realizedPNL;   // realized
                            dataGridView4.Rows[countRow2].Cells[5].Value = iMarkedPNL;   // total pnl
                            break;
                        }
                    }
                    // Cancel the streaming data here for the found symbol 
                    ibClient.ClientSocket.cancelMktData(countRow2 + 20);
                }
                // if symbol was found
                else if (wasFound2)
                {

                    dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                        {
                            // Modify the values in the row based on the current stock symbol.
                            dataGridView4.Rows[countRow2].Cells[1].Value = position;  // Postion
                            dataGridView4.Rows[countRow2].Cells[2].Value = averageCost;    // average cost Price
                            dataGridView4.Rows[countRow2].Cells[3].Value = unrealizedPNL;    // unrealized
                            dataGridView4.Rows[countRow2].Cells[4].Value = realizedPNL;   // realized
                            dataGridView4.Rows[countRow2].Cells[5].Value = iMarkedPNL;    // total pnl

                            // if position is found call method for live streaming data
                            int unique_id = Convert.ToInt32(countRow2 + 20);
                            getPositionData(symbol, unique_id); // calls method symbol and position id

                            break;
                        }

                    }
                }
                // symbol was not found in Data Grid View and position is not equal to zero
                else if (!wasFound2 && position != 0)
                {
                    int n = dataGridView4.Rows.Add();
                    {
                        dataGridView4.Rows[n].Cells[0].Value = symbol;
                        dataGridView4.Rows[n].Cells[1].Value = position;
                        dataGridView4.Rows[n].Cells[2].Value = averageCost;
                        dataGridView4.Rows[n].Cells[3].Value = unrealizedPNL;
                        dataGridView4.Rows[n].Cells[4].Value = realizedPNL;
                        dataGridView4.Rows[n].Cells[5].Value = iMarkedPNL;
                    }
                    // this is where you start the streaming data ***********
                    int unique_id = Convert.ToInt32(n + 20);
                    getPositionData(symbol, unique_id); // requests the live streaming data
                }
                // of all else fails 
                else if (myMarkedPNL != 0.00)
                {
                    int n = dataGridView4.Rows.Add();  // Not added yet in the Data Grid view
                    {
                        dataGridView4.Rows[n].Cells[0].Value = symbol;
                        dataGridView4.Rows[n].Cells[1].Value = position;
                        dataGridView4.Rows[n].Cells[2].Value = averageCost;
                        dataGridView4.Rows[n].Cells[3].Value = unrealizedPNL;
                        dataGridView4.Rows[n].Cells[4].Value = realizedPNL;
                        dataGridView4.Rows[n].Cells[5].Value = iMarkedPNL;
                    }
                }


            }

            positionTotal(); // calls the method to calculate the position total
        }

        public void getPositionData(string symbol, int unique_id)
        {
            // Create a new contract to specify the security we are searching for
            IBApi.Contract contract = new IBApi.Contract();
            // Create a new TagValueList object (for API version 9.71 and later) 
            List<IBApi.TagValue> mktDataOptions = new List<IBApi.TagValue>();

            // Set the underlying stock symbol from the tbSymbol text box
            contract.Symbol = symbol;
            // Set the Security type to STK for a Stock FUT = Futures, 
            contract.SecType = "STK";  // "FUT"
            // Use "SMART" as the general exchange
            contract.Exchange = "SMART"; // "GLOBEX"
            // Set the primary exchange (sometimes called Listing exchange)
            // Use either NYSE or ISLAND for Futures use GLOBEX
            contract.PrimaryExch = "ISLAND";
            // Set the currency to USD
            contract.Currency = "USD";


            ibClient.ClientSocket.reqMarketDataType(1);  // delayed data = 3, live data = 1

            ibClient.ClientSocket.reqMktData(unique_id, contract, "", false, false, mktDataOptions);

        }

        public void positionTotal()
        {
            double sumTotal = 0.00;

            for (int i = 0; i < dataGridView4.Rows.Count; ++i)
            {
                sumTotal += Convert.ToDouble(dataGridView4.Rows[i].Cells[5].Value);
            }

            tbTotalPnl.Text = Convert.ToString(sumTotal);
            if (sumTotal > 0)
            {
                tbTotalPnl.ForeColor = Color.LimeGreen;
            }
            else if (sumTotal < 0)
            {
                tbTotalPnl.ForeColor = Color.Red;
            }
        }

        private void cbTakeProfit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTakeProfit.Checked)
            {
                takeProfitEnabled = true;
                tbTakeProfit.ReadOnly = false;
            }
            else
            {
                takeProfitEnabled = false;
                tbTakeProfit.ReadOnly = true;
            }
        }

        private void ClosePosition(object sender, EventArgs e)
        {
            int countRow2 = 0;
            decimal pos = 0;
            bool wasFound2 = false;
            string searchValue = cbSymbol.Text;
            int orderIds = 0;
            int countRow3 = 0;

            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        pos = Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value); // Position
                        break;
                    }
                }
                IBApi.Contract contract = new Contract();
                contract.Symbol = searchValue;
                contract.SecType = "STK";
                contract.Exchange = "SMART";
                contract.PrimaryExch = "ISLAND";
                contract.Currency = "USD";

                IBApi.Order order = new IBApi.Order();
                order.OrderId = order_id;

                if (pos > 0)
                    order.Action = "Sell";

                else if (pos < 0)
                    order.Action = "Buy";

                order.OrderType = "MKT";
                order.TotalQuantity = Math.Abs(pos);
                order.LmtPrice = 0.00;
                order.AuxPrice = 0.00;
                // checks if Outside RTH is checked, then apply outsideRTH to the order
                order.OutsideRth = chkOutside.Checked;

                // Place the order
                ibClient.ClientSocket.placeOrder(order_id, contract, order);

                string printBox = order.Action + " " + order.TotalQuantity + " " + contract.Symbol + " at " + order.OrderType + " to close.";
                lbData.Items.Insert(0, printBox);

                // increase the order id value
                order_id++;
            }
            bool wasFound3 = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[2].Value.ToString().Equals(searchValue)) // was found in data grid
                {
                    orderIds = Convert.ToInt32(dataGridView1.Rows[countRow3].Cells[1].Value);
                    ibClient.ClientSocket.cancelOrder(orderIds, "");
                    wasFound3 = true;
                }
                countRow3++;
            }
            if (wasFound3)
                lbData.Items.Insert(0, "Canceled all open orders for " + searchValue);

        }
        private void CloseHalfPos(object sender, EventArgs e)
        {
            int countRow2 = 0;
            decimal pos = 0;
            bool wasFound2 = false;
            string searchValue = cbSymbol.Text;
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        pos = Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value); // Position
                        break;
                    }

                }
                IBApi.Contract contract = new Contract();
                contract.Symbol = searchValue;
                contract.SecType = "STK";
                contract.Exchange = "SMART";
                contract.PrimaryExch = "ISLAND";
                contract.Currency = "USD";

                IBApi.Order order = new IBApi.Order();
                order.OrderId = order_id;

                if (pos > 0)
                    order.Action = "Sell";

                else if (pos < 0)
                    order.Action = "Buy";

                order.OrderType = "MKT";
                order.TotalQuantity = Math.Abs(Math.Floor(pos / 2));
                order.LmtPrice = Convert.ToDouble(numPrice.Value);
                order.AuxPrice = 0.00;
                // checks if Outside RTH is checked, then apply outsideRTH to the order
                order.OutsideRth = chkOutside.Checked;

                // Place the order
                ibClient.ClientSocket.placeOrder(order_id, contract, order);

                string printBox = order.Action + " " + order.TotalQuantity + " " + contract.Symbol + " at " + order.OrderType + " to close half.";
                lbData.Items.Insert(0, printBox);

                // increase the order id value
                order_id++;
                UpdateStop(order.TotalQuantity);
            }
        }
        private void btnCloseQtr_Click(object sender, EventArgs e)
        {
            int countRow2 = 0;
            decimal pos = 0;
            bool wasFound2 = false;
            string searchValue = cbSymbol.Text;
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        pos = Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value); // Position
                        break;
                    }

                }
                IBApi.Contract contract = new Contract();
                contract.Symbol = searchValue;
                contract.SecType = "STK";
                contract.Exchange = "SMART";
                contract.PrimaryExch = "ISLAND";
                contract.Currency = "USD";

                IBApi.Order order = new IBApi.Order();
                order.OrderId = order_id;

                if (pos > 0)
                    order.Action = "Sell";

                else if (pos < 0)
                    order.Action = "Buy";

                order.OrderType = "MKT";

                order.TotalQuantity = Math.Abs(Math.Floor(pos / 4));

                order.LmtPrice = Convert.ToDouble(numPrice.Value);
                order.AuxPrice = 0.00;

                // checks if Outside RTH is checked, then apply outsideRTH to the order
                order.OutsideRth = chkOutside.Checked;

                // Place the order
                ibClient.ClientSocket.placeOrder(order_id, contract, order);

                string printBox = order.Action + " " + order.TotalQuantity + " " + contract.Symbol + " at " + order.OrderType + " to close quarter pos.";
                lbData.Items.Insert(0, printBox);

                // increase the order id value
                order_id++;
                UpdateStop(order.TotalQuantity);
            }
        }

        private void UpdateStop(decimal orderQty)
        {
            int countRow2 = 0;
            decimal pos = 0;
            bool wasFound2 = false;
            string searchValue = cbSymbol.Text;
            int stopOrderId = 0;
            double stopPrice = 0;
            string side = "";
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        pos = Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value); // Position
                        break;
                    }

                }
            }
            int countRow3 = 0;
            bool wasFound3 = false;
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Equals(searchValue))
                    {
                        wasFound3 = true;
                        break;
                    }
                    countRow3++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound3)
            {
                try
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[7].Value.ToString().Equals("STP") && row.Cells[2].Value.ToString().Equals(searchValue)) // was found in data grid
                        {
                            // Modify the values in the row based on the current stock symbol.
                            stopOrderId = Convert.ToInt32(dataGridView1.Rows[countRow3].Cells[1].Value);
                            stopPrice = Convert.ToDouble(dataGridView1.Rows[countRow3].Cells[4].Value);
                            side = (string)(dataGridView1.Rows[countRow3].Cells[6].Value);
                            break;
                        }
                    }

                    IBApi.Contract contract = new Contract();
                    contract.Symbol = searchValue;
                    contract.SecType = "STK";
                    contract.Exchange = "SMART";
                    contract.PrimaryExch = "ISLAND";
                    contract.Currency = "USD";

                    IBApi.Order stopLoss = new Order();
                    stopLoss.OrderId = stopOrderId;
                    stopLoss.Action = side;
                    stopLoss.OrderType = "STP";
                    stopLoss.TotalQuantity = Math.Abs(pos) - orderQty;
                    stopLoss.AuxPrice = stopPrice;

                    // Place the order
                    ibClient.ClientSocket.placeOrder(stopOrderId, contract, stopLoss);
                }
                catch (Exception s)
                {
                    lbData.Items.Insert(0, "UpdateStop error: " + s);
                }
            }
        }

        delegate void SetTextCallbackGetFullName(string fullName);
        public void GetFullName(string fullName)
        {
            if (labelName.InvokeRequired)
            {
                try
                {
                    SetTextCallbackGetFullName d = new SetTextCallbackGetFullName(GetFullName);
                    Invoke(d, new object[] { fullName });
                }
                catch (Exception f)
                {
                    lbData.Items.Insert(0, "GetFullName Invoke error: " + f);
                }
            }
            else
            {
                try
                {
                    labelName.Text = fullName;
                }
                catch (Exception) { }
            }
        }

        delegate void SetTextCallbackTickSize(decimal avgvol);
        public void AverageVolume(decimal avgvol)
        {
            if (labelAvgVol.InvokeRequired)
            {
                try
                {
                    SetTextCallbackTickSize d = new SetTextCallbackTickSize(AverageVolume);
                    Invoke(d, new object[] { avgvol });
                }
                catch (Exception f)
                {
                    lbData.Items.Insert(0, "AvgVol Invoke error: " + f);
                }
            }
            else
            {
                decimal AV = avgvol * 100;
                labelAvgVol.Text = "AvgVol: " + AV.ToString("#,##0");
            }
        }

        public double closePrice;
        public double openPrice;

        private void PercentChange(object sender, EventArgs e)
        {
            double percentchange = ((Convert.ToDouble(tbLast.Text) - closePrice) / closePrice) * 100;
            labelChange.Text = percentchange.ToString("0.##") + "%";

            if (percentchange > 0)
                labelChange.ForeColor = Color.Blue;

            else if (percentchange < 0)
                labelChange.ForeColor = Color.DarkRed;

            else
            { labelChange.ForeColor = Color.Black; }

            double changesinceopen = ((Convert.ToDouble(tbLast.Text) - openPrice) / openPrice) * 100;
            labelSinceOpen.Text = "SncOpn: " + changesinceopen.ToString("0.##") + "%";

            if (changesinceopen > 0)
                labelSinceOpen.ForeColor = Color.Blue;

            else if (changesinceopen < 0)
                labelSinceOpen.ForeColor = Color.DarkRed;

            else
            { labelSinceOpen.ForeColor = Color.Black; }
        }

        private void btnS2BE_Click(object sender, EventArgs e)
        {
            int countRow2 = 0;
            double entryPrice = 0;
            bool wasFound2 = false;
            string searchValue = cbSymbol.Text;
            int stopOrderId = 0;
            decimal pos = 0;
            string side = "";
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        entryPrice = Math.Round(Convert.ToDouble(dataGridView4.Rows[countRow2].Cells[2].Value), 2); // Position
                        pos = Math.Abs(Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value));

                        break;
                    }

                }
            }
            int countRow3 = 0;
            bool wasFound3 = false;
            bool wasFound4 = false;
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Equals(searchValue))
                    {
                        wasFound3 = true;
                        break;
                    }
                    countRow3++;
                }
            }
            catch (Exception)
            {
                lbData.Items.Insert(0, "Error in counting Rows");
            }
            if (wasFound3)
            {
                try
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[7].Value.ToString().Equals("STP") && row.Cells[2].Value.ToString().Equals(searchValue)) // was found in data grid
                        {
                            // Modify the values in the row based on the current stock symbol.
                            stopOrderId = Convert.ToInt32(dataGridView1.Rows[countRow3].Cells[1].Value); // Position
                            side = (string)dataGridView1.Rows[countRow3].Cells[6].Value;
                            wasFound4 = true;
                            break;
                        }
                    }
                    IBApi.Contract contract = new Contract();
                    contract.Symbol = searchValue;
                    contract.SecType = "STK";
                    contract.Exchange = "SMART";
                    contract.PrimaryExch = "ISLAND";
                    contract.Currency = "USD";

                    IBApi.Order stopLoss = new Order();
                    stopLoss.OrderId = stopOrderId;
                    stopLoss.Action = side;
                    stopLoss.OrderType = "STP";
                    stopLoss.TotalQuantity = pos;
                    if (side == "Buy")
                        stopLoss.AuxPrice = entryPrice + 0.01;
                    else
                        stopLoss.AuxPrice = entryPrice - 0.01;

                    // Place the order
                    ibClient.ClientSocket.placeOrder(stopOrderId, contract, stopLoss);
                    if (wasFound4)
                    {
                        lbData.Items.Insert(0, "Stop Loss modified to avg price " + Math.Round(stopLoss.AuxPrice, 2));

                    }
                }
                catch (Exception s)
                {
                    lbData.Items.Insert(0, "Stop to BE Error: " + s);
                }
            }
        }
        private void btnPosition_Click(object sender, EventArgs e)
        {
            bool wasFound2 = false;
            int countRow2 = 0;
            decimal pos = 0;
            string searchValue = cbSymbol.Text;

            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception h)
            {
                lbData.Items.Insert(0, "Position click: " + h);
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        pos = Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value); // Position
                        break;
                    }
                }
                numQuantity.Value = Math.Abs(pos);
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

        private void btnTenPercent_Click(object sender, EventArgs e)
        {
            int countRow2 = 0;
            decimal pos = 0;
            bool wasFound2 = false;
            string searchValue = cbSymbol.Text;
            try
            {
                // searches for the symbol and counts the rows and set the wasFound2 to true if found
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        wasFound2 = true;
                        break;
                    }
                    countRow2++;
                }
            }
            catch (Exception)
            {
            }
            if (wasFound2)
            {
                dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue)) // was found in data grid
                    {
                        // Modify the values in the row based on the current stock symbol.
                        pos = Convert.ToDecimal(dataGridView4.Rows[countRow2].Cells[1].Value); // Position
                        break;
                    }

                }
                IBApi.Contract contract = new Contract();
                contract.Symbol = searchValue;
                contract.SecType = "STK";
                contract.Exchange = "SMART";
                contract.PrimaryExch = "ISLAND";
                contract.Currency = "USD";

                IBApi.Order order = new IBApi.Order();
                order.OrderId = order_id;

                if (pos > 0)
                    order.Action = "Sell";

                else if (pos < 0)
                    order.Action = "Buy";

                order.OrderType = "MKT";

                order.TotalQuantity = Math.Abs(Math.Floor(pos / 10));

                order.LmtPrice = Convert.ToDouble(numPrice.Value);
                order.AuxPrice = 0.00;

                // checks if Outside RTH is checked, then apply outsideRTH to the order
                order.OutsideRth = chkOutside.Checked;

                // Place the order
                ibClient.ClientSocket.placeOrder(order_id, contract, order);

                string printBox = order.Action + " " + order.TotalQuantity + " " + contract.Symbol + " at " + order.OrderType + " to close quarter pos.";
                lbData.Items.Insert(0, printBox);

                // increase the order id value
                order_id++;
                UpdateStop(order.TotalQuantity);
            }
        }
    }
}