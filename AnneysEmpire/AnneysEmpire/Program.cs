using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
	/// <summary>
	/// This program is designed to take in input from spreadsheet or
	/// database system, and generate AI code for Age of Empires 2: Age of Kings.
	/// It uses an Artificial Nueral Network to take in game scores and wieghs each
	/// category against existing code blocks in order to decide which values
	/// allow the AI to manage their base in a more efficient way such that they 
	/// can defeat either another AI or another player.
	/// 
	/// NOTE: As proof of concept, we may need to generate more deterministic models for this,
	///  like simulating a game, and then running it to get better test results.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The main execution method.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			// This is how I like my consoles.
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Clear();

			//To do write unit test for my Matrix/Vector library

			//TwoLayer.NeuralNetowrk();
			//TwoLayerInternal.NeuralNetowrkWithAnnMath();
			ThreeLayerNueralNet.RunNet();

			//using (StreamWriter writer = new StreamWriter("out.txt"))
			//{
			//	Console.SetOut(writer);
			//	Outputtofile();
			//}

			Console.ReadKey();
		}

		/// <summary>
		/// Creating a toy ANN for demonstration of how inputs work. 2 and 3 layered ANNs are comming soon.
		/// http://iamtrask.github.io/2015/07/12/basic-python-network/
		/// </summary>
		static void TestToy()
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
