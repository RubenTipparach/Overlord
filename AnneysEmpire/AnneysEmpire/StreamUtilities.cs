using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
    public static class StreamUtilities
    {
		private static string ConnectionString = "Server=localhost; Port=3306; Database=aoenn; Uid=ruben; Pwd=ruben;";

		/// <summary>
		/// Only supports singular array at the moment, dumping complex data table is on my todo list.
		/// </summary>
		/// <param name="filePathName"></param>
		/// <param name="dataArray"></param>
		public static void DumpData(String filePathName, double[] dataArray)
        {
            var outDefault = Console.Out;

            using (StreamWriter writer = new StreamWriter(filePathName))
            {
                Console.SetOut(writer);
                foreach(var d in dataArray)
                {
                    Console.WriteLine(d.ToString());
                }
            }

            Console.SetOut(outDefault);
        }

		/// <summary>
		/// Only supports singular array at the moment, dumping complex data table is on my todo list.
		/// </summary>
		/// <param name="filePathName"></param>
		/// <param name="dataArray"></param>
		public static void GenerateScript(string filePathName, string input, string topPart, string bottomPart)
		{
			var outDefault = Console.Out;

			using (StreamWriter writer = new StreamWriter(filePathName))
			{
				Console.SetOut(writer);
				using (StreamReader readerTop = new StreamReader(topPart))
				using (StreamReader readerBottom = new StreamReader(bottomPart))
				{
					string line = "";
					// Stream top line
					while ((line = readerTop.ReadLine()) != null)
					{
						Console.WriteLine(line);
					}

					Console.WriteLine(input);

					// Stream bottom lines
					while ((line = readerBottom.ReadLine()) != null)
					{
						Console.WriteLine(line);
					}
				}
			}

			Console.SetOut(outDefault);
		}

		/// <summary>
		/// Connects to database. Testing method to see if this actually works.
		/// </summary>
		/// <returns>Database connection result.</returns>
		public static void ConnectToDatabase()
		{
			String s = "";
			//string connectionString = "server=localhost:3306;userid=ruben;password=ruben;database=aoenn";
	
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			try
			{
				//MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

				string cmdString = "SELECT * FROM ai_definition";

				MySqlCommand cmd = new MySqlCommand(cmdString, conn);
				
				cmd.Prepare();
				MySqlDataReader msdr = cmd.ExecuteReader();
				while(msdr.Read())
				{
					Console.Write(Convert.ToString("AIIndex: " + msdr["AIIndex"]) + " ");
					Console.Write(Convert.ToString("Name: " + msdr["Name"]) + " ");
					Console.Write(Convert.ToString("Author: " + msdr["Author"]) + " ");
					Console.WriteLine();
				}
			}
			catch(MySqlException mse)
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

			//return s;
		}

		/// <summary>
		/// Gets the AI data set.
		/// </summary>
		/// <returns>Good shit.</returns>
		public static List<CoastalRaidersFuedalResourceManager> GetAiDataSet()
		{

			MySqlConnection conn = new MySqlConnection(ConnectionString);
			var dataRows = new List<CoastalRaidersFuedalResourceManager>();

			try
			{
				//MySqlConnection conn = new MySqlConnection(connectionString);
				conn.Open();

				string cmdString = @"
					SELECT  AD1.Name as player1, AD2.Name as player2,  ANNF.* 
					FROM aoenn.ai_neural_network_feed ANNF
						inner join ai_definition AD1 on ANNF.p1Index = AD1.AIIndex
						inner join ai_definition AD2 on ANNF.p2Index = AD2.AIIndex; ";

				MySqlCommand cmd = new MySqlCommand(cmdString, conn);

				cmd.Prepare();
				MySqlDataReader msdr = cmd.ExecuteReader();
				// Gets all that good data.
				while (msdr.Read())
				{
					 var player1 = new CoastalRaidersFuedalResourceManager(
									Convert.ToString(msdr["player1"]),
									Convert.ToInt32(msdr["GameId"]),

									Convert.ToDouble(msdr["p1Wood"]),
									Convert.ToDouble(msdr["p1Food"]),
									Convert.ToDouble(msdr["p1Stone"]),
									Convert.ToDouble(msdr["p1Gold"]),
									Convert.ToDouble(msdr["p1Builders"]),

									Convert.ToInt32(msdr["p1WoodHighest"]),
									Convert.ToInt32(msdr["p1FoodHighest"]),
									Convert.ToInt32(msdr["p1GoldHighest"]),
									Convert.ToInt32(msdr["p1StoneHighest"]));

					var player2 = new CoastalRaidersFuedalResourceManager(
									Convert.ToString(msdr["player2"]),
									Convert.ToInt32(msdr["GameId"]),

									Convert.ToDouble(msdr["p2Wood"]),
									Convert.ToDouble(msdr["p2Food"]),
									Convert.ToDouble(msdr["p2Stone"]),
									Convert.ToDouble(msdr["p2Gold"]),
									Convert.ToDouble(msdr["p2Builders"]),

									Convert.ToInt32(msdr["p2WoodHighest"]),
									Convert.ToInt32(msdr["p2FoodHighest"]),
									Convert.ToInt32(msdr["p2GoldHighest"]),
									Convert.ToInt32(msdr["p2StoneHighest"]));

					dataRows.Add(player1);
					dataRows.Add(player2);
				}
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

			return dataRows;
        }

	

		/// <summary>
		/// Determines whether [is file locked] [the specified file].
		/// Use this for checking if the recording file is in use. If it is, the program should not attempt to call it via the php script.
		/// This will help automate the process to parse replay files.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>A boolean flag, true if file is availible for training the AI. Cool stuff.</returns>
		public static bool IsFileLocked(FileInfo file)
		{
			FileStream stream = null;

			try
			{
				stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
			}
			catch (IOException)
			{
				//the file is unavailable because it is:
				//still being written to
				//or being processed by another thread
				//or does not exist (has already been processed)
				return true;
			}
			finally
			{
				if (stream != null)
					stream.Close();
			}

			//file is not locked
			return false;
		}
	}
}
