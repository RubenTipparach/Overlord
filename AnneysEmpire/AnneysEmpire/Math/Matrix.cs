using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{

	public class Matrix
	{
		private Double[,] matrixDouble;

		private int rows;

		private int columns;

		public int Rows
		{
			get
			{
				return rows;
			}
		}

		public int Columns
		{
			get
			{
				return columns;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		public Double this[int i, int j]
		{
			get
			{
				return matrixDouble[i, j];
			}

			set
			{
				matrixDouble[i, j] = value;
			}
		}

		/// <summary>
		/// The matrix constructor.
		/// </summary>
		/// <param name="n">Number of rows.</param>
		/// <param name="m">Number of columns</param>
		/// <param name="input">The matrix in double 2D array format.</param>
		public Matrix(double[,] input)
		{
			matrixDouble = input;

			rows = input.GetLength(0);
			columns = input.GetLength(1);
		}

		/// <summary>
		/// Creates an empty matrix
		/// </summary>
		/// <param name="n">Number of rows.</param>
		/// <param name="m">Number of columns.</param>
		public Matrix(int n, int m)
		{
			matrixDouble = new double[n, m];

			rows = n;
			columns = m;
		}

		/// <summary>
		/// Scales the matrix according to 'x' value.
		/// </summary>
		/// <param name="x">The scalar value</param>
		/// <returns>A reference of self in case you want to do something else with it.</returns>
		public Matrix Scalar(double x)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					matrixDouble[i, j] *= x;
				}
			}

			return this;
		}

		/// <summary>
		/// Transpose operation of a vector.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static Matrix Transpose(Matrix t)
		{
			Matrix u = new Matrix(t.columns, t.rows);

			for (int i = 0; i < t.rows; i++)
			{
				for (int j = 0; j < t.columns; j++)
				{
					u[j, i] = t[i, j];
				}
			}

			return u;
		}

		/// <summary>
		/// Have a matrix multiply another matrix.
		/// </summary>
		/// <param name="b">The second matrix.</param>
		/// <returns>The resulting matrix.</returns>
		public Matrix Dot(Matrix b)
		{
			return Dot(this, b);
		}

		/// <summary>
		/// Add x value to our current matrix
		/// </summary>
		/// <param name="x"></param>
		public void Add(double x)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					matrixDouble[i, j] += x;
				}
			}
		}

		/// <summary>
		/// Add one matrix to another.
		/// </summary>
		/// <param name="b"></param>
		public void Add(Matrix b)
		{
			if (this.columns != b.columns || this.rows != b.rows)
			{
				throw new InvalidOperationException("Current Columns/rows must be EQUAL to Columns/rows in 'b'.");
			}

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					matrixDouble[i, j] += b[i, j];
				}
			}
		}

		/// <summary>
		/// Matrix multiplaction between two matrix. This is probably the recommended method if you dont wanna be an asshat.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Matrix Dot(Matrix a, Matrix b)
		{
			if (a.columns != b.Rows)
			{
				throw new InvalidOperationException("Columns in 'a' must be EQUAL to Rows in 'b'.");
			}

			Matrix c = new Matrix(a.rows, b.columns);

			for (int i = 0; i < c.rows; i++)
			{
				for (int j = 0; j < c.columns; j++)
				{
					double result = 0;

					for (int k = 0; k < a.columns; k++)
					{
						// Iteratre through each column of A, multiplying it by each column of B.
						//if ((i + j) % 2 == 0)
						//{
						//	result += a[i, k] * b[k, i]; // even cells
						//}
						//else
						//{
						//	result += a[i, k] * b[k, (b.columns - 1) - i];//odd cells
						//}
						result += a[i, k] * b[k, j]; // even cells
					}

					// Console.WriteLine("---" + result);
					c[i, j] = result;
				}
			}

			return c;
		}

		/// <summary>
		/// Uses a callback function for each of the elements in the Matrix.
		/// </summary>
		/// <param name="adf">The function</param>
		public static Matrix ApplyCustomOperation(ApplyDoubleFunction adf, Matrix a)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);

			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; i++)
				{
					result[i, j] = adf(a[i, j]);
				}
			}

			return result;
		}

		/// <summary>
		/// Prints out the string representation or whatever.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string result = "";

			int longestStr = 1;

			// fun format stuff, find the largest numerical length, scale all this to that thing...
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					if (longestStr < matrixDouble[i, j].ToString().Length)
					{
						longestStr = matrixDouble[i, j].ToString().Length;
					}
				}
			}

			// Append watever element is found.
			for (int i = 0; i < rows; i++)
			{
				result += "[ ";

				for (int j = 0; j < columns; j++)
				{
					int deltaStrLength = longestStr - matrixDouble[i, j].ToString().Length;
					result += matrixDouble[i, j] + " " + "".PadRight(deltaStrLength, ' ');
				}

				result += "]\n";
			}

			return result;
		}
	}
}
