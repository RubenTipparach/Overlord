using NeuronDotNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord
{
    public class CoastalRaidersFuedalResourceManager
    {
		/// <summary>
		/// The ai mutable code.
		/// </summary>
		private static readonly string AI_Mutable_Code = @"
(defrule
	(current-age == feudal-age)
=>
	(set-strategic-number sn-food-gatherer-percentage {0})
	(set-strategic-number sn-wood-gatherer-percentage {1})
	(set-strategic-number sn-gold-gatherer-percentage {2})
	(set-strategic-number sn-stone-gatherer-percentage {3})
	(set-strategic-number sn-percent-civilian-builders {4})
	(disable-self)
)
";

		// Random ai atrributes.
		private string _aiName;
		private int _gameNumber;
		private string _clonePrefix = "";

        //input
        private double _sn_food_gatherer_percentage_fa;
        private double _sn_wood_gatherer_percentage_fa;
        private double _sn_gold_gatherer_percentage_fa;
        private double _sn_stone_gatherer_percentage_fa;
        private double _sn_percent_civilian_builders_fa;

        //output
        private int _food_Score;
        private int _wood_Score;
        private int _stone_Score;
        private int _gold_Score;

		/// <summary>
		/// Sets the enumerated clone.
		/// </summary>
		/// <value>
		/// The enumerated clone.
		/// </value>
		public string ClonePrefix
		{
			set
			{
				_clonePrefix = value;
			}
		}

        /// <summary>
        /// Returns an array of input
        /// </summary>
        public double[] GetInputParams
        {
            get
            {
                return
                     new double[]
                     {
                        _sn_food_gatherer_percentage_fa,
                        _sn_wood_gatherer_percentage_fa,
                        _sn_stone_gatherer_percentage_fa,
                        _sn_gold_gatherer_percentage_fa,
                        _sn_percent_civilian_builders_fa
                     };
            }
            set
            {
                _sn_food_gatherer_percentage_fa = value[0];
                _sn_wood_gatherer_percentage_fa = value[1];
                _sn_stone_gatherer_percentage_fa = value[2];
                _sn_gold_gatherer_percentage_fa = value[3];
                _sn_percent_civilian_builders_fa = value[4];
            }
        }

        /// <summary>
        /// Returns an array of outputs.
        /// </summary>
        public double[] GetOutputParams
        {
            get
            {
                return new double[]
                {
                    (double)_food_Score,
                    (double)_wood_Score,
                    (double)_stone_Score,
                    (double)_gold_Score
                };
            }
        }

		/// <summary>
		/// Gets the name of the ai.
		/// </summary>
		/// <value>
		/// The name of the ai.
		/// </value>
		public string AiName
		{
			get
			{
				return _aiName;
			}
		}

		/// <summary>
		/// Gets the game number.
		/// </summary>
		/// <value>
		/// The game number.
		/// </value>
		public int GameNumber
		{
			get
			{
				return _gameNumber;
			}
		}

        /// <summary>
        /// Gets the training set for this particular instance.
        /// </summary>
        /// <returns></returns>
        public TrainingSample GenerateAnnSample()
        {
            return new TrainingSample(GetInputParams, GetOutputParams);
        }

		/// <summary>
		/// Constructor for the ai training set.
		/// </summary>
		/// <param name="aiName">Name of the ai.</param>
		/// <param name="gameNumber">The game number.</param>
		/// <param name="sn_food_gatherer_percentage_fa">The sn_food_gatherer_percentage_fa.</param>
		/// <param name="sn_wood_gatherer_percentage_fa">The sn_wood_gatherer_percentage_fa.</param>
		/// <param name="sn_gold_gatherer_percentage_fa">The sn_gold_gatherer_percentage_fa.</param>
		/// <param name="sn_stone_gatherer_percentage_fa">The sn_stone_gatherer_percentage_fa.</param>
		/// <param name="sn_percent_civilian_builders_fa">The sn_percent_civilian_builders_fa.</param>
		/// <param name="food_Score">The food score.</param>
		/// <param name="wood_Score">The wood score.</param>
		/// <param name="stone_Score">The stone score.</param>
		/// <param name="gold_Score">The gold score.</param>
		public CoastalRaidersFuedalResourceManager(
			string aiName,
			int gameNumber,
            double sn_food_gatherer_percentage_fa,
			double sn_wood_gatherer_percentage_fa,
			double sn_gold_gatherer_percentage_fa,
			double sn_stone_gatherer_percentage_fa,
			double sn_percent_civilian_builders_fa,
            int food_Score,
            int wood_Score,
            int stone_Score,
            int gold_Score)
        {
			_aiName = aiName;
			_gameNumber = gameNumber;

            _sn_food_gatherer_percentage_fa = sn_food_gatherer_percentage_fa;
            _sn_wood_gatherer_percentage_fa = sn_wood_gatherer_percentage_fa;
            _sn_gold_gatherer_percentage_fa = sn_gold_gatherer_percentage_fa;
            _sn_stone_gatherer_percentage_fa = sn_stone_gatherer_percentage_fa;
            _sn_percent_civilian_builders_fa = sn_percent_civilian_builders_fa;

            _food_Score = food_Score;
            _wood_Score = wood_Score;
            _stone_Score = stone_Score;
            _gold_Score = gold_Score;
        }

		/// <summary>
		/// Generates the new ai file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public bool GenerateNewAiFile(string filePath)
		{
			string modifiablePart = string.Format(
				AI_Mutable_Code,
				Convert.ToInt32(_sn_food_gatherer_percentage_fa * 100),
				Convert.ToInt32(_sn_wood_gatherer_percentage_fa * 100),
				Convert.ToInt32(_sn_gold_gatherer_percentage_fa * 100),
				Convert.ToInt32(_sn_stone_gatherer_percentage_fa * 100),
				Convert.ToInt32(_sn_percent_civilian_builders_fa * 100));

			return BuildAIScriptParts(modifiablePart, filePath + AiName + _clonePrefix + ".per");
		}

		/// <summary>
		/// Resets the defaule ai file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public bool ResetDefauleAiFile(string filePath)
		{
			string modifiablePart = string.Format(AI_Mutable_Code, 47, 41, 12, 5, 10);
			return BuildAIScriptParts(modifiablePart, filePath + AiName + _clonePrefix + ".per");
		}

		/// <summary>
		/// Builds the ai script parts.
		/// </summary>
		/// <param name="mutatedInput">The mutated input.</param>
		/// <param name="filePathName">Name of the file path.</param>
		/// <returns></returns>
		private bool BuildAIScriptParts(string mutatedInput, string filePath)
		{
			try
			{
				StreamUtilities.GenerateScript(filePath, mutatedInput, "Data\\CoastalRaiders_top_part.txt", "Data\\CoastalRaiders_bottom_part.txt");
                return true;
            }
			catch (Exception e)
			{
				//log the exception or something...
				throw e;
				//return false;
			}
        }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format(
				"AI Name: {9}\n "+
				"Game Number: {10}\n" +
				"\tFood Percent: {0}\n" +
				"\tWood Percent: {1}\n" +
				"\tGold Percent: {2}\n" +
				"\tStone Percent: {3}\n" +
				"\tBuilders Percent: {4}\n" +

				"\tFood Score: {5}\n" +
				"\tWood Score: {6}\n" +
				"\tStone Score: {7}\n" +
				"\tGold Score: {8}\n",

				_sn_food_gatherer_percentage_fa,
				_sn_wood_gatherer_percentage_fa,
				_sn_gold_gatherer_percentage_fa,
				_sn_stone_gatherer_percentage_fa,
				_sn_percent_civilian_builders_fa,

				(double)_food_Score,
				(double)_wood_Score,
				(double)_stone_Score,
				(double)_gold_Score,

				AiName,
				GameNumber);
        }
	}      
}          
           