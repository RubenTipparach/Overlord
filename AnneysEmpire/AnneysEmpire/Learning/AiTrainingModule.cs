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

			TrainingSet trainingSet = new TrainingSet(5, 4);

            using (StreamReader reader = new StreamReader(file))
            {
                Console.SetIn(reader);
                String line = "";
                //trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 0, 1 }, new double[1] { 1 }));
                while((line = reader.ReadLine())!= null)
                {
                    String[] array = line.Split(',');
                    double[] inputArray = new double[5];
                    double[] outputArray = new double[4];

                    for(int i = 0; i < 5; i++)
                    {
                        inputArray[i] = Convert.ToDouble(array[i]);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        outputArray[i] = Convert.ToDouble(array[i+6]);
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

			double[] outputResult = network.OutputLayer.GetOutput();
			Console.WriteLine("final output");


		}
    }
}
