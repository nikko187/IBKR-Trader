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
            btnDisconnect = new Button();
            listViewTns = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            numPort = new NumericUpDown();
            tooltipPort = new ToolTip(components);
            tooltipClosePortion = new ToolTip(components);
            contextFormRightClick = new ContextMenuStrip(components);
            toolstripBorderToggle = new ToolStripMenuItem();
            cbSymbol = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            contextFormRightClick.SuspendLayout();
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
            // listViewTns
            // 
            listViewTns.BackColor = Color.Black;
            listViewTns.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listViewTns.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            listViewTns.ForeColor = Color.White;
            listViewTns.HeaderStyle = ColumnHeaderStyle.None;
            listViewTns.Location = new Point(5, 35);
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
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(238, 435);
            ContextMenuStrip = contextFormRightClick;
            Controls.Add(listViewTns);
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
            ResumeLayout(false);
        }

        #endregion

        private Button btnConnect;
        private Button btnDisconnect;
        private ListView listViewTns;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private NumericUpDown numPort;
        private ListBox lbHelp;
        private ToolTip tooltipPort;
        private ToolTip tooltipClosePortion;
        private Label labelChange;
        private ContextMenuStrip contextFormRightClick;
        private ToolStripMenuItem toolstripBorderToggle;
        private ComboBox cbSymbol;
    }
}