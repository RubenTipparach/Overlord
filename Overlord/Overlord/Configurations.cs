using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord
{
	/// <summary>
	/// This stores a mapping to the App.config file.
	/// </summary>
	public static class Configurations
	{
        /// <summary>
        /// The connection string.
        /// </summary>
        public static readonly string ConnectionString = ConfigurationManager.AppSettings["MySqlConnString"];

        /// <summary>
        /// The target aoe2 path.
        /// </summary>
        public static readonly string TargetAoe2Path = ConfigurationManager.AppSettings["TargetAoe2Path"];

        /// <summary>
        /// The target aoe2 script.
        /// </summary>
        public static readonly string TargetAoe2Script = ConfigurationManager.AppSettings["TargetAoe2Path"];

        /// <summary>
        /// Parses the game mode.
        /// </summary>
        /// <returns></returns>
        public static GameGenMode GameGenerationMode()
        {
            string genMode = ConfigurationManager.AppSettings["GameGenerationMode"];

            if(genMode == "Random")
            {
                return GameGenMode.Random;
            }
            else if( genMode == "HillClimbing")
            {
                return GameGenMode.HillClimbing;
            }
            else
            {
                return GameGenMode.HillClimbing2;
            }
        }

		/// <summary>
		/// The use custom p1 input flag.
		/// </summary>
		public static readonly bool UseCustomP1Input = Convert.ToBoolean(ConfigurationManager.AppSettings["UseCustomEnemy"]);

        /// <summary>
        /// Retrieves the player1 input as specified in the app.config file.
        /// </summary>
        /// <returns></returns>
        public static double[] RetrievePlayer1Input()
        {
            if(UseCustomP1Input)
            {
                return new double[]
                {
                    Convert.ToDouble(ConfigurationManager.AppSettings["p1Wood"]),
                    Convert.ToDouble(ConfigurationManager.AppSettings["p1Food"]),
                    Convert.ToDouble(ConfigurationManager.AppSettings["p1Gold"]),
                    Convert.ToDouble(ConfigurationManager.AppSettings["p1Stone"]),
                    Convert.ToDouble(ConfigurationManager.AppSettings["p1Builders"])
                };
            }
            else
            {
                return new double[]
                {
                    .47,
                    .41,
                    .12,
                    .05,
                    .1
                };
            }
        }

		/// <summary>
		/// The automatic generate ai flag. If true, then this will write to the specified AI folder.
		/// </summary>
		public static readonly bool AutoGenerateAi = Convert.ToBoolean(ConfigurationManager.AppSettings["AutoGenerateNewAI"]);
    }

	/// <summary>
	/// Game mode options.
	/// </summary>
	public enum GameGenMode
    {
        Random,
        HillClimbing,
        HillClimbing2
    }
}
