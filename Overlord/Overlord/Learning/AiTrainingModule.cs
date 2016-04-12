using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Learning
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
        /// What the current state of the running statistics are.
        /// </summary>
        private CoastalRaidersFuedalResourceManager _currentStats;

        /// <summary>
        /// Use ghost docs.
        /// </summary>
        public CoastalRaidersFuedalResourceManager CurrentStats
        {
            get { return _currentStats; }
            set { _currentStats = value; }
        }

        /// <summary>
        /// The brains of the operation, the glorious neural network that we will be using here.
        /// </summary>
        private Network _nueralNetwork;

        /// <summary>
        /// Number of cycles for the initial training set to start out on.
        /// </summary>
        private int _numberOfInitialCycles;

        /// <summary>
        /// Number of training sets to re-run each time a new game is added to the pattern.
        /// </summary>
        private int _numberOfContinuousCycles;

        /// <summary>
        /// This just keeps appending values to an unbound list.
        /// </summary>
        private LinkedList<double> _errorList;

        /// <summary>
        /// Each time the network learns some new stuff. Its brain is refreshed with a few more
        /// training loops.
        /// </summary>
        private int _numberOfNeuronRefreshes;

        /// <summary>
        /// This constructor creates a default network to work with.
        /// </summary>
        /// <param name="aoe2Directory">Directory of your age of empires game.</param>
        /// <param name="aiScript">Name of your ai script that you want to generate.</param>
        public AiTrainingModule(string aoe2Directory, string aiScript)
        {
            _aoe2Directory = aoe2Directory;
            _aiScript = aiScript;

            _numberOfInitialCycles = 100000;
            _numberOfContinuousCycles = 10000;
            _numberOfNeuronRefreshes = 0;

            // Keep track of random number of neurons here.
            int numberOfInputNeurons = 10;
            int numberOfHiddenNeurons = 10;
            int numberOfOutputNeurons = 8;

            double learningRate = 0.25;
            _errorList = new LinkedList<double>();

            LinearLayer inputLayer = new LinearLayer(numberOfInputNeurons);
            SigmoidLayer hiddenLayer = new SigmoidLayer(numberOfHiddenNeurons);
            SigmoidLayer outputLayer = new SigmoidLayer(numberOfOutputNeurons);

            // Wow, how hidden is really hidden. So that I think these connectors do is
            // insert themselves as part of the various layers. This really hides the hidden
            // layer from the network, as only the connectors then modify the hidden layer.
            // In other words "trained".
            var conn1 = new BackpropagationConnector(inputLayer, hiddenLayer);
            var conn2 = new BackpropagationConnector(hiddenLayer, outputLayer);

            _nueralNetwork = new BackpropagationNetwork(inputLayer, outputLayer);
            _nueralNetwork.SetLearningRate(learningRate);
            _nueralNetwork.EndEpochEvent += BackgroundTasks; // hehe call back methods.

			// Needs to make initial configuration of AI.

            // If this module is being instantiated for the first time, create a comprehensive
            // knowledge base/ network so it can continue where it last left off. Tweak the
            // query to filter outliers.
            _rawMgxStats = StreamUtilities.GetAiDataSet();
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
            throw new NotImplementedException("I'm not done with this yet!");
		}

        /// <summary>
        /// This method pushes new data into the neuralNetwork along with existing data, and continues the training procedure.
        /// </summary>
		public void PushNewTrainingSet()
		{
            _rawMgxStats = StreamUtilities.GetAiDataSet();
            _nueralNetwork.Learn(CompileTrainingSet(), _numberOfContinuousCycles);

            _numberOfContinuousCycles++;
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

        private void BackgroundTasks(object networkInput, TrainingEpochEventArgs args)
        {
            _errorList.AddLast(((BackpropagationNetwork)_nueralNetwork).MeanSquaredError);
            // ugh whatever else is supposed to go here.
        }
	}
}
