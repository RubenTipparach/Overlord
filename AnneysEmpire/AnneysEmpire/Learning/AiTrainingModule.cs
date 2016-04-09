using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire.Learning
{
	/// <summary>
	/// This training module serves as an automated program to collect, analyze data,
	/// and improve the AI knowledge base. Players should find a statistical edge when
	/// testing this AI, as the AI will learn from its mistakes and generate a new script
	/// after every game. Getting slightly better...
	/// 
	/// Should also include a randomizer mode so we can autonomously gather data after every
	/// round. This is still a todo kinda thing...
	/// </summary>
	public class AiTrainingModule
	{
        /// <summary>
        /// Directory of your age of empires game.
        /// </summary>
		private string _aoe2Directory;

        /// <summary>
        /// Name of your ai script that you want to generate.
        /// </summary>
        /// <remarks>
        /// Each iteration of this training engine will allow you to generate a new AI.
        /// </remarks>
		private string _aiScript;

        /// <summary>
        /// This is the current AI class we are using to generate AIs for.
        /// The list is to generate random iterations I guess. It might also be training set?
        /// It's prototype code so it doen have fancy pants methods or anything, just straight forward
        /// resource partition values and a bunch of file write methods.
        /// </summary>
        /// <remarks>If I have more time later, this will be improved upon.</remarks>
		private List<CoastalRaidersFuedalResourceManager> _rawMgxStats;

        /// <summary>
        /// 
        /// </summary>
		private CoastalRaidersFuedalResourceManager _currentStats;

        /// <summary>
        /// The brains of the operation, the glorious neural network that we will be using here.
        /// </summary>
        private Network _nueralNetwork;

        private int _numberOfInitialCycles;

        private int _numberOfContinuousCycles;

        /// <summary>
        /// This constructor creates a default network to work with.
        /// </summary>
        /// <param name="aoe2Directory">Directory of your age of empires game.</param>
        /// <param name="aiScript">Name of your ai script that you want to generate.</param>
        public AiTrainingModule(string aoe2Directory, string aiScript)
        {
            _aoe2Directory = aoe2Directory;
            _aiScript = aiScript;

            _rawMgxStats = StreamUtilities.GetAiDataSet();

            _numberOfInitialCycles = 100000;
            _numberOfContinuousCycles = 10000;

            // If this module is being instantiated for the first time, create a comprehensive
            // knowledge base/ network so it can continue where it last left off. Tweak the
            // query to filter outliers.
            _nueralNetwork.Learn(CompileTrainingSet(), _numberOfInitialCycles);
        }

        /// <summary>
        /// This other constructor requires a network to be passed in.
        /// </summary>
        /// <param name="aoe2Directory">Directory of your age of empires game.</param>
        /// <param name="aiScript">Name of your ai script that you want to generate.</param>
        /// <param name="network">Assemble a neural network outside this plz.</param>
		public AiTrainingModule(string aoe2Directory, string aiScript, Network network)
		{
			_aoe2Directory = aoe2Directory;
			_aiScript = aiScript;

			_rawMgxStats = new List<CoastalRaidersFuedalResourceManager>();
		}

        /// <summary>
        /// This method pushes new data into the neuralNetwork along with existing data, and continues the training procedure.
        /// </summary>
		public void PushNewTrainingSet()
		{
            _rawMgxStats = StreamUtilities.GetAiDataSet();
            _nueralNetwork.Learn(CompileTrainingSet(), _numberOfContinuousCycles);
		}


        /// <summary>
        /// Brings all the ai list together into a training set to do some killer stuff.
        /// </summary>
        /// <returns></returns>
        private TrainingSet CompileTrainingSet()
        {
            TrainingSet tset = new TrainingSet(_currentStats.GetInputParams.Length, _currentStats.GetOutputParams.Length);
            foreach (var tsample in _rawMgxStats)
            {
                tset.Add(tsample.GenerateAnnSample());
            }

            return tset;
        }

	}
}
