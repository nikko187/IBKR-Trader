﻿namespace IBKR_Trader
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
            datagridviewTns = new DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).BeginInit();
            SuspendLayout();
            // 
            // datagridviewTns
            // 
            datagridviewTns.AllowUserToAddRows = false;
            datagridviewTns.AllowUserToDeleteRows = false;
            datagridviewTns.AllowUserToOrderColumns = true;
            datagridviewTns.AllowUserToResizeRows = false;
            datagridviewTns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridviewTns.BackgroundColor = Color.Black;
            datagridviewTns.BorderStyle = BorderStyle.None;
            datagridviewTns.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            datagridviewTns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            datagridviewTns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Black;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            datagridviewTns.DefaultCellStyle = dataGridViewCellStyle2;
            datagridviewTns.Dock = DockStyle.Fill;
            datagridviewTns.EnableHeadersVisualStyles = false;
            datagridviewTns.Location = new Point(0, 0);
            datagridviewTns.Name = "datagridviewTns";
            datagridviewTns.ReadOnly = true;
            datagridviewTns.RowHeadersVisible = false;
            datagridviewTns.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            datagridviewTns.RowTemplate.Height = 20;
            datagridviewTns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewTns.Size = new Size(193, 485);
            datagridviewTns.TabIndex = 0;
            datagridviewTns.RowPrePaint += dataGridView1_RowPrePaint;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 200;
            timer1.Tick += timer1_Tick;
            // 
            // tnsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(193, 485);
            Controls.Add(datagridviewTns);
            DoubleBuffered = true;
            Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Gainsboro;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "tnsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Time & Sales";
            Load += tnsForm_Load;
            ((System.ComponentModel.ISupportInitialize)datagridviewTns).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DataGridView datagridviewTns;
        private System.Windows.Forms.Timer timer1;
    }
}