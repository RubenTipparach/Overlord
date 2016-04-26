using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace OverlordVisualizer
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.Axis01Ordinal1 = new System.Windows.Forms.Button();
            this.Axis02Ordinal2 = new System.Windows.Forms.Button();
            this.Axis03Ordinal3 = new System.Windows.Forms.Button();
            this.Axis04Ordinal4 = new System.Windows.Forms.Button();
            this.Axis12Ordinal5 = new System.Windows.Forms.Button();
            this.Axis13Ordinal6 = new System.Windows.Forms.Button();
            this.Axis14Ordinal7 = new System.Windows.Forms.Button();
            this.Axis23Ordinal8 = new System.Windows.Forms.Button();
            this.Axis24Ordinal9 = new System.Windows.Forms.Button();
            this.Axis34Ordinal10 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "Default";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1120, 720);
            this.chart1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 738);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Axis01Ordinal1
            // 
            this.Axis01Ordinal1.Location = new System.Drawing.Point(1157, 25);
            this.Axis01Ordinal1.Name = "Axis01Ordinal1";
            this.Axis01Ordinal1.Size = new System.Drawing.Size(174, 23);
            this.Axis01Ordinal1.TabIndex = 2;
            this.Axis01Ordinal1.Text = "Axis 01 Ordinal 1";
            this.Axis01Ordinal1.UseVisualStyleBackColor = true;
            this.Axis01Ordinal1.Click += new System.EventHandler(this.button2_Click);
            // 
            // Axis02Ordinal2
            // 
            this.Axis02Ordinal2.Location = new System.Drawing.Point(1157, 54);
            this.Axis02Ordinal2.Name = "Axis02Ordinal2";
            this.Axis02Ordinal2.Size = new System.Drawing.Size(174, 23);
            this.Axis02Ordinal2.TabIndex = 3;
            this.Axis02Ordinal2.Text = "Axis 02 Ordinal 2";
            this.Axis02Ordinal2.UseVisualStyleBackColor = true;
            this.Axis02Ordinal2.Click += new System.EventHandler(this.Axis02Ordinal2_Click);
            // 
            // Axis03Ordinal3
            // 
            this.Axis03Ordinal3.Location = new System.Drawing.Point(1157, 83);
            this.Axis03Ordinal3.Name = "Axis03Ordinal3";
            this.Axis03Ordinal3.Size = new System.Drawing.Size(174, 23);
            this.Axis03Ordinal3.TabIndex = 4;
            this.Axis03Ordinal3.Text = "Axis 03 Ordinal 3";
            this.Axis03Ordinal3.UseVisualStyleBackColor = true;
            this.Axis03Ordinal3.Click += new System.EventHandler(this.Axis03Ordinal3_Click);
            // 
            // Axis04Ordinal4
            // 
            this.Axis04Ordinal4.Location = new System.Drawing.Point(1157, 112);
            this.Axis04Ordinal4.Name = "Axis04Ordinal4";
            this.Axis04Ordinal4.Size = new System.Drawing.Size(174, 23);
            this.Axis04Ordinal4.TabIndex = 5;
            this.Axis04Ordinal4.Text = "Axis 04 Ordinal 4";
            this.Axis04Ordinal4.UseVisualStyleBackColor = true;
            this.Axis04Ordinal4.Click += new System.EventHandler(this.Axis04Ordinal4_Click);
            // 
            // Axis12Ordinal5
            // 
            this.Axis12Ordinal5.Location = new System.Drawing.Point(1157, 141);
            this.Axis12Ordinal5.Name = "Axis12Ordinal5";
            this.Axis12Ordinal5.Size = new System.Drawing.Size(174, 23);
            this.Axis12Ordinal5.TabIndex = 6;
            this.Axis12Ordinal5.Text = "Axis 12 Ordinal 5";
            this.Axis12Ordinal5.UseVisualStyleBackColor = true;
            this.Axis12Ordinal5.Click += new System.EventHandler(this.Axis12Ordinal5_Click);
            // 
            // Axis13Ordinal6
            // 
            this.Axis13Ordinal6.Location = new System.Drawing.Point(1157, 170);
            this.Axis13Ordinal6.Name = "Axis13Ordinal6";
            this.Axis13Ordinal6.Size = new System.Drawing.Size(174, 23);
            this.Axis13Ordinal6.TabIndex = 7;
            this.Axis13Ordinal6.Text = "Axis 13 Ordinal 6";
            this.Axis13Ordinal6.UseVisualStyleBackColor = true;
            this.Axis13Ordinal6.Click += new System.EventHandler(this.Axis13Ordinal6_Click);
            // 
            // Axis14Ordinal7
            // 
            this.Axis14Ordinal7.Location = new System.Drawing.Point(1157, 199);
            this.Axis14Ordinal7.Name = "Axis14Ordinal7";
            this.Axis14Ordinal7.Size = new System.Drawing.Size(174, 23);
            this.Axis14Ordinal7.TabIndex = 8;
            this.Axis14Ordinal7.Text = "Axis 14 Ordinal 7";
            this.Axis14Ordinal7.UseVisualStyleBackColor = true;
            this.Axis14Ordinal7.Click += new System.EventHandler(this.Axis14Ordinal7_Click);
            // 
            // Axis23Ordinal8
            // 
            this.Axis23Ordinal8.Location = new System.Drawing.Point(1157, 228);
            this.Axis23Ordinal8.Name = "Axis23Ordinal8";
            this.Axis23Ordinal8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Axis23Ordinal8.Size = new System.Drawing.Size(174, 23);
            this.Axis23Ordinal8.TabIndex = 9;
            this.Axis23Ordinal8.Text = "Axis 23 Ordinal 8";
            this.Axis23Ordinal8.UseVisualStyleBackColor = true;
            this.Axis23Ordinal8.Click += new System.EventHandler(this.Axis15Ordinal8_Click);
            // 
            // Axis24Ordinal9
            // 
            this.Axis24Ordinal9.Location = new System.Drawing.Point(1157, 257);
            this.Axis24Ordinal9.Name = "Axis24Ordinal9";
            this.Axis24Ordinal9.Size = new System.Drawing.Size(174, 23);
            this.Axis24Ordinal9.TabIndex = 10;
            this.Axis24Ordinal9.Text = "Axis 24 Ordinal 9";
            this.Axis24Ordinal9.UseVisualStyleBackColor = true;
            this.Axis24Ordinal9.Click += new System.EventHandler(this.Axis16Ordinal9_Click);
            // 
            // Axis34Ordinal10
            // 
            this.Axis34Ordinal10.Location = new System.Drawing.Point(1157, 286);
            this.Axis34Ordinal10.Name = "Axis34Ordinal10";
            this.Axis34Ordinal10.Size = new System.Drawing.Size(174, 23);
            this.Axis34Ordinal10.TabIndex = 11;
            this.Axis34Ordinal10.Text = "Axis 34 Ordinal 10";
            this.Axis34Ordinal10.UseVisualStyleBackColor = true;
            this.Axis34Ordinal10.Click += new System.EventHandler(this.Axis17Ordinal10_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 773);
            this.Controls.Add(this.Axis34Ordinal10);
            this.Controls.Add(this.Axis24Ordinal9);
            this.Controls.Add(this.Axis23Ordinal8);
            this.Controls.Add(this.Axis14Ordinal7);
            this.Controls.Add(this.Axis13Ordinal6);
            this.Controls.Add(this.Axis12Ordinal5);
            this.Controls.Add(this.Axis04Ordinal4);
            this.Controls.Add(this.Axis03Ordinal3);
            this.Controls.Add(this.Axis02Ordinal2);
            this.Controls.Add(this.Axis01Ordinal1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }


      

        #endregion

        private Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Axis01Ordinal1;
        private System.Windows.Forms.Button Axis02Ordinal2;
        private System.Windows.Forms.Button Axis03Ordinal3;
        private System.Windows.Forms.Button Axis04Ordinal4;
        private System.Windows.Forms.Button Axis12Ordinal5;
        private System.Windows.Forms.Button Axis13Ordinal6;
        private System.Windows.Forms.Button Axis14Ordinal7;
        private System.Windows.Forms.Button Axis23Ordinal8;
        private System.Windows.Forms.Button Axis24Ordinal9;
        private System.Windows.Forms.Button Axis34Ordinal10;
    }
}

