using System;
using TNT.Utilities;

namespace MutexWrapperTest
{
	class Program
	{
		static void Main(string[] args)
		{
			MutexWrapper mutex = new MutexWrapper("MutexWrapperTest");

			try
			{
				mutex.CriticalSection(() =>
				{
					Console.WriteLine("Press key to close");
					Console.ReadKey();
					throw new Exception("Thrown from within critical section.");
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Press key to close");
				Console.ReadKey();
			}
		}
	}
}
