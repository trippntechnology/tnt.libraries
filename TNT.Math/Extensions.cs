namespace TNT.Math
{
	/// <summary>
	/// Math extentions
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Rounds <paramref name="value"/> to the nearst base value represented by <paramref name="baseValue"/>
		/// </summary>
		/// <param name="value">Value to round</param>
		/// <param name="baseValue">Base value to round to</param>
		/// <returns></returns>
		public static int RoundUpToNearest(this int value, int baseValue)
		{
			var divisorValue = value / baseValue * baseValue;
			var modulusValue = value % baseValue > 0 ? baseValue : 0;

			return divisorValue + modulusValue;
		}

		/// <summary>
		/// Rounds <paramref name="value"/> to the nearst base value represented by <paramref name="baseValue"/>
		/// </summary>
		/// <param name="value">Value to round</param>
		/// <param name="baseValue">Base value to round to</param>
		/// <returns></returns>
		public static double RoundUpToNearest(this double value, double baseValue)
		{
			var divisorValue = (int)value / (int)baseValue * baseValue;
			var modulusValue = value % baseValue > 0 ? baseValue : 0;

			return divisorValue + modulusValue;
		}
	}
}
