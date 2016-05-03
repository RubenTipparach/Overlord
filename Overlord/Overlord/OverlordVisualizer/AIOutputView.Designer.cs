namespace OverlordVisualizer
{
    partial class AIOutputView
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
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.TurnRight = new System.Windows.Forms.Button();
			this.TurnLeft = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// chart1
			// 
			chartArea1.Name = "Default";
			this.chart1.ChartAreas.Add(chartArea1);
			this.chart1.Location = new System.Drawing.Point(12, 12);
			this.chart1.Name = "chart1";
			this.chart1.Size = new System.Drawing.Size(1171, 512);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			// 
			// TurnRight
			// 
			this.TurnRight.Location = new System.Drawing.Point(116, 540);
			this.TurnRight.Name = "TurnRight";
			this.TurnRight.Size = new System.Drawing.Size(53, 23);
			this.TurnRight.TabIndex = 23;
			this.TurnRight.Text = "->";
			this.TurnRight.UseVisualStyleBackColor = true;
			this.TurnRight.Click += new System.EventHandler(this.TurnRight_Click);
			// 
			// TurnLeft
			// 
			this.TurnLeft.Location = new System.Drawing.Point(61, 540);
			this.TurnLeft.Name = "TurnLeft";
			this.TurnLeft.Size = new System.Drawing.Size(48, 23);
			this.TurnLeft.TabIndex = 22;
			this.TurnLeft.Text = "<-";
			this.TurnLeft.UseVisualStyleBackColor = true;
			this.TurnLeft.Click += new System.EventHandler(this.TurnLeft_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 546);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 21;
			this.label2.Text = "Rotate";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// AIOutputView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1223, 575);
			this.Controls.Add(this.TurnRight);
			this.Controls.Add(this.TurnLeft);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chart1);
			this.Name = "AIOutputView";
			this.Text = "AIOutputView";
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.Button TurnRight;
		private System.Windows.Forms.Button TurnLeft;
		private System.Windows.Forms.Label label2;
	}

}