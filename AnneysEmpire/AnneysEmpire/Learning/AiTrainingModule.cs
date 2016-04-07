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
	public class AiTrainingModule
	{
		/// <summary>
		/// Created a bullshit method to train bullshit results.
		/// </summary>
		/// <param name="writer"></param>
		public static void Test(
			string file,
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

			TrainingSet trainingSet = new TrainingSet(10, 8);

            // Do I care about reader? Nah, probably not.
            var inDefaule = Console.In;
			using (StreamReader reader = new StreamReader(file))
			{
				Console.SetIn(reader);
				String line = "";
				//trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 0, 1 }, new double[1] { 1 }));
				while((line = reader.ReadLine())!= null)
				{
					String[] array = line.Split(',');
					double[] inputArray = new double[10];
					double[] outputArray = new double[8];

					for(int i = 0; i < 10; i++)
					{
						inputArray[i] = Convert.ToDouble(array[i]);
					}

					for (int i = 0; i < 8; i++)
					{
						outputArray[i] = Convert.ToDouble(array[i+11]);
					}

					trainingSet.Add(new TrainingSample(inputArray, outputArray));
				}
			}

			double max = 0;

			// create an anonymous function to capture the error value of each iteration, and report back the percent of completion.
			network.EndEpochEvent +=
				delegate(object networkInput, TrainingEpochEventArgs args)
				{
					errorList[args.TrainingIteration] = network.MeanSquaredError;
					max = Math.Max(max, network.MeanSquaredError);
					// PercentComplete = args.TrainingIteration * 100 / numberOfCycles;
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

            // print out the error list for scientific evaluation.
            StreamUtilities.DumpData("dumpErrorValues.txt", errorList);

			double[] outputResult = network.OutputLayer.GetOutput();


			outputResult = network.Run(new double[] { 0.47, 0.41, 0.12, 0.05, 0.1, 0.5, 0.1, 0.1, 0.05, 0.1 });
			
			foreach( var d in outputResult)
			{
				Console.WriteLine("output: " + d);
			}

			// Console.WriteLine("final output");
		}
	}
}
