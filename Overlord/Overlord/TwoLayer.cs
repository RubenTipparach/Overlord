using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix = Overlord.Matrix;

namespace Overlord
{
	/// <summary>
	/// Blah blah blah.
	/// </summary>
	public class TwoLayer
	{
		//Simple two layer neural network.
		public static void NeuralNetowrk()
		{
			Vector3[] xArray = new Vector3[4]
			{
				new Vector3(0,0,1),
				new Vector3(0,1,1),
				new Vector3(1,0,1),
				new Vector3(1,1,1)
			};

			float[] yArray = new float[4] { 0, 0, 1, 1};

			Random r = new Random(1); // deterministic seed.

			Vector3 syn0 = new Vector3(
				(float) (2 * r.NextDouble() - 1),
				(float) (2 * r.NextDouble() - 1),
				(float) (2 * r.NextDouble() - 1));

			float[] l1 = new float[4];

			for (int i = 0; i < 100000; i++)
			{
				var l0 = xArray;

				l1 = new float[4]
				{
					(float)AMath.Sigmoid(Vector3.Dot(l0[0], syn0)),
					(float)AMath.Sigmoid(Vector3.Dot(l0[1], syn0)),
					(float)AMath.Sigmoid(Vector3.Dot(l0[2], syn0)),
					(float)AMath.Sigmoid(Vector3.Dot(l0[3], syn0))
				};

				float[] l1_error = new float[4]
				{
					yArray[0] - l1[0],
					yArray[1] - l1[1],
					yArray[2] - l1[2],
					yArray[3] - l1[3]
				};

				Vector4 l1_delta = new Vector4
				(
					l1_error[0] * (float) AMath.Sigmoid(l1[0], true),
					l1_error[1] * (float) AMath.Sigmoid(l1[1], true),
					l1_error[2] * (float) AMath.Sigmoid(l1[2], true),
					l1_error[3] * (float) AMath.Sigmoid(l1[3], true)
				);

				Vector3 weights = new Vector3(
					Vector4.Dot(new Vector4(l0[0].X, l0[1].X, l0[2].X, l0[3].X), l1_delta),
					Vector4.Dot(new Vector4(l0[0].Y, l0[1].Y, l0[2].Y, l0[3].Y), l1_delta),
					Vector4.Dot(new Vector4(l0[0].Z, l0[1].Z, l0[2].Z, l0[3].Z), l1_delta)
				);

				syn0.X += weights.X;
				syn0.Y += weights.Y;
				syn0.Z += weights.Z;
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
			List<Vector3> xArray = new List<Vector3>();

			xArray.Add(new Vector3(0, 0, 1));
			xArray.Add(new Vector3(1, 1, 1));
			xArray.Add(new Vector3(1, 0, 1));
			xArray.Add(new Vector3(0, 1, 1));

			float[] yArray = new float[4] { 0, 1, 1, 0 };
			Random r = new Random();

			// generate random inputs
			List<Vector3> syn0 = new List<Vector3>();
			for (int i = 0; i < 4; i++)
			{
				syn0.Add(new Vector3(
					(float)(2 * r.NextDouble() - 1),
					(float)(2 * r.NextDouble() - 1),
					(float)(2 * r.NextDouble() - 1)));
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
					l1.Add(Vector3.Dot(xArray[jx], syn0[jx]));

					l1[jx] = 1f / (1f + (float)Math.Exp(-l1[jx]));

					l2.Add((new Vector4(
						l1[jx] * syn1[0],
						l1[jx] * syn1[1],
						l1[jx] * syn1[2],
						l1[jx] * syn1[3])).Length());

					l2[jx] = 1f / (1f + (float)Math.Exp(-l2[jx]));

					l2_delta[jx] = (yArray[jx] - l2[jx]) * (l2[jx] * (1 - l2[jx]));
					l1_delta[jx] = (l2[jx] * syn1[jx]) * (l1[jx] * (1 - l1[jx]));

					syn1[jx] += l1[jx] * l2_delta[jx];

					syn0[jx] = new Vector3(
						syn0[jx].X + xArray[jx].X * l1_delta[jx],
						syn0[jx].Y + xArray[jx].Y * l1_delta[jx],
						syn0[jx].Z + xArray[jx].Z * l1_delta[jx]);
				}

				foreach (var v in syn0)
				{
					Console.WriteLine("\tsyn0: " + v.X + ", " + v.Y + ", " + v.Z);
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
