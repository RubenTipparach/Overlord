using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.Initializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord
{
	/// <summary>
	/// This class was used as a test to construct a sample neural network model.
	/// </summary>
	public class TestingNdn 
	{
		public static double PercentComplete = 0;

		/// <summary>
		/// This constructs a training procedure for standard backpropagation techniques.
		/// More advanced ones will be used as seen in the example.
		/// </summary>
		/// <param name="writer"></param>
		public TestingNdn(StreamWriter writer)
		{

			TrainingSample sample = new TrainingSample(
				new double[] { },
				new double[] { });

			//We might make a gui for this later.
			int numberOfNeurons = 3;
			double learningRate = 0.5;
			int numberOfCycles = 10000;

			double[] errorList = new double[numberOfCycles];

			LinearLayer inputLayer = new LinearLayer(2);
			SigmoidLayer hiddenLayer = new SigmoidLayer(numberOfNeurons);
			SigmoidLayer outputLayer = new SigmoidLayer(1);
			
			// This layer is a event handler that fires when the output is generated, hence backpropagation.
			BackpropagationConnector conn1 = new BackpropagationConnector(inputLayer, hiddenLayer);
			BackpropagationConnector conn2 = new BackpropagationConnector(hiddenLayer, outputLayer);

			BackpropagationNetwork network = new BackpropagationNetwork(inputLayer, outputLayer);
			network.SetLearningRate(learningRate);

			TrainingSet trainingSet = new TrainingSet(2, 1);
			trainingSet.Add(new TrainingSample(new double[2] { 0, 0 }, new double[1] { 0 }));
			trainingSet.Add(new TrainingSample(new double[2] { 0, 1 }, new double[1] { 1 }));
			trainingSet.Add(new TrainingSample(new double[2] { 1, 0 }, new double[1] { 1 }));
			trainingSet.Add(new TrainingSample(new double[2] { 1, 1 }, new double[1] { 0 }));
																						 
			double max = 0;

			// create an anonymous function to capture the error value of each iteration, and report back the percent of completion.
			network.EndEpochEvent +=
				delegate (object networkInput, TrainingEpochEventArgs args)
				{
					errorList[args.TrainingIteration] = network.MeanSquaredError;
					max = Math.Max(max, network.MeanSquaredError);
					PercentComplete = args.TrainingIteration * 100 / numberOfCycles;
				};

			network.Learn(trainingSet, numberOfCycles);

			double[] indices = new double[numberOfCycles];
			// for (int i = 0; i < numberOfCycles; i++) { indices[i] = i; } .. oh nvm, its for graphing the learning curve

			// what to do for error list?
			// errorList => for plotting stuff.
			for (int i = 0; i < numberOfCycles; i++)
			{
				//Console.WriteLine(errorList[i]);
			}

			double[] outputResult = network.OutputLayer.GetOutput();
			Console.WriteLine("final output");

			double[] r1 = new double[] { 0, 0 };
			double[] r2 = new double[] { 0, 1 };
			double[] r3 = new double[] { 1, 0 };
			double[] r4 = new double[] { 1, 1 };

			Console.WriteLine(" 0 0 => " + network.Run(r1)[0]);
			Console.WriteLine(" 0 1 => " + network.Run(r2)[0]);
			Console.WriteLine(" 1 0 => " + network.Run(r3)[0]);
			Console.WriteLine(" 1 1 => " + network.Run(r4)[0]);
		}
	}
}
