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
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private DataTable TickbyTicKTable = new DataTable();

        // DELEGATE ENABLES ASYNCHRONOUS CALLS FOR SETTING TEXT PROPERTY ON LISTBOX
        //delegate void SetTextCallback(string text);
        //delegate void SetTextCallbackTickPrice(string _tickPrice);

        // Create ibClient object to represent the connection
        EWrapperImpl ibClient;

        public Form1()
        {
            InitializeComponent();
            //listViewTns.DoubleBuffering(true); 
            
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, true, null);
            }

            // Instantiate the ibClient
            ibClient = new EWrapperImpl();

            DataColumn timeColumn = new DataColumn("Time");
            DataColumn priceColumn = new DataColumn("Price");
            DataColumn sizeColumn = new DataColumn("Size");

            TickbyTicKTable.Columns.Add(timeColumn);
            TickbyTicKTable.Columns.Add(priceColumn);
            TickbyTicKTable.Columns.Add(sizeColumn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Auto-Click connect on launch - DISABLED because app does not launch if the port # is incorrect.
            // btnConnect.PerformClick();

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //or even better .DisableResizing. 
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dataGridView1.RowHeadersVisible = false; // set it to false if not needed
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

                    //ibClient.ClientSocket.SetConnectOptions("+PACEAPI");    // Option to pace msgs to 50/second.
                    int port = (int)numPort.Value;
                    ibClient.ClientSocket.eConnect("", port, 17);

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
                    //ibClient.ClientSocket.subscribeToGroupEvents(9002, 4);

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
            if (ibClient.ClientSocket.IsConnected())
            {
                btnConnect.Text = "Connected";
                btnConnect.BackColor = Color.LightGreen;
            }
            else
            {
                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.Gainsboro;
            }

            ibClient.ClientSocket.cancelTickByTickData(1);
            ibClient.ClientSocket.cancelTickByTickData(2);

            // clears contents of TnS when changing tickers
            // dataGridView1.Rows.Clear();


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

            // Tick by tick TESTING -- SUCCESS!
            ibClient.ClientSocket.reqTickByTickData(1, contract, "Last", 0, false);
            ibClient.ClientSocket.reqTickByTickData(2, contract, "BidAsk", 0, true);

            // request contract details based on contract that was created above
            // ibClient.ClientSocket.reqContractDetails(88, contract);


        }

        private double theBid = 0;  // decalring variables to be used in the scope.
        private double theAsk = 0;
        delegate void SetTextCallbackBidAskTicks(double bidTick, double askTick);
        public void BidAskTick(double bidTick, double askTick)  // the only variables I need from TickByTickBidAsk
        {
            theAsk = askTick;
            theBid = bidTick;
        }

        delegate void SetTextCallbackTickByTick(string time, double price, decimal size);
        public void TickByTick(string time, double price, decimal size)  // variables for actual Last prices on tickbytick basis.
        {
            if (dataGridView1.InvokeRequired)
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
            {/*
                // LISTVIEW TIME AND SALES
                try
                {
                    // Proper way to adapt SIZE from tickstring data value and get rid of trailing zeroes.
                    string strShareSize = size.ToString("#,##0");

                    string strSaleTime = time; //epoch.ToString("h:mm:ss:ff");  // formatting for time

                    ListViewItem lx = new ListViewItem();

                    if (price > theBid && price < theAsk)
                    {
                        lx.ForeColor = Color.Silver;
                        lx.Text = price.ToString();
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);
                    }
                    if (price >= theAsk)
                    {
                        lx.BackColor = Color.LimeGreen; // listview foreground color
                        lx.Text = price.ToString(); // last price
                        lx.SubItems.Add(strShareSize); // share size
                        lx.SubItems.Add(strSaleTime); // time
                        listViewTns.Items.Insert(0, lx); // use Insert instead of Add listView.Items.Add(li); 
                    }

                    if (price <= theBid)
                    {
                        lx.BackColor = Color.DarkRed;
                        lx.Text = price.ToString();
                        lx.SubItems.Add(strShareSize);
                        lx.SubItems.Add(strSaleTime);
                        listViewTns.Items.Insert(0, lx);
                    }


                }
                catch (Exception)
                {
                    // lbData.Items.Insert(0, "TnS error: " + g);
                }*/


                try
                {
                    dataGridView1.DataSource = TickbyTicKTable;
                    DataRow row = TickbyTicKTable.NewRow();
                    row[0] = time; row[1] = price; row[2] = size.ToString("#,##0");

                    TickbyTicKTable.Rows.InsertAt(row, 0);

                    if (price >= theAsk)
                    {
                        dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.SeaGreen;
                        dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (price <= theBid)
                    {
                        dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.DarkRed;
                        dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Silver;
                    }
                }
                catch (Exception h) { MessageBox.Show("Tns Err: " + h); }

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
        private void AlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (toolstripAlwaysOnTop.Checked == true)
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            else
                SetWindowPos(this.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

    }
    // DOUBLE BUFFER FOR LISTVIEW
    public static class ControlExtensions
    {
        public static void DoubleBuffering(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }

    /* DOUBLE BUFFER FOR LISTVIEW
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }
    */
}
