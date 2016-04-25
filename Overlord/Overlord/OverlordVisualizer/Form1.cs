using MySql.Data.MySqlClient;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GenerateChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateNewChart();
        }

        //generate chart
        void GenerateChart()
        {
            // Set 3D chart settings
            chart1.ChartAreas["Default"].Area3DStyle.Enable3D = true;
            chart1.ChartAreas["Default"].Area3DStyle.IsRightAngleAxes = false;
            
            chart1.ChartAreas["Default"].Area3DStyle.Inclination = 40;
            chart1.ChartAreas["Default"].Area3DStyle.Rotation = 20;
            chart1.ChartAreas["Default"].Area3DStyle.LightStyle = LightStyle.Realistic;
            chart1.ChartAreas["Default"].Area3DStyle.PointDepth = 1000;

            // Populate series with random data
            Random random = new Random();
            for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            {
                chart1.Series["Series1"].Points.AddY(random.Next(45, 95));
                chart1.Series["Series2"].Points.AddY(random.Next(5, 75));
            }

            // Set series chart type
            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            chart1.Series["Series2"].ChartType = SeriesChartType.Line;

            // Set point labels
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series2"].IsValueShownAsLabel = true;

            // Enable X axis margin
           //chart1.ChartAreas["Default"].AxisX.IsMarginVisible = true;

            // Enable the ShowMarkerLines
            chart1.Series["Series1"]["ShowMarkerLines"] = "true";
            chart1.Series["Series2"]["ShowMarkerLines"] = "true";
        }

        /// <summary>
        /// Generates the new chart.
        /// </summary>
        void GenerateNewChart()
        {

        }

        /// <summary>
        /// This method allows me to make more database connections.
        /// Maybe I should keep one open in another method?
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cmdString"></param>
        private static void ReadSql(Action<MySqlDataReader> buildDataSet, string cmdString)
        {
            MySqlConnection conn = new MySqlConnection(Configurations.ConnectionString);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdString, conn);
                cmd.Prepare();
                MySqlDataReader msdr = cmd.ExecuteReader();
                // Gets all that good data.
                buildDataSet(msdr);
            }
            catch (MySqlException mse)
            {
                throw mse;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
