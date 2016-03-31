using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
	public class TestNdn2
	{
		public static double PercentComplete = 0;

		/// <summary>
		/// Created a bullshit method to train bullshit results.
		/// </summary>
		/// <param name="writer"></param>
		public TestNdn2(
			int numberOfInputNeurons,
			int numberOfHiddenNeurons,
			int numberOfOutputNeurons,
			int numberOfCycles = 50000,
			double learningRate = 0.25)
		{

			TrainingSample sample = new TrainingSample(
				new double[] { },
				new double[] { });

			//We might make a gui for this later.

			double[] errorList = new double[numberOfCycles];

			int totalNumberOfNeurons = numberOfInputNeurons + numberOfOutputNeurons;

			LinearLayer inputLayer = new LinearLayer(numberOfInputNeurons);
			SigmoidLayer hiddenLayer = new SigmoidLayer(numberOfHiddenNeurons);
			SigmoidLayer outputLayer = new SigmoidLayer(numberOfOutputNeurons);

			// This layer is a event handler that fires when the output is generated, hence backpropagation.
			BackpropagationConnector conn1 = new BackpropagationConnector(inputLayer, hiddenLayer);
			BackpropagationConnector conn2 = new BackpropagationConnector(hiddenLayer, outputLayer);

			BackpropagationNetwork network = new BackpropagationNetwork(inputLayer, outputLayer);
			network.SetLearningRate(learningRate);

			TrainingSet trainingSet = new TrainingSet(5, 1);
			trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 0, 1 }, new double[1] { 1 }));
			trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 1, 1 }, new double[1] { 1 }));
			trainingSet.Add(new TrainingSample(new double[] { 0, 0, 1, 1, 1 }, new double[1] { 1 }));
			trainingSet.Add(new TrainingSample(new double[] { 1, 1, 1, 1, 1 }, new double[1] { 1 }));
			trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 0, 0 }, new double[1] { 0 }));
			trainingSet.Add(new TrainingSample(new double[] { 1, 1, 0, 0, 0 }, new double[1] { 0 }));
			trainingSet.Add(new TrainingSample(new double[] { 1, 1, 1, 0, 0 }, new double[1] { 0 }));
			trainingSet.Add(new TrainingSample(new double[] { 1, 1, 1, 1, 0 }, new double[1] { 0 }));

			double max = 0;

			// create an anonymous function to capture the error value of each iteration, and report back the percent of completion.
			network.EndEpochEvent +=
				delegate(object networkInput, TrainingEpochEventArgs args)
				{
					errorList[args.TrainingIteration] = network.MeanSquaredError;
					max = Math.Max(max, network.MeanSquaredError);
					PercentComplete = args.TrainingIteration * 100 / numberOfCycles;
				};

			network.Learn(trainingSet, numberOfCycles);

			// idk what this is for....
			double[] indices = new double[numberOfCycles];
			// for (int i = 0; i < numberOfCycles; i++) { indices[i] = i; } .. oh nvm, its for graphing the learning curve

			// what to do for error list?
			// errorList => for plotting stuff.
			// for (int i = 0; i < numberOfCycles; i++)
			// {
				//Console.WriteLine(errorList[i]);
			//  }

			double[] outputResult = network.OutputLayer.GetOutput();
			Console.WriteLine("final output");
		 
			double[] r1 = new double[] { 0, 0, 0, 0, 0 };
			double[] r2 = new double[] { 0, 0, 0, 0, 1 };
			double[] r3 = new double[] { 0, 0, 1, 1, 1 };   
			double[] r4 = new double[] { 1, 1, 1, 1, 1 };
			double[] r5 = new double[] { 1, 0, 0, 0, 0 };
			double[] r6 = new double[] { 1, 1, 0, 0, 0 };
			double[] r7 = new double[] { 1, 1, 1, 0, 0 };
			double[] r8 = new double[] { 0, 0, 1, 1, 0 };
			double[] r9 = new double[] { 0, 1, 1, 1, 1 };

			Console.WriteLine(" r1 => " + network.Run(r1)[0]);
			Console.WriteLine(" r2 => " + network.Run(r2)[0]);
			Console.WriteLine(" r3 => " + network.Run(r3)[0]);
			Console.WriteLine(" r4 => " + network.Run(r4)[0]);
			Console.WriteLine(" r5 => " + network.Run(r5)[0]);
			Console.WriteLine(" r6 => " + network.Run(r6)[0]);
			Console.WriteLine(" r7 => " + network.Run(r7)[0]);
			Console.WriteLine(" r8 => " + network.Run(r8)[0]);
			Console.WriteLine(" r9 => " + network.Run(r9)[0]);
		}
	}
}
