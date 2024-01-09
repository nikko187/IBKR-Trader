namespace IBKR_Trader
{
    partial class tnsForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tnsForm));
            datagridviewTns = new DataGridView();
            contextTnsRightClick = new ContextMenuStrip(components);
            toolStripSizeFilter = new ToolStripMenuItem();
            toolStripSizeValue = new ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).BeginInit();
            contextTnsRightClick.SuspendLayout();
            SuspendLayout();
            // 
            // datagridviewTns
            // 
            datagridviewTns.AllowUserToDeleteRows = false;
            datagridviewTns.AllowUserToResizeRows = false;
            datagridviewTns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridviewTns.BackgroundColor = Color.Black;
            datagridviewTns.BorderStyle = BorderStyle.None;
            datagridviewTns.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.LightGray;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            datagridviewTns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            datagridviewTns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridviewTns.ContextMenuStrip = contextTnsRightClick;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Black;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            datagridviewTns.DefaultCellStyle = dataGridViewCellStyle2;
            datagridviewTns.Dock = DockStyle.Fill;
            datagridviewTns.EnableHeadersVisualStyles = false;
            datagridviewTns.Location = new Point(0, 0);
            datagridviewTns.Name = "datagridviewTns";
            datagridviewTns.RowHeadersVisible = false;
            datagridviewTns.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            datagridviewTns.RowTemplate.Height = 19;
            datagridviewTns.Size = new Size(208, 481);
            datagridviewTns.TabIndex = 0;
            datagridviewTns.RowPrePaint += datagridviewTns_RowPrePaint;
            // 
            // contextTnsRightClick
            // 
            contextTnsRightClick.Items.AddRange(new ToolStripItem[] { toolStripSizeFilter, toolStripSizeValue });
            contextTnsRightClick.Name = "contextMenuStrip1";
            contextTnsRightClick.Size = new Size(161, 51);
            // 
            // toolStripSizeFilter
            // 
            toolStripSizeFilter.CheckOnClick = true;
            toolStripSizeFilter.Name = "toolStripSizeFilter";
            toolStripSizeFilter.Size = new Size(160, 22);
            toolStripSizeFilter.Text = "Size Filter";
            // 
            // toolStripSizeValue
            // 
            toolStripSizeValue.Name = "toolStripSizeValue";
            toolStripSizeValue.Size = new Size(100, 23);
            toolStripSizeValue.Text = "100";
            toolStripSizeValue.ToolTipText = "Size Filter";
            // 
            // tnsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(208, 481);
            Controls.Add(datagridviewTns);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "tnsForm";
            Text = "T&S";
            FormClosing += tnsForm_FormClosing;
            Load += tnsForm_Load;
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).EndInit();
            contextTnsRightClick.ResumeLayout(false);
            contextTnsRightClick.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView datagridviewTns;
        private ContextMenuStrip contextTnsRightClick;
        private ToolStripMenuItem toolStripSizeFilter;
        private ToolStripTextBox toolStripSizeValue;
    }
}