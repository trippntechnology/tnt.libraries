using System;
using System.Drawing;

namespace TNT.Math
{
	/// <summary>
	/// Represents a mathmatical vector
	/// </summary>
	public class Vector
	{
		#region Members

		/// <summary>
		/// Offest in the X direction
		/// </summary>
		protected double m_X = 0;

		/// <summary>
		/// Offseet in the Y direction
		/// </summary>
		protected double m_Y = 0;

		#endregion

		#region Properties

		/// <summary>
		/// X component of the vector
		/// </summary>
		public double X { get { return m_X; } }

		/// <summary>
		/// Y component of the vector
		/// </summary>
		public double Y { get { return m_Y; } }

		/// <summary>
		/// Magnitude of the vector
		/// </summary>
		public double Magnitude { get { return System.Math.Sqrt(System.Math.Pow(m_X, 2) + System.Math.Pow(m_Y, 2)); } }

		/// <summary>
		/// Unit vector represented by this vector
		/// </summary>
		public Vector Unit
		{
			get
			{
				Vector unit = new Vector(0, 0);

				if (Magnitude > 0)
				{
					unit = new Vector(m_X / Magnitude, m_Y / Magnitude);
				}

				return unit;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a vector with an x and y component
		/// </summary>
		/// <param name="x">X component</param>
		/// <param name="y">Y component</param>
		public Vector(double x, double y)
		{
			m_X = x;
			m_Y = y;
		}

		/// <summary>
		/// Creates a vector from intial point to terminal point
		/// </summary>
		/// <param name="initial">Initial point</param>
		/// <param name="terminal">Terminal point</param>
		public Vector(PointF initial, PointF terminal)
		{
			m_X = terminal.X - initial.X;
			m_Y = terminal.Y - initial.Y;
		}

		/// <summary>
		/// Creates a vector where the terminal point represents the vectors components
		/// </summary>
		/// <param name="terminal"></param>
		public Vector(PointF terminal)
		{
			m_X = terminal.X;
			m_Y = terminal.Y;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="vector">Vector to copy</param>
		public Vector(Vector vector)
		{
			m_X = vector.m_X;
			m_Y = vector.m_Y;
		}

		/// <summary>
		/// Creates a vector given and angle (for 12 o'clock position) and magnitude
		/// </summary>
		/// <param name="angle">Angle (degrees) clockwise (negative counter-clockwise) from a verticle line</param>
		/// <param name="magnitude">Magnitude of the vector</param>
		public Vector(Angle angle, double magnitude)
		{
			m_X = System.Math.Sin(angle.InRadians) * magnitude;
			m_Y = -System.Math.Cos(angle.InRadians) * magnitude;
		}

		#endregion

		/// <summary>
		/// Returns the dot product of this vector and vector
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <returns>Dot product of this vectro and vector</returns>
		public double Dot(Vector vector)
		{
			return m_X * vector.X + m_Y * vector.Y;
		}

		/// <summary>
		/// Gets the angle of this vector from the 12 o'clock position
		/// </summary>
		/// <returns>Angle clockwise from a verticle line</returns>
		public Angle Angle()
		{
			return Angle(new Vector(0, -1), true);
		}

		/// <summary>
		/// Calculates the angle (degrees) between this vector and vector assuming their initial points are the same
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <returns>The angle (degrees) between this vector and vector</returns>
		public Angle Angle(Vector vector)
		{
			return Angle(vector, false);
		}

		/// <summary>
		/// Calculates the angle (degrees) between this vector and vector assuming their initial points are the same
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <param name="reflex">Specifies to return the reflexive angle if greater than 180 degrees. Default: false</param>
		/// <returns>The angle (degrees) between this vector and vector</returns>
		public Angle Angle(Vector vector, bool reflex)
		{
			// Initialize angle
			Angle angle = Math.Angle.Empty;

			if (this.Magnitude > 0 && vector.Magnitude > 0)
			{
				angle = new Angle(System.Math.Acos(this.Unit * vector.Unit / vector.Unit.Magnitude * this.Unit.Magnitude));
			}

			if (reflex)
			{
				double z = Cross(vector);

				if (z > 0)
				{
					angle = new Angle(360 - angle.InDegrees, true);
				}

			}
				
			return angle;
		}

		/// <summary>
		/// Creates a new vector with the same magnitude rotated by angle
		/// </summary>
		/// <param name="angle">Rotation angle</param>
		/// <returns>New vector with the same magnitude rotated by angle</returns>
		public Vector Rotate(Angle angle)
		{
			double x = X * System.Math.Cos(angle.InRadians) - Y * System.Math.Sin(angle.InRadians);
			double y = X * System.Math.Sin(angle.InRadians) + Y * System.Math.Cos(angle.InRadians);
			return new Vector(x, y);
		}

		/// <summary>
		/// Calculates the Z component in the cross product of the two vectors
		/// </summary>
		/// <param name="v">Vector</param>
		/// <returns>Z component in the cross product of the two vectors</returns>
		public double Cross(Vector v)
		{
			return m_X * v.m_Y - m_Y * v.m_X;
		}

		#region Operators

		/// <summary>
		/// Addition operator
		/// </summary>
		/// <param name="v1">Left vector</param>
		/// <param name="v2">Right vector</param>
		/// <returns>The sum of vectors v1 and v2</returns>
		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.m_X + v2.m_X, v1.m_Y + v2.m_Y);
		}

		/// <summary>
		/// Addition operator
		/// </summary>
		/// <param name="v1">Vector</param>
		/// <param name="p1">Origination point</param>
		/// <returns>New point offset from p1 by v1</returns>
		public static PointF operator +(Vector v1, PointF p1)
		{
			return new PointF((float)(p1.X + v1.m_X), (float)(p1.Y + v1.m_Y));
		}

		/// <summary>
		/// Addition operator
		/// </summary>
		/// <param name="p1">Origination point</param>
		/// <param name="v1">Vector</param>
		/// <returns>New point offset from p1 by v1</returns>
		public static PointF operator +(PointF p1, Vector v1)
		{
			return v1 + p1;
		}

		/// <summary>
		/// Subtraction operator
		/// </summary>
		/// <param name="v1">Left vector</param>
		/// <param name="v2">Right vector</param>
		/// <returns>The sum of vectors v2 subtracted from v1</returns>
		public static Vector operator -(Vector v1, Vector v2)
		{
			return new Vector(v1.m_X - v2.m_X, v1.m_Y - v2.m_Y);
		}

		/// <summary>
		/// Subtracts the vector from p1
		/// </summary>
		/// <param name="p1">Initial point</param>
		/// <param name="v1">Vector indicating the change</param>
		/// <returns>New point changed by subtracting the vector's value</returns>
		public static PointF operator -(PointF p1, Vector v1)
		{
			return new PointF((float)(p1.X - v1.m_X), (float)(p1.Y - v1.m_Y));
		}

		/// <summary>
		/// Multiplies the vector components by multiplier
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <param name="multiplier">Multiplier</param>
		/// <returns>New vector with each component modified by the multiplier</returns>
		public static Vector operator *(Vector vector, double multiplier)
		{
			return new Vector(vector.m_X * multiplier, vector.m_Y * multiplier);
		}

		/// <summary>
		/// Multiplies the vector components by multiplier
		/// </summary>
		/// <param name="multiplier">Multiplier</param>
		/// <param name="vector">Vector</param>
		/// <returns>New vector with each component modified by the multiplier</returns>
		public static Vector operator *(double multiplier, Vector vector)
		{
			return vector * multiplier;
		}

		/// <summary>
		/// Dot product of vector v1 and v2
		/// </summary>
		/// <param name="v1">Vector</param>
		/// <param name="v2">Vector</param>
		/// <returns>Dot product of vector v1 and v2</returns>
		public static double operator *(Vector v1, Vector v2)
		{
			return v1.Dot(v2);
		}

		/// <summary>
		/// Divides the vector components by divisor
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <param name="divisor">Divisor</param>
		/// <returns>New vector with each component modified by the divisor</returns>
		public static Vector operator /(Vector vector, double divisor)
		{
			return new Vector(vector.X / divisor, vector.Y / divisor);
		}

		/// <summary>
		/// Casts a Vector type to a PointF type
		/// </summary>
		/// <param name="vector">Vector to cast</param>
		/// <returns>New PointF with same component values as vector</returns>
		public static implicit operator PointF(Vector vector)
		{
			return new PointF((float)vector.m_X, (float)vector.m_Y);
		}

		/// <summary>
		/// Casts a Vector type to a Point type
		/// </summary>
		/// <param name="vector">Vector to cast</param>
		/// <returns>New Point with the same component values as vector</returns>
		public static implicit operator Point(Vector vector)
		{
			return new Point((int)vector.m_X, (int)vector.m_Y);
		}

		#endregion

		#region Overridden

		/// <summary>
		/// Indicates if this vector is equal to vector
		/// </summary>
		/// <param name="obj">Vector to compare</param>
		/// <returns>True if this vector and vector are the same, false otherwise</returns>
		public override bool Equals(object obj)
		{
			Vector vector = obj as Vector;
			return vector != null && m_X == vector.m_X && m_Y == vector.m_Y;
		}

		/// <summary>
		/// GetHashCode
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			int hash = 13;
			hash = hash + m_X.GetHashCode();
			hash = hash + m_Y.GetHashCode();
			return hash;
		}

		/// <summary>
		/// String representation of this vector
		/// </summary>
		/// <returns>String representation of this vector</returns>
		public override string ToString()
		{
			return string.Format("[{0} {1}]", m_X, m_Y);
		}

		#endregion
	}
}
