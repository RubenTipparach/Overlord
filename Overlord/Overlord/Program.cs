using Overlord.Learning;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Overlord
{
	/// <summary>
	/// This program is designed to take in input from spreadsheet or
	/// database system, and generate AI code for Age of Empires 2: Age of Kings.
	/// It uses an Artificial Nueral Network to take in game scores and wieghs each
	/// category against existing code blocks in order to decide which values
	/// allow the AI to manage their base in a more efficient way such that they 
	/// can defeat either another AI or another player.
	/// 
	/// NOTE: As proof of concept, we may need to generate more deterministic models for this,
	/// like simulating a game, and then running it to get better test results.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The logger. This logging utility looks a lot like the ones I wrote back when I had a job.
		/// I'll feel right at home with this little guy :)
		/// </summary>
		/// <remarks>
		/// https://github.com/nlog/nlog/wiki/Tutorial
		/// </remarks>
		public static Logger Logger
		{
			get
			{
				return _logger;
			}
		}

		/// <summary>
		/// The logger.
		/// </summary>
		private static Logger _logger = LogManager.GetCurrentClassLogger();
		
		/// <summary>
		/// The main execution method.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			// Setting some console colors.
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Clear();

			_logger.Info("Started executable.");

            string datasource = Convert.ToString(ConfigurationManager.AppSettings["Datasource"]);

			// Adding some data from the MySql database.
            if (datasource == "Database")
            {
                _logger.Info("Using learning engine.");
                AILearningEngine engine = new AILearningEngine(Configurations.TargetAoe2Path, Configurations.TargetAoe2Script);
                engine.Run();
            }
            else if (datasource == "Local")
            {
				// Run using local ai parser (this method is now deprecated).
				_logger.Info("Using local debugger.");
            }
            else
            {
                _logger.Error("Unknow datasource, terminating program!");
            }

            _logger.Info("Program ended.");
		}

		/// <summary>
		/// Old method used to test ANN stuff.
		/// </summary>
		static void Dump()
		{
			//To do write unit test for my Matrix/Vector library, maybe not since I went with a framework instead.

			//TwoLayer.NeuralNetowrk();
			//TwoLayerInternal.NeuralNetowrkWithAnnMath();
			//ThreeLayerNueralNet.RunNet();
			TextWriter tmp = Console.Out;

			//using (StreamWriter writer = new StreamWriter("out.txt"))
			//{
			//    Console.SetOut(writer);
			//    var ag = new TestingNdn(writer);
			//}

			// Default constructor for whatever stuff.
			//var ag = new TestNdn2(5, 10, 1);

			//AiTrainingModule.Test("C:\\annaoe2\\AnneysEmpire\\AnneysEmpire\\Data\\CR_Manual_data2.csv", 10, 10, 8);

			StreamUtilities.ConnectToDatabase();

			var gameData = StreamUtilities.GetAiDataSet();

			foreach (var gd in gameData)
			{
				Console.WriteLine(gd.ToString());

				// Now we will generate each AI its own file.
				if (gd.AiName == "G4-Coastal-Raiders_Learnt")
				{
					gd.ClonePrefix = "_" + gd.GameNumber.ToString();
					gd.GenerateNewAiFile("Data");
				}
			}

			Console.SetOut(tmp);
			Console.ReadKey();
		}
	}
}
