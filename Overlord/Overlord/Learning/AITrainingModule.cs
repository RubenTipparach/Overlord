using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NLog;
using Overlord.Search;
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
	public class AITrainingModule
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
		/// Gets or sets the current stats.
		/// </summary>
		/// <value>
		/// The current stats.
		/// </value>
		public CoastalRaidersFuedalResourceManager CurrentStats
		{
			get
			{
				return _currentStats;
			}
			set
			{
				_currentStats = value;
			}
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
		/// The logger instance.
		/// </summary>
		private Logger _logger;

		/// <summary>
		/// The percentage of completion.
		/// </summary>
		private double _percent = 0;

		/// <summary>
		/// This is the hill climbing algorithm instance. It has some builtin functionality that allows it
		/// to try and find the global optimum. It also has additional capabilities such as generating
		/// a graph or data set that might be useful for visualizing the prediction output of the neural network.
		/// </summary>
		private HillClimbing _climber;

		/// <summary>
		/// Gets the climber.
		/// </summary>
		/// <value>
		/// The climber.
		/// </value>
		public HillClimbing Climber
		{
			get
			{
				return _climber;
			}
		}

		/// <summary>
		/// This constructor creates a default network to work with.
		/// </summary>
		/// <param name="aoe2Directory">Directory of your age of empires game.</param>
		/// <param name="aiScript">Name of your ai script that you want to generate.</param>
		public AITrainingModule(string aoe2Directory, string aiScript)
		{
			_logger = Program.Logger;
			_logger.Info("Initializing Training module.");

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
			_logger.Warn("Begining initial training cycle...");

			// If this module is being instantiated for the first time, create a comprehensive
			// knowledge base/ network so it can continue where it last left off. Tweak the
			// query to filter outliers.
			_rawMgxStats = StreamUtilities.GetAiDataSet();

			_nueralNetwork.EndEpochEvent += 
				(object networkInput, TrainingEpochEventArgs args) =>
				{
					if (_percent % (_numberOfInitialCycles/100) == 0 && _percent > 0)
					{
						_logger.Info(string.Format("Precent completed {0}%", _percent / (_numberOfInitialCycles/100)));
					}

					_percent++;
				};

			_nueralNetwork.Learn(CompileTrainingSet(_rawMgxStats), _numberOfInitialCycles);
			_logger.Warn("Finished initial training cycle.");

            // Get the latest dataset so we can generate some kind of graph and push the data set to database.
            var knowledgeBase = StreamUtilities.GetLatestAiEntry().ToList();
            var aiTrainingSet = CompileTrainingSet(knowledgeBase);

            _currentStats = knowledgeBase[knowledgeBase.Count - 1];

			// push data, hacked to show simple output
			//double[] veryFirstInput
			//	 =
			//{
			//	0.2,0.2,0.2,0.2,0.2,
			//	0.2,0.2,0.2,0.2,0.2
			//};

			_climber = new HillClimbing(aiTrainingSet[0].InputVector, _nueralNetwork);
			// _climber = new HillClimbing(veryFirstInput, _nueralNetwork);


			// Hardcoding these dimensions, I'm that lazy :[
			int ordinalTracker = 1;
            for(int i = 0; i < 4; i++)
            {
                for (int j = i+1; j < 5; j++)
                {
                    //write normalized data
                    StreamUtilities.SubmitPlotableData(_climber.GenerateTopologyData(i, j), ordinalTracker);

                    //write unnormalized data.
                    StreamUtilities.SubmitPlotableUnnormailizedData(_climber.GenerateUnormalizedTopologyData(i, j), ordinalTracker);
                    _logger.Debug(string.Format("Writing Axis{0} and Axis{1} with ordinal {2}.",i, j, ordinalTracker));
                    ordinalTracker++;
                }
            }

            // If input table == output, then a new game is needed
            if(StreamUtilities.CheckIfNewGameIsNeeded())
            {
                TriggerNewGame();
            }
		}

		/// <summary>
		/// Gets the ideal input data.
		/// </summary>
		/// <returns></returns>
		public double[] GetIdealInputData()
		{
			_climber.FindOptimalSolution();
			return _climber.GetInputData;
		}

        /// <summary>
        /// Gets the standard input data.
        /// </summary>
        /// <param name="axisToRaise">The axis to raise.</param>
        /// <returns></returns>
        public double[] GetStandardInputData(int axisToRaise)
        {
            _climber.GenerateRandomSolution(axisToRaise);
            return _climber.GetInputData;
        }

		/// <summary>
		/// This other constructor requires a network to be passed in.
		/// </summary>
		/// <param name="aoe2Directory">Directory of your age of empires game.</param>
		/// <param name="aiScript">Name of your ai script that you want to generate.</param>
		/// <param name="network">Assemble a neural network outside this plz.</param>
		public AITrainingModule(string aoe2Directory, string aiScript, Network network)
		{
			_aoe2Directory = aoe2Directory;
			_aiScript = aiScript;
			_logger = Program.Logger;

			_rawMgxStats = new List<CoastalRaidersFuedalResourceManager>();
			throw new NotImplementedException("I'm not done with this yet!");
		}

        /// <summary>
        /// This method pushes new data into the neuralNetwork along with existing data, and continues the training procedure.
        /// </summary>
        public void PushNewTrainingSet()
        {
            _numberOfInitialCycles = _numberOfContinuousCycles;
            _percent = 0;
            _rawMgxStats = StreamUtilities.GetAiDataSet();
            _nueralNetwork.Learn(CompileTrainingSet(_rawMgxStats), _numberOfContinuousCycles);

            if (StreamUtilities.CheckIfNewGameIsNeeded())
            {
                TriggerNewGame();
            }

            //_numberOfContinuousCycles++; Why did I add this?
            _logger.Warn(string.Format("Finished additional {0} training cycles.", _numberOfContinuousCycles));

		}

        /// <summary>
        /// Triggers the new game.
        /// </summary>
        private void TriggerNewGame()
        {
            // Randomly tweak some inputs.
            Random r = new Random();

            // Select optimization model.
            if (Configurations.GameGenerationMode() == GameGenMode.HillClimbing)
            {
                _climber.FindOptimalSolution();
            }
            else if(Configurations.GameGenerationMode() == GameGenMode.HillClimbing2)
            {
                _climber.FindOptimalSolution2();
            }
            else
            {
                int randomNum = r.Next(5);
                while (!_climber.GenerateRandomSolution(randomNum))
                {
                    randomNum = r.Next(5);
                }
            }

            double[] input = new double[10];

            input = _climber.GetInputData;

            // Get a bunch of custom input stuff.
            if (Configurations.UseCustomP1Input)
            {
                double[] newInput = Configurations.RetrievePlayer1Input();

                for (int i = 0; i < 5; i++)
                {
                    input[i] = newInput[i];
                }
            }

            double[] p2Input = new double[]
            {
                input[5],
                input[6],
                input[7],
                input[8],
                input[9]
            };

            _currentStats.GetInputParams = p2Input;
            _currentStats.GenerateNewAiFile(_aiScript);
            StreamUtilities.GenerateNewInput(input);
        }


		/// <summary>
		/// Brings all the ai list together into a training set to do some killer stuff.
		/// </summary>
		/// <returns>Compilation of a single training set.</returns>
		private TrainingSet CompileTrainingSet(List<CoastalRaidersFuedalResourceManager> rawMgxStats)
		{
			if(rawMgxStats.Count == 0)
			{
				Program.Logger.Error("There are currently now stats availible in the System to build a database.");
				Program.Logger.Error("Attemting to generate new entry....");
				// Generate brand new AI entry in here to test the auto data collection capability.
			}

			TrainingSet tset = new TrainingSet(rawMgxStats[0].GetInputParams.Length*2, rawMgxStats[0].GetOutputParams.Length*2);
			for (int i = 0; i < rawMgxStats.Count; i += 2)
			{
				var player1 = rawMgxStats[i].GenerateAnnSample();
				var player2 = rawMgxStats[i + 1].GenerateAnnSample();

				// Some bad ass Linq right here.
				var trainingSample = new TrainingSample(
					player1.InputVector.Concat(player2.InputVector).ToArray(),
					player1.OutputVector.Concat(player2.OutputVector).ToArray());

				tset.Add(trainingSample);
			}

			return tset;
		}


        // What the f am I doing?
		private void BackgroundTasks(object networkInput, TrainingEpochEventArgs args)
		{
			_errorList.AddLast(((BackpropagationNetwork)_nueralNetwork).MeanSquaredError);
			// ugh whatever else is supposed to go here.
		}
	}
}
