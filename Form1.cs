using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.ComponentModel;
using IBApi;
using System.Drawing.Text;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;


/* PROPOSED ADDITIONS, REVISIONS, AND FIXES
 * ADD - CLOSE POSITION BUTTON - will close the position (and cancel pending orders) for selected ticker
 * ADD - TRIM BUTTONS - closes 50% of position. maybe a 25% of position also.
 * */

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


        // Delegate enables asynchronous calls for settings text property on ListBox
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
        IBKR_Trader.EWrapperImpl ibClient;

        public Form1()
        {
            InitializeComponent();

            // Instantiate the ibClient
            ibClient = new IBKR_Trader.EWrapperImpl();

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
                btnConnect.Text = "Connected!";
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
                    MessageBox.Show("Failure to connect.\r\nPlease make sure API is active in TWS, read-only is disabled, and the Port number is correct.");
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
                    if (Convert.ToInt32(tickerPrice[1]) == 68)// Delayed Last quote 68, if you want realtime use tickerPrice == 4
                    {
                        // Add the text string to the list box

                        this.tbLast.Text = tickerPrice[2];

                    }
                    else if (Convert.ToInt32(tickerPrice[1]) == 67)  // Delayed Ask quote 67, if you want realtime use tickerPrice == 2
                    {
                        // Add the text string to the list box

                        this.tbAsk.Text = tickerPrice[2];

                    }
                    else if (Convert.ToInt32(tickerPrice[1]) == 66)  // Delayed Bid quote 66, if you want realtime use tickerPrice == 1
                    {
                        // Add the text string to the list box

                        this.tbBid.Text = tickerPrice[2];

                    }
                    double spread = Math.Round(Convert.ToDouble(tbAsk.Text) - Convert.ToDouble(tbBid.Text), 2);
                    labelSpread.Text = spread.ToString();
                }

            }
        }
        private void getData()
        {
            if (ibClient.ClientSocket.IsConnected() == true)
            {
                btnConnect.Text = "Connected!";
                btnConnect.BackColor = Color.LightGreen;
            }
            else if (ibClient.ClientSocket.IsConnected() == false)
            {
                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.Gainsboro;
            }

            // clears contents of TnS when changing
            listViewTns.Items.Clear();

            ibClient.ClientSocket.cancelMktData(1); // cancel market data
            ibClient.ClientSocket.cancelRealTimeBars(0);  // not needed yet.

            // Create a new contract to specify the security we are searching for
            IBApi.Contract contract = new IBApi.Contract();
            // Create a new TagValueList object (for API version 9.71 and later) 
            List<IBApi.TagValue> mktDataOptions = new List<IBApi.TagValue>();

            // Set the underlying stock symbol fromthe cbSymbol combobox            
            contract.Symbol = cbSymbol.Text;
            // Set the Security type to STK for a Stock
            contract.SecType = "STK";
            // Use "SMART" as the general exchange
            contract.Exchange = "SMART";
            // Set the primary exchange (sometimes called Listing exchange)
            // Use either NYSE or ISLAND
            contract.PrimaryExch = "ISLAND";
            // Set the currency to USD
            contract.Currency = "USD";

            // If using delayed market data subscription un-comment 
            // the line below to request delayed data
            ibClient.ClientSocket.reqMarketDataType(3);  // delayed data = 3 live = 1

            // Kick off the subscription for real-time data (add the mktDataOptions list for API v9.71)

            // For API v9.72 and higher, add one more parameter for regulatory snapshot
            ibClient.ClientSocket.reqMktData(1, contract, "233", false, false, mktDataOptions);

            // request contract details based on contract that was created above
            ibClient.ClientSocket.reqContractDetails(88, contract);

            timer1.Start();
        }

        delegate void SetTextCallbackTickString(string _tickString);

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

                    // Console.WriteLine(listTimeSales);

                    // get the first value form the list convert it to a double this value is the last price
                    double last_price = Convert.ToDouble(listTimeSales[0]);

                    // REPLACED LINE: int trade_size = Convert.ToInt32(listTimeSales[1]);
                    int trade_size = Convert.ToInt32(listTimeSales[1]);

                    double trade_time = Convert.ToDouble(listTimeSales[2]);

                    // adds 2 zeros to the trade size  ??  delete the " * 100 " to leave the data unmodified.
                    int share_size = trade_size * 100;

                    // formats a string to commas
                    string strShareSize = share_size.ToString("###,####,##0");

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    epoch = epoch.AddMilliseconds(trade_time);
                    // *************************************************
                    epoch = epoch.AddHours(-4);   //Daylight saving time use -4 Summer otherwise use -5 Winter

                    string strSaleTime = epoch.ToString("h:mm:ss:ff");

                    double myMeanPrice = ((theAsk - theBid) / 2);
                    double myMean = (theBid + myMeanPrice);

                    ListViewItem lx = new ListViewItem();

                    // string dt = String.Format("{0:hh:mm:ss}", dnt);

                    // if the last price is the same as the ask change the color to green
                    if (last_price == theAsk)
                    {
                        lx.ForeColor = Color.Green; // listview foreground color
                        lx.Text = listTimeSales[0]; // last price
                        lx.SubItems.Add(strShareSize); // share size
                        lx.SubItems.Add(strSaleTime); // time
                        listViewTns.Items.Insert(0, lx); // use Insert instead of Add listView.Items.Add(li); 
                    }
                    // if the last price is the same as the bid change the color to red
                    else if (last_price == theBid)
                    {
                        lx.ForeColor = Color.Red;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        lbData.Items.Insert(0, strSaleTime);
                    }
                    // if the last price in greater than the mean price and
                    // less than the ask price change the color to lime green
                    else if (last_price > myMean && last_price < theAsk)
                    {
                        lx.ForeColor = Color.Lime;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);

                        lbData.Items.Add(epoch);
                    }
                    else
                    {
                        lx.ForeColor = Color.DarkRed;
                        lx.Text = listTimeSales[0];
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);
                    }
                }
                catch
                {

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
            List<Order> bracket = BracketOrder(order_id++, action, quantity, lmtPrice, takeProfit, stopLoss, order_type);
            foreach (Order o in bracket)    // loops through each order in the list
                ibClient.ClientSocket.placeOrder(o.OrderId, contract, o);

            string printBox = action + " " + quantity + " " + contract.Symbol + " at " + order_type + " " + lmtPrice + " and Stop " + stopLoss;
            lbData.Items.Insert(0, printBox);

            // increase order id by 2, for parent and stop loss, as to not use same order id twice and get an error
            order_id += 2;

        }


        public static List<Order> BracketOrder(int parentOrderId, string action, double quantity, double limitPrice, double takeProfitLimitPrice, double stopLossPrice, string order_type)
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

            /** TAKE PROFIT IS DISABLED FOR NOW
             * 
            // Profit Target order
            Order takeProfit = new Order();
            takeProfit.OrderId = parent.OrderId + 1;
            takeProfit.Action = action.Equals("Buy") ? "Sell" : "Buy"; // if statement
            takeProfit.OrderType = "LMT";
            takeProfit.TotalQuantity = (decimal)quantity;
            takeProfit.LmtPrice = takeProfitLimitPrice;
            takeProfit.ParentId = parentOrderId;
            takeProfit.Transmit = false;
            **/

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
            // bracketOrder.Add(takeProfit);
            bracketOrder.Add(stopLoss);
            return bracketOrder;
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
            ibClient.ClientSocket.cancelOrder(order_id - 1, "");
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            ibClient.ClientSocket.reqGlobalCancel();
        }

        // THE PURPOSE OF THIS IS TO KEEP THE RISK-CALCULATED QTY UPDATED WITH LIVE PRICE CHANGES, SO USER CAN SEE VARIABLE QTY //
        private void UpdateRiskQty(object sender, EventArgs e)
        {
            if (cbOrderType.Text is "LMT" or "STP")
                numPrice.ReadOnly = false;
            else
                numPrice.ReadOnly = true;

            if (chkBracket_CheckedChanged != null)
            {
                if (chkBracket.Checked)
                {
                    if (cbOrderType.Text is "LMT" or "STP")
                    {
                        try
                        {
                            numQuantity.Value = Math.Abs(Math.Floor(numRisk.Value / (numPrice.Value - Convert.ToDecimal(tbStopLoss.Text))));
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        numQuantity.Value = Math.Abs(Math.Floor(numRisk.Value / (Convert.ToDecimal(tbLast.Text) - Convert.ToDecimal(tbStopLoss.Text))));
                    }
                }
            }
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

        /****************** DISABLED. NOT IN USE RIGHT NOW
        private void OrderType_Changed(object sender, EventArgs e)
        {
            if (cbOrderType.Text is "MKT" or "SNAP MKT" or "SNAP MID" or "SNAP PRIM")
            {
                numPrice.ReadOnly = true;
            }
            else
                numPrice.ReadOnly = false;
        }
        *********************/

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome and thank you for trying IBKR Trader. This is intended for use with the TWS API or IB Gateway and an IBKR Pro account.\r\n\r\nPlease check Global Configuration > API > Settings. Enable ActiveX and Socket Clients, uncheck \"Read-Only\". Set the Socket Port #, then Apply and \"OK\".\r\nIn IBKR Trader app, set the Port # first to the same as you set in TWS.\r\nThen press Connect. Make sure the Bid/Ask/Last prices are being updated in real-time.\r\n\r\nQUICKLY SET LIMIT PRICE:\r\nYou may click the current Bid, Ask, or Last to set that price in the Price box.\r\n\r\nORDER TYPES:\r\nSNAP MKT will get you in automatically at the curret ASK for a Buy, and at the current BID for a Sell.\r\nSNAP MID will put you in the middle of the bid/ask.\r\nSNAP PRIM will put you at the current BID for a Buy, and at the current ASK for a Sell (for adding liquidity).\r\n\r\nROUTING:\r\nYou may leave the Route as SMART (default) or direct route to ISLAND (NSDQ) or EDGX.\r\n\r\nUSING $ RISK:\nIf you check the box \"Use $ Risk + Stop Loss,\" the Qty box will be disabled. Input the $ amount you wish to risk in the $ Risk box, and the Stop Loss price for the bracket order, after which the amount of shares will be automatically calculated once you click Buy or Sell, and will update in real-time with the approximate quantity.\r\nNOTE: The immediate calculation on clicking Buy or Sell is correct and accurate, but the Qty shown changing in the box in real-time is approximated.\r\n\r\nTake Profit function is not enabled at this moment.\r\n\r\nLINK/SYNC:\r\nThis tool is linked to TWS link Group 4, and will therefore change the tickers within TWS windows on group 4.\r\n\r\nDISCLAIMER: I AM NOT RESPONSIBLE FOR FINANCIAL LOSS/GAIN YOU MAY INCUR DUE TO MISCLICK, MISUSE, OR MALFUNCTION OF THE TRADING APP. USE AT YOUR OWN RISK. PRACTICE IN A PAPER TRADING ACCOUNT TO VERIFY ALL FUNCTIONS BEFORE USING IN A LIVE ACCOUNT.");
        }

        private void cbAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAlwaysOnTop.Checked == true)
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            else
                SetWindowPos(this.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
    }
}