namespace IBKR_Trader
{
    partial class TimeAndSales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            datagridviewTns = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).BeginInit();
            SuspendLayout();
            // 
            // datagridviewTns
            // 
            datagridviewTns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridviewTns.Dock = DockStyle.Fill;
            datagridviewTns.Location = new Point(0, 0);
            datagridviewTns.Name = "datagridviewTns";
            datagridviewTns.RowTemplate.Height = 25;
            datagridviewTns.Size = new Size(332, 643);
            datagridviewTns.TabIndex = 0;
            // 
            // TimeAndSales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 643);
            Controls.Add(datagridviewTns);
            Name = "TimeAndSales";
            Text = "TimeAndSales";
            Load += TimeAndSales_Load;
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView datagridviewTns;
    }
}