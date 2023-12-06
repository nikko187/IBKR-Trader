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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnConnect = new Button();
            btnDisconnect = new Button();
            numPort = new NumericUpDown();
            tooltipPort = new ToolTip(components);
            tooltipClosePortion = new ToolTip(components);
            contextFormRightClick = new ContextMenuStrip(components);
            toolstripBorderToggle = new ToolStripMenuItem();
            toolstripTicks = new ToolStripMenuItem();
            toolstripAlwaysOnTop = new ToolStripMenuItem();
            cbSymbol = new ComboBox();
            dataGridView1 = new DataGridView();
            listViewTns = new ListView();
            listviewPrice = new ColumnHeader();
            listviewSize = new ColumnHeader();
            listviewTime = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            contextFormRightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(74, 2);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(63, 25);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(193, 2);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(76, 25);
            btnDisconnect.TabIndex = 26;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // numPort
            // 
            numPort.BackColor = Color.Black;
            numPort.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            numPort.ForeColor = Color.White;
            numPort.Location = new Point(141, 2);
            numPort.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(48, 23);
            numPort.TabIndex = 39;
            tooltipPort.SetToolTip(numPort, "Use the port # that is in your TWS API settings.\r\n");
            numPort.Value = new decimal(new int[] { 7497, 0, 0, 0 });
            // 
            // tooltipPort
            // 
            tooltipPort.ToolTipTitle = "Select Port #";
            // 
            // contextFormRightClick
            // 
            contextFormRightClick.Items.AddRange(new ToolStripItem[] { toolstripBorderToggle, toolstripTicks, toolstripAlwaysOnTop });
            contextFormRightClick.Name = "contextFormRightClick";
            contextFormRightClick.Size = new Size(153, 70);
            // 
            // toolstripBorderToggle
            // 
            toolstripBorderToggle.CheckOnClick = true;
            toolstripBorderToggle.Name = "toolstripBorderToggle";
            toolstripBorderToggle.Size = new Size(152, 22);
            toolstripBorderToggle.Text = "Toggle Border";
            toolstripBorderToggle.Click += ToolstripBorderToggle_Click;
            // 
            // toolstripTicks
            // 
            toolstripTicks.Name = "toolstripTicks";
            toolstripTicks.Size = new Size(152, 22);
            toolstripTicks.Text = "Tick by tick";
            toolstripTicks.Click += ToolstripTickByTick;
            // 
            // toolstripAlwaysOnTop
            // 
            toolstripAlwaysOnTop.CheckOnClick = true;
            toolstripAlwaysOnTop.Name = "toolstripAlwaysOnTop";
            toolstripAlwaysOnTop.Size = new Size(152, 22);
            toolstripAlwaysOnTop.Text = "Always On Top";
            toolstripAlwaysOnTop.CheckedChanged += AlwaysOnTop_CheckedChanged;
            // 
            // cbSymbol
            // 
            cbSymbol.AllowDrop = true;
            cbSymbol.BackColor = Color.Black;
            cbSymbol.FlatStyle = FlatStyle.Flat;
            cbSymbol.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cbSymbol.ForeColor = Color.White;
            cbSymbol.FormattingEnabled = true;
            cbSymbol.Items.AddRange(new object[] { "MSFT", "TSLA", "IBM", "AMD", "NVDA", "META", "SPY", "QQQ" });
            cbSymbol.Location = new Point(2, 2);
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
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.WhiteSmoke;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.Location = new Point(2, 31);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.BackColor = Color.Black;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.LightGray;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowTemplate.Height = 19;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.Size = new Size(328, 854);
            dataGridView1.TabIndex = 40;
            // 
            // listViewTns
            // 
            listViewTns.AllowColumnReorder = true;
            listViewTns.AutoArrange = false;
            listViewTns.BackColor = Color.Black;
            listViewTns.Columns.AddRange(new ColumnHeader[] { listviewPrice, listviewSize, listviewTime });
            listViewTns.ForeColor = Color.White;
            listViewTns.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewTns.HideSelection = true;
            listViewTns.LabelWrap = false;
            listViewTns.Location = new Point(252, 31);
            listViewTns.MultiSelect = false;
            listViewTns.Name = "listViewTns";
            listViewTns.Scrollable = false;
            listViewTns.ShowGroups = false;
            listViewTns.Size = new Size(212, 809);
            listViewTns.TabIndex = 41;
            listViewTns.UseCompatibleStateImageBehavior = false;
            listViewTns.View = View.Details;
            listViewTns.Visible = false;
            // 
            // listviewPrice
            // 
            listviewPrice.Text = "Price";
            // 
            // listviewSize
            // 
            listviewSize.Text = "Size";
            // 
            // listviewTime
            // 
            listviewTime.Text = "Time";
            listviewTime.Width = 80;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(439, 885);
            ContextMenuStrip = contextFormRightClick;
            Controls.Add(listViewTns);
            Controls.Add(dataGridView1);
            Controls.Add(numPort);
            Controls.Add(btnDisconnect);
            Controls.Add(cbSymbol);
            Controls.Add(btnConnect);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "IBKR Trader T&S";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            contextFormRightClick.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        private void ToolstripTicks_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Button btnConnect;
        private Button btnDisconnect;
        private NumericUpDown numPort;
        private ListBox lbHelp;
        private ToolTip tooltipPort;
        private ToolTip tooltipClosePortion;
        private Label labelChange;
        private ContextMenuStrip contextFormRightClick;
        private ToolStripMenuItem toolstripBorderToggle;
        private ComboBox cbSymbol;
        private ToolStripMenuItem toolstripTicks;
        private ToolStripMenuItem toolstripAlwaysOnTop;
        private DataGridView dataGridView1;
        private ListView listViewTns;
        private ColumnHeader listviewPrice;
        private ColumnHeader listviewSize;
        private ColumnHeader listviewTime;
    }
}