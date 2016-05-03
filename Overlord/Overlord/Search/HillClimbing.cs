using MySql.Data.MySqlClient;
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
            for (int i = playerInputLength; i < _inputData.Length; i++)
            {
                _inputData[i] = 0.2;
            }

            int playerOutputLength = _outputData.Length / 2;
            double[] totalSums = FindSums(playerOutputLength);
            double[] totalDeviations = FindStandardDeviation(playerOutputLength, totalSums);

			int tries = 0;
            while (totalSums[0] > totalSums[1] || totalDeviations[0] > totalDeviations[1])
            {
				tries++;
				// Compare output delta.
				int minimumDeltaIndex = 5;
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

                for (int i = playerInputLength; i < _inputData.Length; i++)
                {
                    if (i == minimumDeltaIndex)
                    {
                        // Stop the train when this condition is met!
                        if (CheckGreaterThanOneCondition(_inputData[i] + toleranceAmount))
                        {
                            _logger.Error("Program is stuck at local Max." + FormatData());
                            // return;
                            break;
                            
                        }

                        _inputData[i] += toleranceAmount;
                    }
                    else
                    {
                        // Stop the train when this condition is met!
                        if (CheckSignCondition(_inputData[i] - distributeBackOff))
                        {
                            _logger.Error("Program is stuck at local Max." + FormatData());
                            // return;
                            break;
                        }

                        _inputData[i] -= distributeBackOff;
                    }
                }

                // Execute the new learning set that was applied to input data. WARNING: Do not let the set become negative.
                _outputData = _nueralNetwork.Run(_inputData);
                _logger.Error("Network Input Attempt: " + FormatDataIntoString(_inputData));
                _logger.Error("Network Output Recieved: " + FormatDataIntoString(_outputData));


                // Revaluate to see if our totals are consistent. This will need some polishing so 
                // that our wile loop has a reasonable base case.
                totalSums = FindSums(playerOutputLength);
                totalDeviations = FindStandardDeviation(playerOutputLength, totalSums);
            }

			_logger.Warn(string.Format("Found a solution in {0} tries.", tries));
        }

        /// <summary>
        /// Finds the optimal solution2.
        /// This is my second attempt at writing a hill climbing algorithm. We should try and maximize the output first,
        /// then begin out ascent.
        /// </summary>
        /// <param name="toleranceAmount">The tolerance amount.</param>
        public void FindOptimalSolution2(double toleranceAmount = 0.04)
        {
            // Get maximum known score and apply it to the new data set, If old data set is used.
            string maxPlayerVal = @"
                SELECT GameId, p1Wood, p1Food, p1Gold, p1Stone, p1Builders,
	                p2Wood, p2Food, p2Gold, p2Stone, p2Builders, 
                    (p1WoodHighest + p1FoodHighest + p1GoldHighest + p1StoneHighest) AS EconScoreP1,
                    (p2WoodHighest + p2FoodHighest + p2GoldHighest + p2StoneHighest) AS EconScoreP2
                FROM aoenn.ai_neural_network_feed
                WHERE (p2WoodHighest + p2FoodHighest + p2GoldHighest + p2StoneHighest)  = 
                (
	                SELECT MAX(p2WoodHighest + p2FoodHighest + p2GoldHighest + p2StoneHighest) AS EconScoreP2
	                FROM aoenn.ai_neural_network_feed
                );            
            ";

            double[] p1Input = null;
            int econScoreP1;

            double[] p2Input = null;
            int econScoreP2;

            // read sql and assign values.
            StreamUtilities.ReadSql((MySqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    p1Input = new double[]
                    {
                        Convert.ToDouble(reader["p1Wood"]),
                        Convert.ToDouble(reader["p1Food"]),
                        Convert.ToDouble(reader["p1Gold"]),
                        Convert.ToDouble(reader["p1Stone"]),
                        Convert.ToDouble(reader["p1Builders"])
                    };

                    p2Input = new double[]
                    {
                        Convert.ToDouble(reader["p2Wood"]),
                        Convert.ToDouble(reader["p2Food"]),
                        Convert.ToDouble(reader["p2Gold"]),
                        Convert.ToDouble(reader["p2Stone"]),
                        Convert.ToDouble(reader["p2Builders"])
                    };

                    econScoreP1 = Convert.ToInt32(reader["EconScoreP1"]);
                    econScoreP2 = Convert.ToInt32(reader["EconScoreP2"]);
                }
            }, maxPlayerVal);

            double[] p1CurrentInputContext = new double[5];
            for(int i = 0; i < 5; i++)
            {
                p1CurrentInputContext[i] = _inputData[i];
            }

            // Seek second p1 input and find max of that, calculate the delta odds.
            double[] p1AndP2Input = p1CurrentInputContext.Concat(p2Input).ToArray();
            double[] p1AndP2Output = _nueralNetwork.Run(p1AndP2Input);

            _logger.Error("Network Input Attempt: " + FormatDataIntoString(p1AndP2Input));
            
            // This is the predicted output of the aggregate, i.e. MAX or best outcome for p2.
            // this result is vital to being hill climbing!
            _logger.Error("Network Output Recieved: " + FormatDataIntoString(p1AndP2Output));

            int minIndex = 0;

			//find min index
            for(int i = 1; i < 4; i++ )
            {
                if (p1AndP2Output[4 + minIndex] > p1AndP2Output[4 + minIndex])
                {
                    minIndex = i;
                }
            }

            //outputs split
            double[] p1Output =
            {
                p1AndP2Output[0], p1AndP2Output[1], p1AndP2Output[2], p1AndP2Output[3]
            };

            double[] p2Output =
            {
                p1AndP2Output[4], p1AndP2Output[5], p1AndP2Output[6], p1AndP2Output[7]
            };

            //input split
            double[] p1InputModified =
            {
                p1AndP2Input[0], p1AndP2Input[1], p1AndP2Input[2], p1AndP2Input[3], p1AndP2Input[4]
            };

            double[] p2InputModified =
            {
                p1AndP2Input[5], p1AndP2Input[6], p1AndP2Input[7], p1AndP2Input[8], p1AndP2Input[9]
            };

            //set climbing variables.
            bool climbing = true;
            double climbRate = toleranceAmount / 4;
            int climbIndex = minIndex;
            double climbedSum = SumVector(p2Output);
			int maxAttemps = 10000;
			int attemps = 0;
            //some general sense of climbing.
            while (climbing && attemps < maxAttemps)
            {
                climbIndex = (climbIndex + 1) % 5;
                if(climbIndex == minIndex)
                {
                    climbIndex = (climbIndex + 1) % 5;

                }

                p2InputModified[minIndex] += climbRate;
                p2InputModified[climbIndex] -= climbRate;

                double[] p1AndP2OutputPrime = _nueralNetwork.Run(p1InputModified.Concat(p2InputModified).ToArray());
                double currentAttempt = SumVector(new double[]{
                    p1AndP2OutputPrime[4], p1AndP2OutputPrime[5],p1AndP2OutputPrime[6],p1AndP2OutputPrime[7]
                });

                _logger.Error("Network Input Attempt: " + FormatDataIntoString(p1InputModified.Concat(p2InputModified).ToArray()));
                _logger.Error("Network Output Recieved: " + FormatDataIntoString(p1AndP2OutputPrime));

                if (currentAttempt < climbedSum)
                {
                    // climbing = false;
                    p2InputModified[minIndex] -= climbRate;
                    p2InputModified[climbIndex] += climbRate;
                }

				// find new min index if next climb index is min index
				if ((climbIndex + 1) % 5 == minIndex)
				{
					for (int i = 1; i < 5; i++)
					{
						if (p1AndP2OutputPrime[4 + minIndex] > p1AndP2OutputPrime[4 + minIndex])
						{
							minIndex = i;
						}
					}
				}


				attemps++;
            }

            // Write out to an output.
            double[] p1AndP2OutputFinal = _nueralNetwork.Run(p1InputModified.Concat(p2InputModified).ToArray());
            _logger.Error("Network Input Attempt: " + FormatDataIntoString(p1InputModified.Concat(p2InputModified).ToArray()));
            _logger.Error("Network Output Recieved: " + FormatDataIntoString(p1AndP2OutputFinal));
        }

        /// <summary>
        /// Sums the vector.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns></returns>
        private double SumVector(double[] output)
        {
            double result = 0;
            for (int i = 0; i < output.Length; i++)
            {
                result += output[i];
            }

            return result;
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

        /// <summary>
        /// Formats the data into string.
        /// </summary>
        /// <param name="dataArray">The data array.</param>
        /// <returns></returns>
        private string FormatDataIntoString(double[] dataArray)
        {
            string output = "[ ";

            for (int i = 0; i < dataArray.Length; i++)
            {
                if (i != dataArray.Length - 1)
                {
                    output += dataArray[i] + ", ";
                }
                else
                {
                    output += dataArray[i];
                }
            }

            output += " ]";

            return output;
        }
    }
}
