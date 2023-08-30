namespace IBKR_Trader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnConnect = new Button();
            cbSymbol = new ComboBox();
            lbData = new ListBox();
            label1 = new Label();
            numQuantity = new NumericUpDown();
            numPrice = new NumericUpDown();
            Price = new Label();
            cbMarket = new ComboBox();
            label3 = new Label();
            cbOrderType = new ComboBox();
            label4 = new Label();
            tbPrimaryEx = new TextBox();
            PrimaryExchange = new Label();
            cbTif = new ComboBox();
            label6 = new Label();
            tbBid = new TextBox();
            tbAsk = new TextBox();
            B = new Label();
            label7 = new Label();
            tbLast = new TextBox();
            label8 = new Label();
            btnDisconnect = new Button();
            btnSell = new Button();
            btnBuy = new Button();
            chkOutside = new CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            listViewTns = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            label5 = new Label();
            label9 = new Label();
            tbTakeProfit = new TextBox();
            tbStopLoss = new TextBox();
            btnCancelLast = new Button();
            btnCancelAll = new Button();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(88, 25);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // cbSymbol
            // 
            cbSymbol.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbSymbol.FormattingEnabled = true;
            cbSymbol.Items.AddRange(new object[] { "MSFT", "TSLA", "IBM", "AMD", "NVDA", "META", "SPY", "QQQ" });
            cbSymbol.Location = new Point(12, 70);
            cbSymbol.Name = "cbSymbol";
            cbSymbol.Size = new Size(88, 25);
            cbSymbol.TabIndex = 1;
            cbSymbol.Text = "AAPL";
            cbSymbol.SelectedIndexChanged += cbSymbol_SelectedIndexChanged;
            cbSymbol.KeyDown += cbSymbol_KeyDown;
            cbSymbol.KeyPress += cbSymbol_KeyPress;
            // 
            // lbData
            // 
            lbData.FormattingEnabled = true;
            lbData.ItemHeight = 15;
            lbData.Location = new Point(12, 329);
            lbData.Name = "lbData";
            lbData.Size = new Size(397, 124);
            lbData.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 50);
            label1.Name = "label1";
            label1.Size = new Size(51, 17);
            label1.TabIndex = 3;
            label1.Text = "Symbol";
            // 
            // numQuantity
            // 
            numQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numQuantity.Location = new Point(115, 70);
            numQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(88, 25);
            numQuantity.TabIndex = 4;
            numQuantity.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numPrice.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numPrice.Location = new Point(218, 70);
            numPrice.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(88, 25);
            numPrice.TabIndex = 6;
            // 
            // Price
            // 
            Price.AutoSize = true;
            Price.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Price.Location = new Point(218, 52);
            Price.Name = "Price";
            Price.Size = new Size(36, 17);
            Price.TabIndex = 7;
            Price.Text = "Price";
            // 
            // cbMarket
            // 
            cbMarket.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbMarket.FormattingEnabled = true;
            cbMarket.Items.AddRange(new object[] { "SMART", "ARCA", "BATS", "ISLAND", "NYSE" });
            cbMarket.Location = new Point(321, 69);
            cbMarket.Name = "cbMarket";
            cbMarket.Size = new Size(88, 25);
            cbMarket.TabIndex = 8;
            cbMarket.Text = "SMART";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(321, 52);
            label3.Name = "label3";
            label3.Size = new Size(42, 17);
            label3.TabIndex = 9;
            label3.Text = "Route";
            // 
            // cbOrderType
            // 
            cbOrderType.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbOrderType.FormattingEnabled = true;
            cbOrderType.Items.AddRange(new object[] { "LMT", "MKT", "STP" });
            cbOrderType.Location = new Point(12, 125);
            cbOrderType.Name = "cbOrderType";
            cbOrderType.Size = new Size(88, 25);
            cbOrderType.TabIndex = 10;
            cbOrderType.Text = "LMT";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 105);
            label4.Name = "label4";
            label4.Size = new Size(35, 17);
            label4.TabIndex = 12;
            label4.Text = "Type";
            // 
            // tbPrimaryEx
            // 
            tbPrimaryEx.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbPrimaryEx.Location = new Point(115, 125);
            tbPrimaryEx.Name = "tbPrimaryEx";
            tbPrimaryEx.Size = new Size(88, 25);
            tbPrimaryEx.TabIndex = 14;
            tbPrimaryEx.Text = "NASDAQ";
            // 
            // PrimaryExchange
            // 
            PrimaryExchange.AutoSize = true;
            PrimaryExchange.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PrimaryExchange.Location = new Point(115, 107);
            PrimaryExchange.Name = "PrimaryExchange";
            PrimaryExchange.Size = new Size(65, 17);
            PrimaryExchange.TabIndex = 15;
            PrimaryExchange.Text = "PrimaryEx";
            // 
            // cbTif
            // 
            cbTif.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbTif.FormattingEnabled = true;
            cbTif.Items.AddRange(new object[] { "DAY", "GTC" });
            cbTif.Location = new Point(218, 125);
            cbTif.Name = "cbTif";
            cbTif.Size = new Size(88, 25);
            cbTif.TabIndex = 16;
            cbTif.Text = "DAY";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(218, 107);
            label6.Name = "label6";
            label6.Size = new Size(24, 17);
            label6.TabIndex = 17;
            label6.Text = "TIF";
            // 
            // tbBid
            // 
            tbBid.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbBid.Location = new Point(115, 183);
            tbBid.Name = "tbBid";
            tbBid.Size = new Size(88, 25);
            tbBid.TabIndex = 18;
            tbBid.Text = "0.00";
            tbBid.Click += tbBid_Click;
            // 
            // tbAsk
            // 
            tbAsk.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbAsk.Location = new Point(218, 183);
            tbAsk.Name = "tbAsk";
            tbAsk.Size = new Size(88, 25);
            tbAsk.TabIndex = 19;
            tbAsk.Text = "0.00";
            tbAsk.Click += tbAsk_Click;
            // 
            // B
            // 
            B.AutoSize = true;
            B.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            B.Location = new Point(115, 163);
            B.Name = "B";
            B.Size = new Size(26, 17);
            B.TabIndex = 20;
            B.Text = "Bid";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(218, 163);
            label7.Name = "label7";
            label7.Size = new Size(28, 17);
            label7.TabIndex = 21;
            label7.Text = "Ask";
            // 
            // tbLast
            // 
            tbLast.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbLast.Location = new Point(218, 220);
            tbLast.Name = "tbLast";
            tbLast.Size = new Size(88, 25);
            tbLast.TabIndex = 22;
            tbLast.Text = "0.00";
            tbLast.Click += tbLast_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(172, 220);
            label8.Name = "label8";
            label8.Size = new Size(31, 17);
            label8.TabIndex = 23;
            label8.Text = "Last";
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(115, 12);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(88, 25);
            btnDisconnect.TabIndex = 26;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnSell
            // 
            btnSell.BackColor = Color.LightCoral;
            btnSell.FlatStyle = FlatStyle.Popup;
            btnSell.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnSell.ForeColor = Color.Black;
            btnSell.Location = new Point(12, 183);
            btnSell.Name = "btnSell";
            btnSell.Size = new Size(88, 25);
            btnSell.TabIndex = 27;
            btnSell.Text = "SELL";
            btnSell.UseVisualStyleBackColor = false;
            btnSell.Click += btnSell_Click;
            // 
            // btnBuy
            // 
            btnBuy.BackColor = Color.LightGreen;
            btnBuy.FlatStyle = FlatStyle.Popup;
            btnBuy.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnBuy.ForeColor = Color.Black;
            btnBuy.Location = new Point(321, 183);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(88, 25);
            btnBuy.TabIndex = 28;
            btnBuy.Text = "BUY";
            btnBuy.UseVisualStyleBackColor = false;
            btnBuy.Click += btnBuy_Click;
            // 
            // chkOutside
            // 
            chkOutside.AutoSize = true;
            chkOutside.Location = new Point(321, 125);
            chkOutside.Name = "chkOutside";
            chkOutside.Size = new Size(88, 19);
            chkOutside.TabIndex = 29;
            chkOutside.Text = "OutsideRTH";
            chkOutside.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // listViewTns
            // 
            listViewTns.BackColor = Color.Black;
            listViewTns.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listViewTns.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            listViewTns.ForeColor = Color.White;
            listViewTns.Location = new Point(430, 12);
            listViewTns.Name = "listViewTns";
            listViewTns.Size = new Size(248, 441);
            listViewTns.TabIndex = 30;
            listViewTns.UseCompatibleStateImageBehavior = false;
            listViewTns.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Price";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Shares";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Time";
            columnHeader3.Width = 80;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(246, 295);
            label5.Name = "label5";
            label5.Size = new Size(69, 17);
            label5.TabIndex = 31;
            label5.Text = "Take Profit";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(250, 264);
            label9.Name = "label9";
            label9.Size = new Size(65, 17);
            label9.TabIndex = 32;
            label9.Text = "Stop Loss";
            // 
            // tbTakeProfit
            // 
            tbTakeProfit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbTakeProfit.Location = new Point(321, 292);
            tbTakeProfit.Name = "tbTakeProfit";
            tbTakeProfit.Size = new Size(88, 25);
            tbTakeProfit.TabIndex = 33;
            tbTakeProfit.Text = "0.00";
            // 
            // tbStopLoss
            // 
            tbStopLoss.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbStopLoss.Location = new Point(321, 261);
            tbStopLoss.Name = "tbStopLoss";
            tbStopLoss.Size = new Size(88, 25);
            tbStopLoss.TabIndex = 34;
            tbStopLoss.Text = "0.00";
            // 
            // btnCancelLast
            // 
            btnCancelLast.BackColor = Color.Yellow;
            btnCancelLast.FlatStyle = FlatStyle.Popup;
            btnCancelLast.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancelLast.Location = new Point(12, 222);
            btnCancelLast.Name = "btnCancelLast";
            btnCancelLast.Size = new Size(88, 25);
            btnCancelLast.TabIndex = 35;
            btnCancelLast.Text = "CXL Last";
            btnCancelLast.UseVisualStyleBackColor = false;
            btnCancelLast.Click += btnCancelLast_Click;
            // 
            // btnCancelAll
            // 
            btnCancelAll.BackColor = Color.FromArgb(255, 192, 128);
            btnCancelAll.FlatStyle = FlatStyle.Popup;
            btnCancelAll.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancelAll.Location = new Point(321, 220);
            btnCancelAll.Name = "btnCancelAll";
            btnCancelAll.Size = new Size(88, 25);
            btnCancelAll.TabIndex = 36;
            btnCancelAll.Text = "CXL ALL";
            btnCancelAll.UseVisualStyleBackColor = false;
            btnCancelAll.Click += btnCancelAll_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(218, 17);
            label10.Name = "label10";
            label10.Size = new Size(78, 15);
            label10.TabIndex = 37;
            label10.Text = "Use Port 7497";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(694, 465);
            Controls.Add(label10);
            Controls.Add(btnCancelAll);
            Controls.Add(btnCancelLast);
            Controls.Add(tbStopLoss);
            Controls.Add(tbTakeProfit);
            Controls.Add(label9);
            Controls.Add(label5);
            Controls.Add(listViewTns);
            Controls.Add(chkOutside);
            Controls.Add(btnBuy);
            Controls.Add(btnSell);
            Controls.Add(btnDisconnect);
            Controls.Add(label8);
            Controls.Add(tbLast);
            Controls.Add(label7);
            Controls.Add(B);
            Controls.Add(tbAsk);
            Controls.Add(tbBid);
            Controls.Add(label6);
            Controls.Add(cbTif);
            Controls.Add(PrimaryExchange);
            Controls.Add(tbPrimaryEx);
            Controls.Add(label4);
            Controls.Add(cbOrderType);
            Controls.Add(label3);
            Controls.Add(cbMarket);
            Controls.Add(Price);
            Controls.Add(numPrice);
            Controls.Add(numQuantity);
            Controls.Add(label1);
            Controls.Add(lbData);
            Controls.Add(cbSymbol);
            Controls.Add(btnConnect);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "IBKR Trader";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private ComboBox cbSymbol;
        private ListBox lbData;
        private Label label1;
        private NumericUpDown numQuantity;
        private NumericUpDown numPrice;
        private Label Price;
        private ComboBox cbMarket;
        private Label label3;
        private ComboBox cbOrderType;
        private Label label4;
        private TextBox tbPrimaryEx;
        private Label PrimaryExchange;
        private ComboBox cbTif;
        private Label label6;
        private TextBox tbBid;
        private TextBox tbAsk;
        private Label B;
        private Label label7;
        private TextBox tbLast;
        private Label label8;
        private Button btnDisconnect;
        private Button btnSell;
        private Button btnBuy;
        private CheckBox chkOutside;
        private System.Windows.Forms.Timer timer1;
        private ListView listViewTns;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label label5;
        private Label label9;
        private TextBox tbTakeProfit;
        private TextBox tbStopLoss;
        private Button btnCancelLast;
        private Button btnCancelAll;
        private Label label10;
    }
}