using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNT.Math
{
	/// <summary>
	/// Represents an angle
	/// </summary>
	public class Angle
	{
		/// <summary>
		/// Radian representation of the angle
		/// </summary>
		protected double m_Radians;

		/// <summary>
		/// Returns the angle in radians
		/// </summary>
		public double InRadians { get { return m_Radians; } }

		/// <summary>
		/// Returns an empty Angle (0 degrees)
		/// </summary>
		public static Angle Empty { get { return new Angle(0); } }

		/// <summary>
		/// Returns the angle in degrees
		/// </summary>
		public double InDegrees { get { return m_Radians * 180 / System.Math.PI; } }

		#region Constructors

		/// <summary>
		/// Creates an angle where angle is in radians
		/// </summary>
		/// <param name="angle">The angle</param>
		public Angle(double angle)
		{
			m_Radians = angle;
		}

		/// <summary>
		/// Creates an angle interpreting angle with the value of isDegrees
		/// </summary>
		/// <param name="angle">The angle</param>
		/// <param name="isDegrees">Indicates whether the angle is in degrees or radians (default: false)</param>
		public Angle(double angle, bool isDegrees)
		{
			if (isDegrees)
			{
				m_Radians = angle * System.Math.PI / 180;
			}
			else
			{
				m_Radians = angle;
			}
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="angle">Angle object to copy</param>
		public Angle(Angle angle)
		{
			m_Radians = angle.m_Radians;
		}

		#endregion

		#region Operators

		/// <summary>
		/// Adds two angles together
		/// </summary>
		/// <param name="a1">Angle 1</param>
		/// <param name="a2">Angle 2</param>
		/// <returns>Creates an angle that represents the sum of a1 and a2</returns>
		public static Angle operator +(Angle a1, Angle a2)
		{
			return new Angle(a1.InRadians + a2.InRadians);
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Creates a string (in degrees)
		/// </summary>
		/// <returns>The angle as a string in degrees</returns>
		public override string ToString()
		{
			return InDegrees.ToString();
		}

		/// <summary>
		/// Compares this angle with another angle
		/// </summary>
		/// <param name="obj">Angle to compare</param>
		/// <returns>True if the angles are equal, false otherwise</returns>
		public override bool Equals(object obj)
		{
			Angle angle = obj as Angle;

			return (angle != null && angle.m_Radians == m_Radians);
		}

		/// <summary>
		/// GetHashCode
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#endregion
	}
}
