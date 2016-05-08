using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix = Overlord.Matrix;

namespace Overlord
{
    /// <summary>
    /// My hand made first neural network experiment.
    /// </summary>
    public class TwoLayer
    {
        //Simple two layer neural network.
        public static void NeuralNetowrk()
        {
            VectorN[] xArray = new VectorN[4]
            {
                new VectorN(new double[] { 0,0,1 }),
                new VectorN(new double[] { 0,1,1 }),
                new VectorN(new double[] { 1,0,1 }),
                new VectorN(new double[] {1,1,1 })
            };

            float[] yArray = new float[4] { 0, 0, 1, 1 };

            Random r = new Random(1); // deterministic seed.

            VectorN syn0 = new VectorN(new double[] {
                (float) (2 * r.NextDouble() - 1),
                (float) (2 * r.NextDouble() - 1),
                (float) (2 * r.NextDouble() - 1) });

            float[] l1 = new float[4];

            for (int i = 0; i < 100000; i++)
            {
                var l0 = xArray;

                l1 = new float[4]
                {
                    (float)AMath.Sigmoid(l0[0].Dot(syn0)),
                    (float)AMath.Sigmoid(l0[1].Dot(syn0)),
                    (float)AMath.Sigmoid(l0[2].Dot(syn0)),
                    (float)AMath.Sigmoid(l0[3].Dot(syn0))
                };

                float[] l1_error = new float[4]
                {
                    yArray[0] - l1[0],
                    yArray[1] - l1[1],
                    yArray[2] - l1[2],
                    yArray[3] - l1[3]
                };

                VectorN l1_delta = new VectorN
                (
                    new double[] {
                    l1_error[0] * (float) AMath.Sigmoid(l1[0], true),
                    l1_error[1] * (float) AMath.Sigmoid(l1[1], true),
                    l1_error[2] * (float) AMath.Sigmoid(l1[2], true),
                    l1_error[3] * (float) AMath.Sigmoid(l1[3], true)
                    }
                );

                // conversion not working
                VectorN weights = new VectorN(new double[] {
                    (new VectorN(new double[] { l0[0][0], l0[1][0], l0[2][0], l0[3][0] })).Dot(l1_delta),
                    (new VectorN(new double[] { l0[0][1], l0[1][1], l0[2][1], l0[3][1] })).Dot(l1_delta),
                    (new VectorN(new double[] { l0[0][2], l0[1][2], l0[2][2], l0[3][2] })).Dot(l1_delta)
                }
                );

                syn0[0] += weights[0];
                syn0[1] += weights[1];
                syn0[2] += weights[2];
            }

			Console.WriteLine("layer 1: " + l1[0] + ", " + l1[1] + ", " + l1[2] + ", " + l1[3] + " ");
		}

		/// <summary>
		/// Creating a toy ANN for demonstration of how inputs work. 2 and 3 layered ANNs are comming soon.
		/// http://iamtrask.github.io/2015/07/12/basic-python-network/
		/// </summary>
		public static void TestToy()
		{
			// Hard data.
			List<VectorN> xArray = new List<VectorN>();

			xArray.Add(new VectorN(new double[] { 0, 0, 1 }));
			xArray.Add(new VectorN(new double[] { 1, 1, 1 }));
			xArray.Add(new VectorN(new double[] { 1, 0, 1 }));
			xArray.Add(new VectorN(new double[] { 0, 1, 1 }));

			float[] yArray = new float[4] { 0, 1, 1, 0 };
			Random r = new Random();

			// generate random inputs
			List<VectorN> syn0 = new List<VectorN>();
			for (int i = 0; i < 4; i++)
			{
				syn0.Add(new VectorN(new double[] {
                    (float)(2 * r.NextDouble() - 1),
                    (float)(2 * r.NextDouble() - 1),
                    (float)(2 * r.NextDouble() - 1)}));
			}

			// generate random outputs
			float[] syn1 = new float[4] {
				(float)(2 * r.NextDouble() - 1),
				(float)(2 * r.NextDouble() - 1),
				(float)(2 * r.NextDouble() - 1),
				(float)(2 * r.NextDouble() - 1)
			};

			// some extreme mathy code
			for (int i = 0; i < 60000; i++)
			{
				// Layer 1
				List<float> l1 = new List<float>();

				// Layer 2
				List<float> l2 = new List<float>();

				float[] l1_delta = new float[4];
				float[] l2_delta = new float[4];

                for (int jx = 0; jx < xArray.Count; jx++)
                {
                    l1.Add((float)xArray[jx].Dot(syn0[jx]));

                    l1[jx] = 1f / (1f + (float)Math.Exp(-l1[jx]));

                    l2.Add((float)(new VectorN(new double[] {
                        l1[jx] * syn1[0],
                        l1[jx] * syn1[1],
                        l1[jx] * syn1[2],
                        l1[jx] * syn1[3] })).Length);

                    l2[jx] = 1f / (1f + (float)Math.Exp(-l2[jx]));

					l2_delta[jx] = (yArray[jx] - l2[jx]) * (l2[jx] * (1 - l2[jx]));
					l1_delta[jx] = (l2[jx] * syn1[jx]) * (l1[jx] * (1 - l1[jx]));

					syn1[jx] += l1[jx] * l2_delta[jx];

					syn0[jx] = new VectorN(new double[] {
                        syn0[jx][0] + xArray[jx][0] * l1_delta[jx],
                        syn0[jx][1] + xArray[jx][1] * l1_delta[jx],
                        syn0[jx][2] + xArray[jx][2] * l1_delta[jx]});
				}

				foreach (var v in syn0)
				{
					Console.WriteLine("\tsyn0: " + v[0] + ", " + v[1] + ", " + v[2]);
				}

				Console.WriteLine();
				Console.Write("\tsyn1: ");

				foreach (var v in syn1)
				{
					Console.Write(" " + v);
				}

				Console.WriteLine();
				Console.WriteLine("--------------");
			}
		}
	}

}
