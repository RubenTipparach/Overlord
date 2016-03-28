using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
	/// <summary>
	/// Math library for Artificial Nueral Network.
	/// </summary>
	public class AMath
	{
		/// <summary>
		/// Standard sigmoid function, -1 to 1.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>Result number.</returns>
		public static double Sigmoid(double input)
		{
			return 1.0 / (1.0 + Math.Exp(-input));
		}

		/// <summary>
		/// Sigmoid with derivitive flag. (linear when: derive = false)
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="deriv">if set to <c>true</c> [deriv].</param>
		/// <returns>Result number.</returns>
		public static double SigmoidLinear(double input)
		{
			return input * (1 - input);
		}

		/// <summary>
		/// Sigmoid with derivitive flag. (linear when: derive = false)
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="deriv">if set to <c>true</c> [deriv].</param>
		/// <returns>Result number.</returns>
		public static double Sigmoid(double input, bool deriv)
		{
			if (deriv)
			{
				return input * (1 - input);
			}
			else
			{
				return 1.0 / (1.0 + Math.Exp(-input));
			}
		}
	}
}
