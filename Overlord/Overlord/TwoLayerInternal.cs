using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord
{
	/// <summary>
	/// Same as the standard Two layer test neural network,
	/// except it is using the proprietary ANN math libary.
	/// </summary>
	public class TwoLayerInternal
	{
		// creating API to handle matrix multiplication, maybe switch over to GPU subroutines
		public static void NeuralNetowrkWithAnnMath()
		{
			//Initialize example matrices to teach neural net.
			Matrix xArray = new Matrix(
				new double[4, 3] {
                    // columns then rows.... ugh stupid c# 
					{0,0,1},
					{0,1,1},
					{1,0,1},
					{1,1,1}});

			VectorN yArray = new VectorN (new double[4] { 0, 0, 1, 1 });

			// fill wieghted array with random weights,
			// and teach it to conform
			// with the approximate statistics
			Random r = new Random(1);
			VectorN syn0 = new VectorN (new double[3]{
				2 * r.NextDouble() - 1,
				2 * r.NextDouble() - 1,
				2 * r.NextDouble() - 1});

			VectorN l1 = new VectorN(4);

			// Begin learning iteration loop.
			for ( int i = 0; i < 100000; i++)
			{
				Matrix l0 = xArray;
				l1 = VectorN.Product(l0, syn0);
				l1 = VectorN.ApplyCustomOperation(AMath.Sigmoid, l1);

				VectorN l1_error = VectorN.Subtract(yArray, l1);
				VectorN l1_delta = new VectorN(l1.Size);

				l1_delta = VectorN.Product(l1_error, 
					VectorN.ApplyCustomOperation(AMath.SigmoidLinear, l1));

				VectorN weights = VectorN.Product(l0.Transpose(), l1_delta);
				syn0 = VectorN.Add(syn0, weights);
			}

			Console.WriteLine("layer 1: \n" + l1);
		}
	}
}
