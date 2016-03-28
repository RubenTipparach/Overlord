using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix = AnneysEmpire.Matrix;

namespace AnneysEmpire
{
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
	}
}
