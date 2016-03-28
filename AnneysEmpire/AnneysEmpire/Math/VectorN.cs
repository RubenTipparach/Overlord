using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
	/// <summary>
	/// A vector of N size class. To be used for storing arrays of data and doing math operations with them.
	/// TODO: optimize into struct so we can avoid side effects from manipulation via reference.
	/// </summary>
	public class VectorN
	{
		private Double[] _vectorArray;

		private int _vSize;

		public int Size
		{
			get { return _vSize; }
		}

		/// <summary>
		/// Initializes and empty vector.
		/// </summary>
		/// <param name="vSize"></param>
		public VectorN(int vSize)
		{
			_vectorArray = new Double[vSize];
			_vSize = vSize;
		}

		/// <summary>
		/// Initializes a vector according to a double array.
		/// </summary>
		/// <param name="vectorArray"></param>
		public VectorN(double[] vectorArray)
		{
			_vectorArray = vectorArray;
			_vSize = vectorArray.Length;
		}

		public Double this[int i]
		{
			get
			{
				return _vectorArray[i];
			}
			set
			{
				_vectorArray[i] = value;
			}
		}

		/// <summary>
		/// The length of one vector projected onto another.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public double Dot(VectorN vector)
		{
			double result = 0;

			if (vector.Size != _vSize)
			{
				throw new InvalidOperationException("Current vector size must be EQUAL to size in 'b'.");
			}

			for (int i = 0; i < _vSize; i++)
			{
				result += _vectorArray[i] + vector[i];
			}

			return result;
		}

		/// <summary>
		/// Adds two vectors of the same size together. "a + b"
		/// </summary>
		/// <param name="vector">The other vector</param>
		public static VectorN Add(VectorN a, VectorN b)
		{
			if (a.Size!= b.Size)
			{
				throw new InvalidOperationException("Current vector size must be EQUAL to size in 'b'.");
			}

			VectorN result = new VectorN(a.Size);
			
			for(int i = 0; i < a.Size; i++)
			{
				result[i] = a[i] + b[i];
			}

			return result;
		}

		/// <summary>
		/// Subtracts vecor b from a. "a - b"
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static VectorN Subtract(VectorN a, VectorN b)
		{
			if (a.Size!= b.Size)
			{
				throw new InvalidOperationException("Current vector size must be EQUAL to size in 'b'.");
			}

			VectorN result = new VectorN(a.Size);

			for (int i = 0; i < a.Size; i++)
			{
				result[i] = a[i] - b[i];
			}

			return result;
		}

		/// <summary>
		/// Uses a callback function for each of the elements in the vector.
		/// </summary>
		/// <param name="adf"></param>
		public static VectorN ApplyCustomOperation(ApplyDoubleFunction adf, VectorN a)
		{
			VectorN result = new VectorN(a.Size);
			for (int i = 0; i < a.Size; i++)
			{
				result[i] = adf(a[i]);
			}

			return result;
		}

		/// <summary>
		/// Not sure if this is cross product or just multiplication?
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static VectorN Product(VectorN a, VectorN b)
		{
			if (a.Size!= b.Size)
			{
				throw new InvalidOperationException("Current vector size must be EQUAL to size in 'b'.");
			}

			VectorN result = new VectorN(a.Size);

			for (int i = 0; i < a.Size; i++)
			{
				result[i] = a[i] * b[i];
			}

			return result;
		}

		//public void Product(Matrix m)
		//{
		//	VectorN result = Product(m, this);
		//	_vectorArray = result._vectorArray;
		//	_vSize = result._vSize;
		//}


		public static VectorN Product(Matrix m, VectorN v)
		{
			// special private hack ;)
			//_vectorArray = Product(m, this)._vectorArray;
			Matrix b = new Matrix(v._vSize, 1);

			for (int i = 0; i < v._vSize; i++)
			{
				b[i, 0] = v._vectorArray[i];
			}

			b = Matrix.Dot(m, b);

			VectorN nv = new VectorN(b.Rows);

			for (int i = 0; i < b.Rows; i++)
			{
				nv._vectorArray[i] = b[i, 0];
			}

			//return new vector here.
			return nv;
		}

		// I'm not sure if this is correct.
		//public static VectorN Product(Matrix m, VectorN v)
		//{
		//	if (m.Columns != v.Size)
		//	{
		//		throw new InvalidOperationException("Columns in 'm' must be EQUAL to Rows in 'v'.");
		//	}

		//	VectorN result = new VectorN(v.Size);

		//	for (int i = 0; i < m.Rows; i++)
		//	{
		//		for (int j = 0; j < v.Size; j++)
		//		{
		//			result[j] += v[j] * m[i, j];
		//			// Console.WriteLine("*  " + result[j]);
		//		}
		//	}

		//	return result;
		//}

		/// <summary>
		/// Outputs this vector result into a string or whatever.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string result = "";
			int longestStr = 1;

			// fun format stuff, find the largest numerical length, scale all this to that thing...
			for (int i = 0; i < _vSize; i++)
			{
				var currentLength = _vectorArray[i].ToString().Length;

				if (longestStr < currentLength)
				{
					longestStr = currentLength;
				}
			}

			// Append watever element is found.
			for (int i = 0; i < _vSize; i++)
			{
				result += "[ ";

				int deltaStrLength = longestStr - _vectorArray[i].ToString().Length;
				result += _vectorArray[i] + " " + "".PadRight(deltaStrLength, ' ');

				result += "]\n";
			}

			return result;
		}
	}

	public delegate double ApplyDoubleFunction(double x);

	public delegate double ApplyDoubleFunctionWFlag(double x, bool b);
}
