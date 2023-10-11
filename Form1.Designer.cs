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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
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
            tbTakeProfit = new TextBox();
            btnCancelLast = new Button();
            btnCancelAll = new Button();
            label10 = new Label();
            label2 = new Label();
            numPort = new NumericUpDown();
            chkBracket = new CheckBox();
            cbTakeProfit = new CheckBox();
            label5 = new Label();
            btnHelp = new Button();
            label9 = new Label();
            numRisk = new NumericUpDown();
            labelSpread = new Label();
            dataGridView1 = new DataGridView();
            colTime = new DataGridViewTextBoxColumn();
            colid = new DataGridViewTextBoxColumn();
            colSymbol = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colTrigger = new DataGridViewTextBoxColumn();
            colShares = new DataGridViewTextBoxColumn();
            colSide = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colFill = new DataGridViewTextBoxColumn();
            colCancel = new DataGridViewTextBoxColumn();
            cbAlwaysOnTop = new CheckBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tbTotalPnl = new TextBox();
            dataGridView4 = new DataGridView();
            col_Symbol = new DataGridViewTextBoxColumn();
            col_Position = new DataGridViewTextBoxColumn();
            col_Price = new DataGridViewTextBoxColumn();
            col_Openpnl = new DataGridViewTextBoxColumn();
            col_Closedpnl = new DataGridViewTextBoxColumn();
            col_Markedpnl = new DataGridViewTextBoxColumn();
            tbShortable = new TextBox();
            label11 = new Label();
            btnClose = new Button();
            btnCloseHalf = new Button();
            labelName = new Label();
            tooltipPort = new ToolTip(components);
            btnCloseQtr = new Button();
            tooltipClosePortion = new ToolTip(components);
            panel1 = new Panel();
            labelChange = new Label();
            panel2 = new Panel();
            labelSinceOpen = new Label();
            labelVolume = new Label();
            labelAvgVol = new Label();
            btnS2BE = new Button();
            btnPosition = new Button();
            contextFormRightClick = new ContextMenuStrip(components);
            toolstripBorderToggle = new ToolStripMenuItem();
            tbStopLoss = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRisk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            contextFormRightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbStopLoss).BeginInit();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(381, 4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(78, 23);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // cbSymbol
            // 
            cbSymbol.AllowDrop = true;
            cbSymbol.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbSymbol.FormattingEnabled = true;
            cbSymbol.Items.AddRange(new object[] { "MSFT", "TSLA", "IBM", "AMD", "NVDA", "META", "SPY", "QQQ" });
            cbSymbol.Location = new Point(12, 45);
            cbSymbol.MaxLength = 6;
            cbSymbol.Name = "cbSymbol";
            cbSymbol.Size = new Size(68, 25);
            cbSymbol.TabIndex = 1;
            cbSymbol.Text = "TSLA";
            cbSymbol.SelectedIndexChanged += cbSymbol_SelectedIndexChanged;
            cbSymbol.DragDrop += cbSymbol_DragDrop;
            cbSymbol.KeyDown += cbSymbol_KeyDown;
            cbSymbol.KeyPress += cbSymbol_KeyPress;
            // 
            // lbData
            // 
            lbData.FormattingEnabled = true;
            lbData.ItemHeight = 15;
            lbData.Location = new Point(12, 230);
            lbData.Name = "lbData";
            lbData.Size = new Size(392, 64);
            lbData.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 31);
            label1.Name = "label1";
            label1.Size = new Size(44, 13);
            label1.TabIndex = 3;
            label1.Text = "Symbol";
            // 
            // numQuantity
            // 
            numQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numQuantity.Location = new Point(86, 45);
            numQuantity.Margin = new Padding(1);
            numQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(60, 25);
            numQuantity.TabIndex = 4;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numPrice.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numPrice.Location = new Point(106, 87);
            numPrice.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(88, 25);
            numPrice.TabIndex = 6;
            numPrice.ValueChanged += UpdateRiskQty;
            // 
            // Price
            // 
            Price.AutoSize = true;
            Price.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            Price.Location = new Point(106, 73);
            Price.Name = "Price";
            Price.Size = new Size(31, 13);
            Price.TabIndex = 7;
            Price.Text = "Price";
            // 
            // cbMarket
            // 
            cbMarket.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbMarket.FormattingEnabled = true;
            cbMarket.Items.AddRange(new object[] { "SMART", "ISLAND", "EDGX" });
            cbMarket.Location = new Point(326, 44);
            cbMarket.Name = "cbMarket";
            cbMarket.Size = new Size(71, 25);
            cbMarket.TabIndex = 8;
            cbMarket.Text = "SMART";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(326, 31);
            label3.Name = "label3";
            label3.Size = new Size(38, 13);
            label3.TabIndex = 9;
            label3.Text = "Route";
            // 
            // cbOrderType
            // 
            cbOrderType.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbOrderType.FormattingEnabled = true;
            cbOrderType.Items.AddRange(new object[] { "LMT", "MKT", "STP", "SNAP MKT", "SNAP MID", "SNAP PRIM" });
            cbOrderType.Location = new Point(12, 87);
            cbOrderType.Name = "cbOrderType";
            cbOrderType.Size = new Size(88, 25);
            cbOrderType.TabIndex = 10;
            cbOrderType.Text = "LMT";
            cbOrderType.TextChanged += UpdateRiskQty;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 73);
            label4.Name = "label4";
            label4.Size = new Size(30, 13);
            label4.TabIndex = 12;
            label4.Text = "Type";
            // 
            // cbTif
            // 
            cbTif.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbTif.FormattingEnabled = true;
            cbTif.Items.AddRange(new object[] { "DAY", "GTC" });
            cbTif.Location = new Point(403, 44);
            cbTif.Name = "cbTif";
            cbTif.Size = new Size(54, 25);
            cbTif.TabIndex = 16;
            cbTif.Text = "DAY";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(403, 31);
            label6.Name = "label6";
            label6.Size = new Size(22, 13);
            label6.TabIndex = 17;
            label6.Text = "TIF";
            // 
            // tbBid
            // 
            tbBid.BackColor = SystemColors.ControlLight;
            tbBid.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            tbBid.Location = new Point(200, 87);
            tbBid.Name = "tbBid";
            tbBid.ReadOnly = true;
            tbBid.Size = new Size(69, 25);
            tbBid.TabIndex = 18;
            tbBid.Text = "0.00";
            tbBid.Click += tbBid_Click;
            // 
            // tbAsk
            // 
            tbAsk.BackColor = SystemColors.ControlLight;
            tbAsk.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            tbAsk.Location = new Point(275, 87);
            tbAsk.Name = "tbAsk";
            tbAsk.ReadOnly = true;
            tbAsk.Size = new Size(69, 25);
            tbAsk.TabIndex = 19;
            tbAsk.Text = "0.00";
            tbAsk.Click += tbAsk_Click;
            // 
            // B
            // 
            B.AutoSize = true;
            B.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            B.Location = new Point(200, 73);
            B.Name = "B";
            B.Size = new Size(23, 13);
            B.TabIndex = 20;
            B.Text = "Bid";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(319, 73);
            label7.Name = "label7";
            label7.Size = new Size(25, 13);
            label7.TabIndex = 21;
            label7.Text = "Ask";
            // 
            // tbLast
            // 
            tbLast.BackColor = SystemColors.ControlLight;
            tbLast.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbLast.Location = new Point(356, 87);
            tbLast.Name = "tbLast";
            tbLast.ReadOnly = true;
            tbLast.Size = new Size(71, 25);
            tbLast.TabIndex = 22;
            tbLast.Text = "0.00";
            tbLast.Click += tbLast_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(400, 73);
            label8.Name = "label8";
            label8.Size = new Size(27, 13);
            label8.TabIndex = 23;
            label8.Text = "Last";
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(465, 4);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(79, 23);
            btnDisconnect.TabIndex = 26;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnSell
            // 
            btnSell.BackColor = Color.LightCoral;
            btnSell.FlatStyle = FlatStyle.Flat;
            btnSell.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnSell.ForeColor = Color.Black;
            btnSell.Location = new Point(12, 118);
            btnSell.Name = "btnSell";
            btnSell.Size = new Size(77, 28);
            btnSell.TabIndex = 27;
            btnSell.Text = "SELL";
            btnSell.UseVisualStyleBackColor = false;
            btnSell.Click += btnSell_Click;
            // 
            // btnBuy
            // 
            btnBuy.BackColor = Color.LightGreen;
            btnBuy.FlatStyle = FlatStyle.Flat;
            btnBuy.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnBuy.ForeColor = Color.Black;
            btnBuy.Location = new Point(95, 118);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(77, 28);
            btnBuy.TabIndex = 28;
            btnBuy.Text = "BUY";
            btnBuy.UseVisualStyleBackColor = false;
            btnBuy.Click += btnBuy_Click;
            // 
            // chkOutside
            // 
            chkOutside.AutoSize = true;
            chkOutside.Location = new Point(463, 47);
            chkOutside.Name = "chkOutside";
            chkOutside.Size = new Size(56, 19);
            chkOutside.TabIndex = 29;
            chkOutside.Text = "DAY+";
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
            listViewTns.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            listViewTns.ForeColor = Color.White;
            listViewTns.HeaderStyle = ColumnHeaderStyle.None;
            listViewTns.Location = new Point(410, 154);
            listViewTns.Name = "listViewTns";
            listViewTns.Size = new Size(230, 395);
            listViewTns.TabIndex = 30;
            listViewTns.UseCompatibleStateImageBehavior = false;
            listViewTns.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Price";
            columnHeader1.Width = 65;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Shares";
            columnHeader2.Width = 55;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Time";
            columnHeader3.TextAlign = HorizontalAlignment.Right;
            columnHeader3.Width = 86;
            // 
            // tbTakeProfit
            // 
            tbTakeProfit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbTakeProfit.Location = new Point(294, 118);
            tbTakeProfit.Name = "tbTakeProfit";
            tbTakeProfit.ReadOnly = true;
            tbTakeProfit.Size = new Size(88, 25);
            tbTakeProfit.TabIndex = 33;
            tbTakeProfit.Text = "0.00";
            // 
            // btnCancelLast
            // 
            btnCancelLast.BackColor = Color.Yellow;
            btnCancelLast.FlatStyle = FlatStyle.Flat;
            btnCancelLast.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancelLast.Location = new Point(200, 118);
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
            btnCancelAll.FlatStyle = FlatStyle.Flat;
            btnCancelAll.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancelAll.Location = new Point(438, 87);
            btnCancelAll.Name = "btnCancelAll";
            btnCancelAll.Size = new Size(76, 25);
            btnCancelAll.TabIndex = 36;
            btnCancelAll.Text = "CXL ALL";
            btnCancelAll.UseVisualStyleBackColor = false;
            btnCancelAll.Click += btnCancelAll_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(550, 10);
            label10.Name = "label10";
            label10.Size = new Size(35, 15);
            label10.TabIndex = 37;
            label10.Text = "PORT";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(86, 31);
            label2.Name = "label2";
            label2.Size = new Size(51, 13);
            label2.TabIndex = 38;
            label2.Text = "Quantity";
            // 
            // numPort
            // 
            numPort.Location = new Point(590, 6);
            numPort.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(48, 23);
            numPort.TabIndex = 39;
            tooltipPort.SetToolTip(numPort, "Use the port # that is in your TWS API settings.\r\n");
            numPort.Value = new decimal(new int[] { 7497, 0, 0, 0 });
            // 
            // chkBracket
            // 
            chkBracket.AutoSize = true;
            chkBracket.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            chkBracket.Location = new Point(239, 30);
            chkBracket.Margin = new Padding(2);
            chkBracket.Name = "chkBracket";
            chkBracket.Size = new Size(64, 17);
            chkBracket.TabIndex = 40;
            chkBracket.Text = "SL + $R";
            chkBracket.UseVisualStyleBackColor = true;
            chkBracket.CheckedChanged += chkBracket_CheckedChanged;
            // 
            // cbTakeProfit
            // 
            cbTakeProfit.AutoSize = true;
            cbTakeProfit.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            cbTakeProfit.Location = new Point(386, 122);
            cbTakeProfit.Name = "cbTakeProfit";
            cbTakeProfit.Size = new Size(40, 19);
            cbTakeProfit.TabIndex = 41;
            cbTakeProfit.Text = "TP";
            cbTakeProfit.UseVisualStyleBackColor = true;
            cbTakeProfit.CheckedChanged += cbTakeProfit_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
            label5.Location = new Point(12, 214);
            label5.Name = "label5";
            label5.Size = new Size(116, 13);
            label5.TabIndex = 42;
            label5.Text = "Linked to TWS Group 4";
            // 
            // btnHelp
            // 
            btnHelp.Location = new Point(12, 186);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(47, 23);
            btnHelp.TabIndex = 43;
            btnHelp.Text = "Help";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(173, 31);
            label9.Name = "label9";
            label9.Size = new Size(37, 13);
            label9.TabIndex = 44;
            label9.Text = "$ Risk";
            // 
            // numRisk
            // 
            numRisk.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numRisk.Location = new Point(173, 45);
            numRisk.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numRisk.Name = "numRisk";
            numRisk.Size = new Size(61, 25);
            numRisk.TabIndex = 45;
            numRisk.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numRisk.ValueChanged += UpdateRiskQty;
            // 
            // labelSpread
            // 
            labelSpread.AutoSize = true;
            labelSpread.Location = new Point(258, 72);
            labelSpread.Name = "labelSpread";
            labelSpread.Size = new Size(22, 15);
            labelSpread.TabIndex = 46;
            labelSpread.Text = "0.0";
            labelSpread.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.AppWorkspace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colTime, colid, colSymbol, colPrice, colTrigger, colShares, colSide, colType, colStatus, colFill, colCancel });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.Black;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(381, 146);
            dataGridView1.TabIndex = 47;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            // 
            // colTime
            // 
            colTime.HeaderText = "Time";
            colTime.Name = "colTime";
            // 
            // colid
            // 
            colid.HeaderText = "ID";
            colid.Name = "colid";
            // 
            // colSymbol
            // 
            colSymbol.HeaderText = "Symbol";
            colSymbol.Name = "colSymbol";
            // 
            // colPrice
            // 
            colPrice.HeaderText = "Price";
            colPrice.Name = "colPrice";
            // 
            // colTrigger
            // 
            colTrigger.HeaderText = "AuxPrice";
            colTrigger.Name = "colTrigger";
            // 
            // colShares
            // 
            colShares.HeaderText = "Shares";
            colShares.Name = "colShares";
            // 
            // colSide
            // 
            colSide.HeaderText = "Side";
            colSide.Name = "colSide";
            // 
            // colType
            // 
            colType.HeaderText = "Type";
            colType.Name = "colType";
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            // 
            // colFill
            // 
            colFill.HeaderText = "Fill";
            colFill.Name = "colFill";
            // 
            // colCancel
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 192, 192);
            colCancel.DefaultCellStyle = dataGridViewCellStyle2;
            colCancel.HeaderText = "X";
            colCancel.Name = "colCancel";
            // 
            // cbAlwaysOnTop
            // 
            cbAlwaysOnTop.AutoSize = true;
            cbAlwaysOnTop.Location = new Point(68, 187);
            cbAlwaysOnTop.Name = "cbAlwaysOnTop";
            cbAlwaysOnTop.Size = new Size(104, 19);
            cbAlwaysOnTop.TabIndex = 48;
            cbAlwaysOnTop.Text = "Always On Top";
            cbAlwaysOnTop.UseVisualStyleBackColor = true;
            cbAlwaysOnTop.CheckedChanged += cbAlwaysOnTop_CheckedChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 300);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(392, 180);
            tabControl1.TabIndex = 49;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(384, 152);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Orders";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tbTotalPnl);
            tabPage2.Controls.Add(dataGridView4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(384, 152);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Positions";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbTotalPnl
            // 
            tbTotalPnl.BackColor = Color.Black;
            tbTotalPnl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tbTotalPnl.ForeColor = Color.White;
            tbTotalPnl.Location = new Point(503, 129);
            tbTotalPnl.Name = "tbTotalPnl";
            tbTotalPnl.Size = new Size(100, 23);
            tbTotalPnl.TabIndex = 1;
            tbTotalPnl.Text = "0.00";
            tbTotalPnl.TextAlign = HorizontalAlignment.Right;
            // 
            // dataGridView4
            // 
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Columns.AddRange(new DataGridViewColumn[] { col_Symbol, col_Position, col_Price, col_Openpnl, col_Closedpnl, col_Markedpnl });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Black;
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridView4.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridView4.Location = new Point(3, 3);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersVisible = false;
            dataGridView4.RowTemplate.Height = 25;
            dataGridView4.ShowCellToolTips = false;
            dataGridView4.Size = new Size(381, 124);
            dataGridView4.TabIndex = 0;
            dataGridView4.CellFormatting += dataGridView4_CellFormatting;
            // 
            // col_Symbol
            // 
            col_Symbol.HeaderText = "Symbol";
            col_Symbol.Name = "col_Symbol";
            col_Symbol.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Position
            // 
            col_Position.HeaderText = "Position";
            col_Position.Name = "col_Position";
            col_Position.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Price
            // 
            col_Price.HeaderText = "Price";
            col_Price.Name = "col_Price";
            // 
            // col_Openpnl
            // 
            col_Openpnl.HeaderText = "Open PnL";
            col_Openpnl.Name = "col_Openpnl";
            // 
            // col_Closedpnl
            // 
            col_Closedpnl.HeaderText = "Closed PnL";
            col_Closedpnl.Name = "col_Closedpnl";
            // 
            // col_Markedpnl
            // 
            col_Markedpnl.HeaderText = "Marked PnL";
            col_Markedpnl.Name = "col_Markedpnl";
            // 
            // tbShortable
            // 
            tbShortable.BorderStyle = BorderStyle.FixedSingle;
            tbShortable.Location = new Point(588, 33);
            tbShortable.MaximumSize = new Size(50, 20);
            tbShortable.Name = "tbShortable";
            tbShortable.ReadOnly = true;
            tbShortable.Size = new Size(50, 20);
            tbShortable.TabIndex = 50;
            tbShortable.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(550, 35);
            label11.Name = "label11";
            label11.Size = new Size(38, 15);
            label11.TabIndex = 51;
            label11.Text = "Shrtbl";
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Chocolate;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Location = new Point(523, 59);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(106, 25);
            btnClose.TabIndex = 52;
            btnClose.Text = "CLOSE POS";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += ClosePosition;
            // 
            // btnCloseHalf
            // 
            btnCloseHalf.BackColor = Color.Salmon;
            btnCloseHalf.FlatStyle = FlatStyle.Flat;
            btnCloseHalf.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCloseHalf.Location = new Point(579, 87);
            btnCloseHalf.Name = "btnCloseHalf";
            btnCloseHalf.Size = new Size(50, 23);
            btnCloseHalf.TabIndex = 53;
            btnCloseHalf.Text = "50%";
            tooltipClosePortion.SetToolTip(btnCloseHalf, "Will close 50% of pos at MKT\r\n");
            btnCloseHalf.UseVisualStyleBackColor = false;
            btnCloseHalf.Click += CloseHalfPos;
            // 
            // labelName
            // 
            labelName.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.Location = new Point(0, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(306, 19);
            labelName.TabIndex = 54;
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tooltipPort
            // 
            tooltipPort.ToolTipTitle = "Select Port #";
            // 
            // btnCloseQtr
            // 
            btnCloseQtr.BackColor = Color.Salmon;
            btnCloseQtr.FlatStyle = FlatStyle.Flat;
            btnCloseQtr.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCloseQtr.Location = new Point(523, 87);
            btnCloseQtr.Name = "btnCloseQtr";
            btnCloseQtr.Size = new Size(50, 23);
            btnCloseQtr.TabIndex = 55;
            btnCloseQtr.Text = "25%";
            tooltipClosePortion.SetToolTip(btnCloseQtr, "Will close 25% of pos at MKT\r\n");
            btnCloseQtr.UseVisualStyleBackColor = false;
            btnCloseQtr.Click += btnCloseQtr_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(labelName);
            panel1.Controls.Add(labelChange);
            panel1.Location = new Point(12, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(363, 23);
            panel1.TabIndex = 56;
            // 
            // labelChange
            // 
            labelChange.Anchor = AnchorStyles.Right;
            labelChange.AutoSize = true;
            labelChange.Location = new Point(312, 2);
            labelChange.Name = "labelChange";
            labelChange.Size = new Size(13, 15);
            labelChange.TabIndex = 2;
            labelChange.Text = "0";
            labelChange.TextAlign = ContentAlignment.TopRight;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(labelSinceOpen);
            panel2.Controls.Add(labelVolume);
            panel2.Controls.Add(labelAvgVol);
            panel2.Location = new Point(12, 157);
            panel2.Name = "panel2";
            panel2.Size = new Size(392, 24);
            panel2.TabIndex = 57;
            // 
            // labelSinceOpen
            // 
            labelSinceOpen.AutoSize = true;
            labelSinceOpen.Location = new Point(300, 2);
            labelSinceOpen.Name = "labelSinceOpen";
            labelSinceOpen.Size = new Size(61, 15);
            labelSinceOpen.TabIndex = 3;
            labelSinceOpen.Text = "SncOpn: 0";
            labelSinceOpen.TextAlign = ContentAlignment.TopRight;
            // 
            // labelVolume
            // 
            labelVolume.AutoSize = true;
            labelVolume.Location = new Point(-2, 2);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new Size(29, 15);
            labelVolume.TabIndex = 0;
            labelVolume.Text = "Vol: ";
            // 
            // labelAvgVol
            // 
            labelAvgVol.AutoSize = true;
            labelAvgVol.Location = new Point(98, 2);
            labelAvgVol.Name = "labelAvgVol";
            labelAvgVol.Size = new Size(50, 15);
            labelAvgVol.TabIndex = 1;
            labelAvgVol.Text = "AvgVol: ";
            // 
            // btnS2BE
            // 
            btnS2BE.BackColor = Color.SkyBlue;
            btnS2BE.FlatStyle = FlatStyle.Flat;
            btnS2BE.Location = new Point(523, 116);
            btnS2BE.Name = "btnS2BE";
            btnS2BE.Size = new Size(106, 24);
            btnS2BE.TabIndex = 58;
            btnS2BE.Text = "Stop to BE";
            btnS2BE.UseVisualStyleBackColor = false;
            btnS2BE.Click += btnS2BE_Click;
            // 
            // btnPosition
            // 
            btnPosition.FlatAppearance.BorderColor = Color.Gray;
            btnPosition.FlatStyle = FlatStyle.Flat;
            btnPosition.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnPosition.Location = new Point(147, 45);
            btnPosition.Margin = new Padding(0);
            btnPosition.Name = "btnPosition";
            btnPosition.Size = new Size(21, 25);
            btnPosition.TabIndex = 59;
            btnPosition.Text = "P";
            btnPosition.UseVisualStyleBackColor = true;
            btnPosition.Click += btnPosition_Click;
            // 
            // contextFormRightClick
            // 
            contextFormRightClick.Items.AddRange(new ToolStripItem[] { toolstripBorderToggle });
            contextFormRightClick.Name = "contextFormRightClick";
            contextFormRightClick.Size = new Size(148, 26);
            // 
            // toolstripBorderToggle
            // 
            toolstripBorderToggle.CheckOnClick = true;
            toolstripBorderToggle.Name = "toolstripBorderToggle";
            toolstripBorderToggle.Size = new Size(147, 22);
            toolstripBorderToggle.Text = "Toggle Border";
            toolstripBorderToggle.Click += ToolstripBorderToggle_Click;
            // 
            // tbStopLoss
            // 
            tbStopLoss.DecimalPlaces = 2;
            tbStopLoss.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbStopLoss.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            tbStopLoss.Location = new Point(239, 44);
            tbStopLoss.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            tbStopLoss.Name = "tbStopLoss";
            tbStopLoss.ReadOnly = true;
            tbStopLoss.Size = new Size(81, 25);
            tbStopLoss.TabIndex = 60;
            tbStopLoss.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(645, 551);
            ContextMenuStrip = contextFormRightClick;
            Controls.Add(tbStopLoss);
            Controls.Add(btnPosition);
            Controls.Add(btnS2BE);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnCloseQtr);
            Controls.Add(btnCloseHalf);
            Controls.Add(btnClose);
            Controls.Add(tbShortable);
            Controls.Add(tabControl1);
            Controls.Add(cbAlwaysOnTop);
            Controls.Add(listViewTns);
            Controls.Add(labelSpread);
            Controls.Add(numRisk);
            Controls.Add(label9);
            Controls.Add(btnHelp);
            Controls.Add(label5);
            Controls.Add(cbTakeProfit);
            Controls.Add(chkBracket);
            Controls.Add(numPort);
            Controls.Add(label2);
            Controls.Add(btnCancelAll);
            Controls.Add(btnCancelLast);
            Controls.Add(tbTakeProfit);
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
            Controls.Add(label10);
            Controls.Add(label11);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "IBKR Trader";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRisk).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            contextFormRightClick.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tbStopLoss).EndInit();
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
        private ComboBox cbTif;
        private Label label6;
        private Label B;
        private Label label7;
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
        private TextBox tbTakeProfit;
        private Button btnCancelLast;
        private Button btnCancelAll;
        private Label label10;
        private Label label2;
        private NumericUpDown numPort;
        private CheckBox chkBracket;
        private CheckBox cbTakeProfit;
        private Label label5;
        private ListBox lbHelp;
        private Button btnHelp;
        private Label label9;
        private NumericUpDown numRisk;
        private Label labelSpread;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewTextBoxColumn colid;
        private DataGridViewTextBoxColumn colSymbol;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colTrigger;
        private DataGridViewTextBoxColumn colShares;
        private DataGridViewTextBoxColumn colSide;
        private DataGridViewTextBoxColumn colType;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colFill;
        private DataGridViewTextBoxColumn colCancel;
        private CheckBox cbAlwaysOnTop;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView4;
        private TextBox tbTotalPnl;
        private DataGridViewTextBoxColumn col_Symbol;
        private DataGridViewTextBoxColumn col_Position;
        private DataGridViewTextBoxColumn col_Price;
        private DataGridViewTextBoxColumn col_Openpnl;
        private DataGridViewTextBoxColumn col_Closedpnl;
        private DataGridViewTextBoxColumn col_Markedpnl;
        private TextBox tbShortable;
        private Label label11;
        private Button btnClose;
        private Button btnCloseHalf;
        private Label labelName;
        private ToolTip tooltipPort;
        private Button btnCloseQtr;
        private ToolTip tooltipClosePortion;
        private Panel panel1;
        private Panel panel2;
        private Label labelVolume;
        private Label labelAvgVol;
        private Label labelChange;
        private Label labelSinceOpen;
        private Button btnS2BE;
        public TextBox tbLast;
        private Button btnPosition;
        private ContextMenuStrip contextFormRightClick;
        private ToolStripMenuItem toolstripBorderToggle;
        public TextBox tbBid;
        public TextBox tbAsk;
        private NumericUpDown tbStopLoss;
    }
}