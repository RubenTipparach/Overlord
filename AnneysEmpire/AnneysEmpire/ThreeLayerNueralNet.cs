using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
	public class ThreeLayerNueralNet
	{
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

			VectorN yArray = new VectorN(new double[4] { 0, 0, 1, 1 });

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
				for (int i = 0; i < syn0.Rows; j++ )
				{
					syn0[i, j] = 2*r.NextDouble() - 1;
				}

				syn1[j] = 2*r.NextDouble() - 1;
			}

			// begin firing neurons.
			for(int i = 0; i < 60000; i++)
			{
				l0 = xArray;
				l1 = Matrix.ApplyCustomOperation(AMath.Sigmoid, l0.Dot(syn0));
				l2 = VectorN.ApplyCustomOperation(AMath.Sigmoid, VectorN.Product(l1, syn1));

				VectorN l2_error = VectorN.Subtract(yArray, l2);


            }
        }
	}
}
