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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnConnect = new Button();
            btnDisconnect = new Button();
            numPort = new NumericUpDown();
            tooltipPort = new ToolTip(components);
            tooltipClosePortion = new ToolTip(components);
            contextFormRightClick = new ContextMenuStrip(components);
            toolstripBorderToggle = new ToolStripMenuItem();
            toolstripTicks = new ToolStripMenuItem();
            cbSymbol = new ComboBox();
            datagridviewTns = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            contextFormRightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).BeginInit();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(79, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(63, 25);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(202, 6);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(76, 25);
            btnDisconnect.TabIndex = 26;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // numPort
            // 
            numPort.Location = new Point(148, 6);
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
            contextFormRightClick.Items.AddRange(new ToolStripItem[] { toolstripBorderToggle, toolstripTicks });
            contextFormRightClick.Name = "contextFormRightClick";
            contextFormRightClick.Size = new Size(148, 48);
            // 
            // toolstripBorderToggle
            // 
            toolstripBorderToggle.CheckOnClick = true;
            toolstripBorderToggle.Name = "toolstripBorderToggle";
            toolstripBorderToggle.Size = new Size(147, 22);
            toolstripBorderToggle.Text = "Toggle Border";
            toolstripBorderToggle.Click += ToolstripBorderToggle_Click;
            // 
            // toolstripTicks
            // 
            toolstripTicks.Name = "toolstripTicks";
            toolstripTicks.Size = new Size(147, 22);
            toolstripTicks.Text = "Tick by tick";
            toolstripTicks.Click += ToolstripTickByTick;
            // 
            // cbSymbol
            // 
            cbSymbol.AllowDrop = true;
            cbSymbol.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            cbSymbol.FormattingEnabled = true;
            cbSymbol.Items.AddRange(new object[] { "MSFT", "TSLA", "IBM", "AMD", "NVDA", "META", "SPY", "QQQ" });
            cbSymbol.Location = new Point(5, 4);
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
            // datagridviewTns
            // 
            datagridviewTns.AllowUserToAddRows = false;
            datagridviewTns.AllowUserToDeleteRows = false;
            datagridviewTns.AllowUserToOrderColumns = true;
            datagridviewTns.AllowUserToResizeRows = false;
            datagridviewTns.BackgroundColor = Color.Black;
            datagridviewTns.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            datagridviewTns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            datagridviewTns.ColumnHeadersHeight = 20;
            datagridviewTns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Black;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            datagridviewTns.DefaultCellStyle = dataGridViewCellStyle2;
            datagridviewTns.EditMode = DataGridViewEditMode.EditProgrammatically;
            datagridviewTns.Location = new Point(5, 35);
            datagridviewTns.Name = "datagridviewTns";
            datagridviewTns.RowHeadersVisible = false;
            datagridviewTns.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = Color.Transparent;
            dataGridViewCellStyle3.ForeColor = Color.Silver;
            datagridviewTns.RowsDefaultCellStyle = dataGridViewCellStyle3;
            datagridviewTns.RowTemplate.Height = 20;
            datagridviewTns.Size = new Size(199, 680);
            datagridviewTns.TabIndex = 40;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(210, 718);
            ContextMenuStrip = contextFormRightClick;
            Controls.Add(datagridviewTns);
            Controls.Add(numPort);
            Controls.Add(btnDisconnect);
            Controls.Add(cbSymbol);
            Controls.Add(btnConnect);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "IBKR Trader";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            contextFormRightClick.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).EndInit();
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
        private DataGridView datagridviewTns;
    }
}