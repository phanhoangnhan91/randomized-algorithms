﻿namespace ClosestPair
{
    partial class FindClosestPair
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lbresult = new System.Windows.Forms.Label();
            this.btRA = new System.Windows.Forms.Button();
            this.btDC = new System.Windows.Forms.Button();
            this.btInput = new System.Windows.Forms.Button();
            this.btBF = new System.Windows.Forms.Button();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listViewDuLieu = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btDel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(741, 435);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lbresult);
            this.tabPage1.Controls.Add(this.btRA);
            this.tabPage1.Controls.Add(this.btDC);
            this.tabPage1.Controls.Add(this.btInput);
            this.tabPage1.Controls.Add(this.btBF);
            this.tabPage1.Controls.Add(this.tbInput);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(733, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Algorithm";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "*Points in array are seperated by a tab.";
            // 
            // lbresult
            // 
            this.lbresult.AutoSize = true;
            this.lbresult.Location = new System.Drawing.Point(14, 315);
            this.lbresult.Name = "lbresult";
            this.lbresult.Size = new System.Drawing.Size(52, 17);
            this.lbresult.TabIndex = 3;
            this.lbresult.Text = "output:";
            // 
            // btRA
            // 
            this.btRA.Location = new System.Drawing.Point(344, 268);
            this.btRA.Name = "btRA";
            this.btRA.Size = new System.Drawing.Size(159, 36);
            this.btRA.TabIndex = 50;
            this.btRA.Text = "Randomized Algorithm";
            this.btRA.UseVisualStyleBackColor = true;
            this.btRA.Click += new System.EventHandler(this.btRA_Click);
            // 
            // btDC
            // 
            this.btDC.Location = new System.Drawing.Point(178, 268);
            this.btDC.Name = "btDC";
            this.btDC.Size = new System.Drawing.Size(160, 36);
            this.btDC.TabIndex = 40;
            this.btDC.Text = "Divide and Conquer";
            this.btDC.UseVisualStyleBackColor = true;
            this.btDC.Click += new System.EventHandler(this.btDC_Click);
            // 
            // btInput
            // 
            this.btInput.Location = new System.Drawing.Point(66, 10);
            this.btInput.Name = "btInput";
            this.btInput.Size = new System.Drawing.Size(159, 27);
            this.btInput.TabIndex = 10;
            this.btInput.Text = "Randomly Generate";
            this.btInput.UseVisualStyleBackColor = true;
            this.btInput.Click += new System.EventHandler(this.btInput_Click);
            // 
            // btBF
            // 
            this.btBF.Location = new System.Drawing.Point(12, 267);
            this.btBF.Name = "btBF";
            this.btBF.Size = new System.Drawing.Size(160, 36);
            this.btBF.TabIndex = 30;
            this.btBF.Text = "Brute Force";
            this.btBF.UseVisualStyleBackColor = true;
            this.btBF.Click += new System.EventHandler(this.btBF_Click);
            // 
            // tbInput
            // 
            this.tbInput.AcceptsTab = true;
            this.tbInput.Location = new System.Drawing.Point(66, 43);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInput.Size = new System.Drawing.Size(437, 192);
            this.tbInput.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Points:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.listViewDuLieu);
            this.tabPage2.Controls.Add(this.btDel);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(733, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Running time chart";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Location = new System.Drawing.Point(341, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 392);
            this.panel1.TabIndex = 23;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "sBF";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "sDC";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "sRa";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(378, 386);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // listViewDuLieu
            // 
            this.listViewDuLieu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.listViewDuLieu.Location = new System.Drawing.Point(8, 6);
            this.listViewDuLieu.Name = "listViewDuLieu";
            this.listViewDuLieu.Size = new System.Drawing.Size(327, 356);
            this.listViewDuLieu.TabIndex = 22;
            this.listViewDuLieu.UseCompatibleStateImageBehavior = false;
            this.listViewDuLieu.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Algorithm";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Size";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Running time (μs)";
            this.columnHeader2.Width = 120;
            // 
            // btDel
            // 
            this.btDel.Location = new System.Drawing.Point(126, 368);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(78, 30);
            this.btDel.TabIndex = 100;
            this.btDel.Text = "Clear all";
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // FindClosestPair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 435);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FindClosestPair";
            this.Text = "Closest pair of points problem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindClosestPair_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lbresult;
        private System.Windows.Forms.Button btRA;
        private System.Windows.Forms.Button btDC;
        private System.Windows.Forms.Button btInput;
        private System.Windows.Forms.Button btBF;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewDuLieu;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label2;
    }
}