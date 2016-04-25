using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlord.Models;
using System.Configuration;
using System.Data;

namespace Overlord
{
    public static class StreamUtilities
    {

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
	
			MySqlConnection conn = new MySqlConnection(Configurations.ConnectionString);
			try
			{
				//MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

				string cmdString = "SELECT AIIndex, `Name`, Author FROM ai_definition;";

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
			var dataRows = new List<CoastalRaidersFuedalResourceManager>();

			string cmdString = @"
				SELECT  AD1.Name as player1, AD2.Name as player2,  ANNF.* 
				FROM aoenn.ai_neural_network_feed ANNF
					INNER JOIN ai_definition AD1 ON ANNF.p1Index = AD1.AIIndex
					INNER JOIN ai_definition AD2 ON ANNF.p2Index = AD2.AIIndex;";

			// Delegates allow me to take laziness to a whole new level.
			ReadSql((MySqlDataReader msdr) =>
			{
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
			}, cmdString);

			return dataRows;
        }

		/// <summary>
		/// Retrieves the latest game information only if it is an unprocessed
		/// record.
		/// </summary>
		/// <returns></returns>
		public static GameData GetLatestGame()
		{
			string sqlCmd = @"
				SELECT GameId, IsReady, DateGamePlayed_CDT
				FROM ai_game_table
				WHERE IsReady = 0
				ORDER BY GameId DESC LIMIT 1;";

			GameData defaultDat = null;

			ReadSql((MySqlDataReader msdr) =>
				{
					if(msdr.Read())
					{
						defaultDat = new GameData(
							Convert.ToInt32(msdr["GameId"]),
							Convert.ToBoolean(msdr["IsReady"]),
							Convert.ToDateTime(msdr["DateGamePlayed_CDT"]));
					}
				}, sqlCmd);

			return defaultDat;
		}

		/// <summary>
		/// Updates the game. This method tells us that the game's process id has been consumed
		/// and that the AI is adding this information to its knowledgebase.
		/// </summary>
		/// <param name="gameUpdated">The game updated.</param>
		public static void UpdateGame(GameData gameUpdated)
		{
			string sqlCmd = @"
				UPDATE ai_game_table 
				SET IsReady = 1
				WHERE GameId = @CurrentGame;";

			ExecuteSql((MySqlCommand cmd) =>
			{
				var msqlPc = cmd.Parameters;
                msqlPc.Add(new MySqlParameter("@CurrentGame", gameUpdated.GameId));
            }, sqlCmd);
		}

        public static void SubmitPlotableData(List<VectorN> dataPoints)
        {

            int maxDataId = 0;
            string readCmd = "SELECT MAX(DataId) FROM aoenn.ai_plotable_data;";
            ReadSql((MySqlDataReader msdr) =>
            {
                if (msdr.Read())
                {
                    maxDataId = Convert.ToInt32(msdr["MaxRow"]);
                }
            }, readCmd);

            StringBuilder sqlCmd = new StringBuilder(@"
				INSERT INTO ai_plotable_data (DataId, X, Y, Z) VALUE ");

            List<string> rows = new List<string>(dataPoints.Count);

            for (int i = 0; i < dataPoints.Count; i++)
            {
                rows.Add(string.Format("( {0}, {1}, {2} ,{3} )", maxDataId, dataPoints[i][0], dataPoints[i][1], dataPoints[i][2]));
            }

            sqlCmd.Append(string.Join(",", rows));
            sqlCmd.Append(";");

            ExecuteSql((MySqlCommand cmd) => {}, sqlCmd.ToString());
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

		/// <summary>
		/// Use this method to apply changes to the database or trigger stored procedure events.
		/// </summary>
		/// <param name="buildSql"></param>
		/// <param name="cmdString"></param>
		private static void ExecuteSql(Action<MySqlCommand> buildSql, string cmdString)
		{
			MySqlConnection conn = new MySqlConnection(Configurations.ConnectionString);

			try
			{
				conn.Open();
				MySqlCommand cmd = new MySqlCommand(cmdString, conn);
				buildSql(cmd);

				cmd.Prepare();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
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
