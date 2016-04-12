using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord
{
	public class ThreeLayerNueralNet
	{
		/// <summary>
		/// Runs the Three layer test neural network.
		/// </summary>
		public static void RunNet()
		{
			//Initialize example matrices to teach neural net.
			Matrix xArray = new Matrix(
				new double[4, 3] {
					// columns then rows.... ugh stupid c# 
					{0,0,1},
					{0,1,1},
					{1,0,1},
					{1,1,1}});

			VectorN yArray = new VectorN(new double[4] { 0, 1, 1, 0 });

			// fill wieghted array with random weights,
			// and teach it to conform
			// with the approximate statistics
			Random r = new Random(1);

			Matrix l0 = new Matrix(3, 4);
			Matrix l1 = new Matrix(3, 4);
			VectorN l2 =  new VectorN(4);

			Matrix syn0 = new Matrix(3, 4); // random array of a 3X4 matrix
			VectorN syn1 = new VectorN(4); // random array of a 4X1 vector

			// Seeding the neurons with random weights.
			for (int j = 0; j < syn0.Columns; j++)
			{
				for (int i = 0; i < syn0.Rows; i++ )
				{
					syn0[i, j] = 2*r.NextDouble() - 1;
				}

				syn1[j] = 2*r.NextDouble() - 1;
			}

			// begin firing neurons.
			for(int i = 0; i < 60001; i++)
			{
				l0 = xArray;
				l1 = Matrix.ApplyCustomOperation(AMath.Sigmoid, Matrix.Dot(l0, syn0));
				l2 = VectorN.ApplyCustomOperation(AMath.Sigmoid, VectorN.Product(l1, syn1));

				VectorN l2_error = VectorN.Subtract(yArray, l2);

				// Print out the average error.
				if (i%10000 == 0)
				{
					Console.WriteLine("-------- test " + i  + " --------");
					Console.WriteLine("Error: " + l2_error.Mean(true));
				}

				// inner product
				VectorN l2_delta = VectorN.Product(l2_error, VectorN.ApplyCustomOperation(AMath.SigmoidLinear, l2));

				// scalar product
				double l1_error = l2_delta.Dot(syn1);

				// Apply that scalar to l1 to get l1_delta.
				Matrix l1_delta = Matrix.ApplyCustomOperation(AMath.SigmoidLinear, l1).Scalar(l1_error);

				syn1 = VectorN.Add(syn1, VectorN.Product(l1.Transpose(), l2_delta));
				Matrix weights = Matrix.Dot(l0.Transpose(), l1_delta);
				syn0.Add(weights);

				if (i % 10000 == 0)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("l1 : \n" + l1);
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.WriteLine("l2 delta: " + l2_delta);
					Console.WriteLine("syn1: " + syn1);
				}
			}
		}
	}
}
