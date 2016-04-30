using NeuronDotNet.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Search
{
    /// <summary>
    /// This class implements our own take on the hill climbing optimization
    /// for the ANN network. Once the ANN learns about the game and contains a knowledge base of interpolated weights, it applies
    /// the hill climbing algorithm to illustrate how the AI improves by modifying the known values to something useful.
    /// This may be a hit or miss as we might hit only the local max and not the global max. But we'll
    /// see where this goes. Its a start.
    /// </summary>
    public class HillClimbing
    {
        /// <summary>
        /// The input data array. Since this is a local search algorithm,
        /// we need not worry about other expanded nodes.
        /// </summary>
        private double[] _inputData;

        /// <summary>
        /// The output data array.
        /// </summary>
        private double[] _outputData;

        /// <summary>
        /// The nueral network instance. Should this be singleton? Idk.
        /// </summary>
        private Network _nueralNetwork;

        /// <summary>
        /// The data list, represents a 3 dimensional array for storring all the delta information.
        /// </summary>
        private List<VectorN> _dataArray = new List<VectorN>();

        /// <summary>
        /// This object get the singleton instance of logger.
        /// </summary>
        private Logger _logger = Program.Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HillClimbing"/> class.
        /// </summary>
        /// <param name="incomming">The initial state of the script.</param>
        /// <param name="nueralNetwork">The nueral network to assert the predicitions.</param>s
        public HillClimbing(double[] incomming, Network nueralNetwork)
        {
            _inputData = incomming;
            _nueralNetwork = nueralNetwork;
            _outputData = _nueralNetwork.Run(_inputData);
        }

		/// <summary>
		/// Finds the optimal solution.
		/// Algorithm:
		///     1. Find the most largest delta  => Xi between player 1  => X[] and player 2 => Y[].
		///     2. Adjust that delta and devide that delta to all the other values.
		///     3. repeat steps 1 and 2 and until Y is maximized in Y`
		///     4. Continue to variable Y1 until Y`...YN` for player 2 is maximized, i.e. sum(Y`...YN`) > sum(X`...XN`)
		/// </summary>
		/// <param name="toleranceAmount">
		/// Defaults to 1/1,000th other wise specifiesthe incremental value of each controled vars.
		/// </param>
        public void FindOptimalSolution(double toleranceAmount = 0.04)
        {
			_logger.Warn("Finding optimal solution with Hill Climbing....");

            int playerInputLength = _inputData.Length / 2;
            int playerOutputLength = _outputData.Length / 2;
            double[] totalSums = FindSums(playerOutputLength);
            double[] totalDeviations = FindStandardDeviation(playerOutputLength, totalSums);

			int tries = 0;
            while (totalSums[0] > totalSums[1] || totalDeviations[0] > totalDeviations[1])
            {
				tries++;
				// Compare output delta.
				int minimumDeltaIndex = 0;
                double currentDelta = _outputData[minimumDeltaIndex + playerOutputLength] - _outputData[minimumDeltaIndex];

                // Find the least delta.
                for (int i = 0; i < playerOutputLength; i++)
                {
                    double localDelta = _outputData[i + playerOutputLength] - _outputData[i];

                    if (localDelta < currentDelta)
                    {
                        minimumDeltaIndex = i;
                        currentDelta = _outputData[minimumDeltaIndex + playerOutputLength] - _outputData[minimumDeltaIndex];
                    }
                }

                // Start climbing the hill. Raise the desired value by a small amount.
                // Decrease everything else by tolerance amount by tAm/number of other variables.
                double distributeBackOff = toleranceAmount / (playerInputLength - 1);

                for (int i = playerOutputLength; i < _outputData.Length; i++)
                {
                    if (i == minimumDeltaIndex)
                    {
                        // Stop the train when this condition is met!
                        if (CheckGreaterThanOneCondition(_inputData[i] + toleranceAmount))
                        {
                            _logger.Error("Program is stuck at local Max." + FormatData());
                            return;
                        }

                        _inputData[i] += toleranceAmount;
                    }
                    else
                    {
                        // Stop the train when this condition is met!
                        if (CheckSignCondition(_inputData[i] - distributeBackOff))
                        {
                            _logger.Error("Program is stuck at local Max." + FormatData());
                            return;
                        }

                        _inputData[i] -= distributeBackOff;
                    }
                }

                // Execute the new learning set that was applied to input data. WARNING: Do not let the set become negative.
                _outputData = _nueralNetwork.Run(_inputData);

                // Revaluate to see if our totals are consistent. This will need some polishing so 
                // that our wile loop has a reasonable base case.
                totalSums = FindSums(playerOutputLength);
                totalDeviations = FindStandardDeviation(playerOutputLength, totalSums);
            }

			_logger.Warn(string.Format("Found a solution in {0} tries.", tries));
        }

        
        /// <summary>
        /// Finds the sums.
        /// </summary>
        /// <returns>The total sums of both of the two players.</returns>
        private double[] FindSums(int playerOutputLength)
        {
            double sumPlayer1Output = 0;
            for (int i = 0; i < playerOutputLength; i++)
            {
                sumPlayer1Output += _outputData[i];
            }

            double sumPlayer2Output = 0;
            for (int i = playerOutputLength; i < _outputData.Length; i++)
            {
                sumPlayer2Output += _outputData[i];
            }

            return new double[] { sumPlayer1Output, sumPlayer2Output };
        }

        /// <summary>
        /// Finds the standatd deviation of each player.
        /// </summary>
        /// <returns>Returns the averages of the two players.</returns>
        private double[] FindStandardDeviation(int playerOutputLength, double[] totalSums)
        {
            double p1StandardDev = 0;
            double p1SquaredSum = 0;

            // Square everything and add them together.
            for (int i = 0; i < playerOutputLength; i++)
            {
                p1SquaredSum += _outputData[i] * _outputData[i];
            }

            // Find the normalized vector value.
            p1StandardDev = Math.Sqrt(p1SquaredSum / playerOutputLength);

            double p2StandardDev = 0;
            double p2SquaredSum = 0;

            for (int i = playerOutputLength; i < _outputData.Length; i++)
            {
                p2SquaredSum += _outputData[i] * _outputData[i];
            }

            p2StandardDev = Math.Sqrt(p2SquaredSum / playerOutputLength);

            return new double[] { p1StandardDev, p2StandardDev };
        }

        /// <summary>
        /// Checks the stopping conditions.
        /// Usually this predicts if the next hillclimbing move will break the normalization.
        /// </summary>
        /// <param name="numberTest">The number test.</param>
        /// <returns>A boolean whether to give up on the process or not.</returns>
        private bool CheckSignCondition(double numberTest)
        {
            if(Math.Sign(numberTest) == -1)
            {
                return true;
            }
            else if(Math.Sign(numberTest) == 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the greater than one condition.
        /// Usually this predicts if the next hillclimbing move will break the normalization.
        /// </summary>
        /// <param name="numberTest">The number test.</param>
        /// <returns>A boolean whether to give up on the process or not.</returns>
        private bool CheckGreaterThanOneCondition(double numberTest)
        {
            if(numberTest >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Formats the data.
        /// </summary>
        /// <returns>Returns a formatted string of outputs. I'm using this incase something goes wrong!</returns>
        private string FormatData()
        {
            string data = "\tInputData: \n\t\t";
            for (int i = 0; i < _inputData.Length; i++)
            {
                data += " " + _inputData[i];
            }

            data += "\n\tOutputData: \n\t\t";
            for (int i = 0; i < _outputData.Length; i++)
            {
                data += " " + _outputData[i];
            }

            return data;
        }

        /// <summary>
        /// Generates the random solution.
        /// </summary>
        /// <returns>
        /// A random set of solution for the AI writer to take and apply to the game.
        /// </returns>
        public bool GenerateRandomSolution(int axisToRaise, double toleranceAmount = 0.04)
        {
            int playerInputLength = _inputData.Length / 2;

            if (CheckGreaterThanOneCondition(_inputData[playerInputLength + axisToRaise] + toleranceAmount))
            {
                _logger.Error("Program is stuck at local Max." + FormatData());
                return false;
            }

            _inputData[playerInputLength + axisToRaise] += toleranceAmount;

            double backoffAmount = toleranceAmount / (playerInputLength - 1);
            for (int i = playerInputLength; i < _inputData.Length; i++)
            {
                if ((axisToRaise + playerInputLength) != i)
                {
                    // Stop the train when this condition is met!
                    if (CheckSignCondition(_inputData[i] - backoffAmount))
                    {
                        _logger.Error("Program is stuck at local Max." + FormatData());
                        return false;
                    }

                    _inputData[i] -= (toleranceAmount / 4.0);
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the get ideal input data.
        /// </summary>
        /// <value>
        /// The get ideal input data.
        /// </value>
        /// <remarks>
        /// This data is useless without running FindOptimalSolution().
        /// </remarks>
        public double[] GetInputData
        {
            get
            {
                return _inputData;
            }
        }

        /// <summary>
        /// Generate a list based on any three pivot axis. Allows us to sweep the current data set and generate whatever we think
        /// is a reasonable list of data.
        /// </summary>
        /// <param name="axisX">The axis x.</param>
        /// <param name="axisY">The axis y.</param>
        /// <param name="tolleranceAmount">Careful with this, the precision here is what decides the detail of our graph.
        /// By default it is 100X100</param>
        /// <returns>The data list, represents a 3 dimensional array for storing all the delta information.</returns>
        /// <remarks>
        /// VectorN should be a 3 dimensinoal array. Don't goof it up!
        /// In order to store 5 dimensional data, 
        /// I'm designing this so that 3 of the first axies illustrate the entire
        /// 3d graph. Depending on each location on the of the second graph X2,Z2 plane the Y2 represents the first graph's local max
        /// which is the max of X1,Y1,Z1.
        /// 
        /// The Z is the normalized result? Just experimenting ways to display graph.
        /// </remarks>
        public List<VectorN> GenerateTopologyData(int axisX, int axisY, double toleranceAmount = 0.01)
        {
			_logger.Debug("Beginning topology generation...");
            List<VectorN> tempDataArray = new List<VectorN>();

			// Copy array.
			double[] inputLocal = new double[_inputData.Length];
			for(int i = 0; i < inputLocal.Length; i++)
			{
				inputLocal[i] = _inputData[i];
            }

            int playerInputLength = inputLocal.Length / 2;

			VectorN variableDataVector = new VectorN(2);
			variableDataVector[0] = 0;
			variableDataVector[1] = 0;

			inputLocal[playerInputLength + axisX] = 0;
			inputLocal[playerInputLength + axisY] = 0;

			int iterations = (int)(1.0/ toleranceAmount);
			_logger.Trace(string.Format("Running for {0} iterations.",iterations));

			for (int i = 0; i < iterations; i++)
            {
                for(int j = 0; j < iterations; j++)
                {
                    double localOutNormalized =  (new VectorN(_nueralNetwork.Run(inputLocal))).Length;
					
					// I'm following unity's coordinate system. It's the easiest thing I can remember.
					tempDataArray.Add(new VectorN(new double[3]{ i, localOutNormalized, j }));

                    inputLocal[playerInputLength + axisX] = i * toleranceAmount;
                    inputLocal[playerInputLength + axisY] = j * toleranceAmount;
				}
            }

			// Create new plot set entry.
			StreamUtilities.CreateNewPlot(axisX, axisY, toleranceAmount);
			_logger.Debug("Topology generation completed, data visualization can now be executed.");
            return tempDataArray;
        }

        /// <summary>
        /// Generates the unormalized topology data.
        /// </summary>
        /// <param name="axisX">The axis x.</param>
        /// <param name="axisY">The axis y.</param>
        /// <param name="toleranceAmount">The tolerance amount.</param>
        /// <returns>Some vectors.</returns>
        public List<VectorN> GenerateUnormalizedTopologyData(int axisX, int axisY, double toleranceAmount = 0.01)
        {
            _logger.Debug("Beginning unnomrailized* topology generation...");
            List<VectorN> tempDataArray = new List<VectorN>();

            // Copy array.
            double[] inputLocal = new double[_inputData.Length];
            for (int i = 0; i < inputLocal.Length; i++)
            {
                inputLocal[i] = _inputData[i];
            }

            int playerInputLength = inputLocal.Length / 2;

            VectorN variableDataVector = new VectorN(2);
            variableDataVector[0] = 0;
            variableDataVector[1] = 0;

            inputLocal[playerInputLength + axisX] = 0;
            inputLocal[playerInputLength + axisY] = 0;

            int iterations = (int)(1.0 / toleranceAmount);
            _logger.Trace(string.Format("Running for {0} iterations.", iterations));

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < iterations; j++)
                {
                    VectorN unnormalized = new VectorN(_nueralNetwork.Run(inputLocal));

                    // I'm following unity's coordinate system. It's the easiest thing I can remember.
                    tempDataArray.Add(new VectorN(
                        new double[6]
                        {
                            i,
                            j,
                            unnormalized[0],
                            unnormalized[1],
                            unnormalized[2],
                            unnormalized[3]
                        }));

                    inputLocal[playerInputLength + axisX] = i * toleranceAmount;
                    inputLocal[playerInputLength + axisY] = j * toleranceAmount;
                }
            }

            // Create new plot set entry.
            //StreamUtilities.CreateNewPlot(axisX, axisY, toleranceAmount);
            _logger.Debug("Topology generation completed, data visualization can now be executed.");
            return tempDataArray;
        }
    }
}
