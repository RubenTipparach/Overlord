using MySql.Data.MySqlClient;
using Overlord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OverlordVisualizer
{
    public partial class AIOutputView : Form
    {
        private int _dataId;
        private int _axisX;
        private int _axisY;
        private int _ordinalId;

        private int _zNumber;

        public AIOutputView(int dataId, int axisX, int axisY, int ordinalId, int zNumber)
        {
            _dataId = dataId;
            _axisX = axisX;
            _axisY = axisY;
            _ordinalId = ordinalId;
            _zNumber = zNumber;

            InitializeComponent();
            DrawChart();
        }

        public void DrawChart()
        {
            // Read fresh data from database.
            string readPlotableSql = string.Format(@"
                SELECT X, Y, Z1, Z2, Z3, Z4 FROM ai_plotable_unnormalized_data 
                WHERE DataId = {0} AND OrdinalId = {1};"
                , _dataId, _ordinalId);

            List<VectorN> vectors = new List<VectorN>(10000); // initialized to 10,000 units. i.e. 100X100

            Form1.ReadSql((MySqlDataReader msdr, MySqlCommand cmd) =>
            {
                while (msdr.Read())
                {
                    VectorN tempVector = new VectorN(
                        new double[] {
                            Convert.ToDouble(msdr["X"]),
                            Convert.ToDouble(msdr["Z" + _zNumber]),
                            Convert.ToDouble(msdr["Y"])
                    });

                    vectors.Add(tempVector);
                }
            }, readPlotableSql);

            // Take a list of vectors, and plot them!


            ChartArea chartArea1 = new ChartArea();
            List<Series> seriesSet = new List<Series>(100);
            //this.chart1 = new Chart();

            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            chart1.ChartAreas["Default"].Area3DStyle.Enable3D = true;
            chart1.ChartAreas["Default"].Area3DStyle.IsRightAngleAxes = false;

            chart1.ChartAreas["Default"].Area3DStyle.Inclination = 40;
            chart1.ChartAreas["Default"].Area3DStyle.Rotation = 20;
            chart1.ChartAreas["Default"].Area3DStyle.WallWidth = 10;
            chart1.ChartAreas["Default"].Area3DStyle.LightStyle = LightStyle.Realistic;
            chart1.ChartAreas["Default"].Area3DStyle.PointDepth = 100;
            //chartArea1.Name = "Default";
            //this.chart1.ChartAreas.Add(chartArea1);
            //this.chart1.Location = new System.Drawing.Point(12, 12);
            //this.chart1.Name = "chart1";

            int size = 100;
            double min = 100000;
            double max = 0;
            // adds 100 series!
            for (int i = 0; i < size; i++)
            {
                seriesSet.Add(new Series());
                seriesSet[i].ChartArea = "Default";
                seriesSet[i].Name = "" + i;

                for (int j = 0; j < size; j++)
                {
                    if (vectors.Count > j + i * size)
                    {
                        double normalized = vectors[j + i * size][1];
                        seriesSet[i].Points.AddXY(j, normalized);

                        if (normalized > max)
                        {
                            max = normalized;
                        }

                        if (normalized < min)
                        {
                            min = normalized;
                        }
                    }
                }

                for (int j = 0; j < size; j++)
                {
                    if (vectors.Count > j + i * size)
                    {
                        double normalized = seriesSet[i].Points[j].YValues[0];
                        double diff = max - min;
                        // total is 255,  we need to find scaling factor.
                        double redRange = ((normalized - min) / diff) * 200; //red range!
                        double greenRange = ((normalized - min) / diff) * 100; //red range!
                        seriesSet[i].Points[j].Color = Color.FromArgb((int)redRange, 0, 0);
                    }
                }

                seriesSet[i].Legend = "Legend1";
                seriesSet[i].ChartType = SeriesChartType.Line;
                //seriesSet[i].Color = Color.FromArgb(100-i, i, 0);
                // Set point labels
                //seriesSet[i].IsValueShownAsLabel = true;

                this.chart1.Series.Add(seriesSet[i]);
            }


            // this.chart1.Size = new System.Drawing.Size(1319, 720);
            this.chart1.TabIndex = 0;
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.Refresh();
        }

		private void TurnLeft_Click(object sender, EventArgs e)
		{
			if (chart1.ChartAreas["Default"].Area3DStyle.Rotation - 2 > -180)
			{
				chart1.ChartAreas["Default"].Area3DStyle.Rotation = chart1.ChartAreas["Default"].Area3DStyle.Rotation - 2;
			}
			else
			{
				chart1.ChartAreas["Default"].Area3DStyle.Rotation = 180;
			}
		}

		private void TurnRight_Click(object sender, EventArgs e)
		{
			if (chart1.ChartAreas["Default"].Area3DStyle.Rotation + 2 < 180)
			{
				chart1.ChartAreas["Default"].Area3DStyle.Rotation = chart1.ChartAreas["Default"].Area3DStyle.Rotation + 2;
			}
			else
			{
				chart1.ChartAreas["Default"].Area3DStyle.Rotation = -180;
			}
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}
	}
}
