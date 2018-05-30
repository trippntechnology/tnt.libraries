using System;
using System.Threading;

namespace TNT.Utilities
{
	/// <summary>
	/// Wraps System.Threading.Mutex
	/// </summary>
	public class MutexWrapper
	{
		/// <summary>
		/// Name associated with the mutex
		/// </summary>
		protected string Name = string.Empty;

		/// <summary>
		/// Wrapped <see cref="System.Threading.Mutex"/>
		/// </summary>
		private Mutex _Mutex = null;

		/// <summary>
		/// Initializes a mutex with <paramref name="name"/>
		/// </summary>
		/// <param name="name">Name associated with the mutex</param>
		public MutexWrapper(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Opens or creates mutex that is used to protect <paramref name="criticalSection"/>
		/// </summary>
		/// <param name="criticalSection">Code that is protected by mutex</param>
		public void CriticalSection(Action criticalSection)
		{
			bool doesNotExist = false;

			// The value of this variable is set by the mutex constructor. It is true if the named system mutex was 
			// created, and false if the named mutex already existed. 
			bool mutexWasCreated = false;

			// Attempt to open the named mutex. 
			try
			{
				// Open the mutex with (MutexRights.Synchronize | 
				// MutexRights.Modify), to enter and release the 
				// named mutex. 
				//
				_Mutex = System.Threading.Mutex.OpenExisting(Name);
			}
			catch (WaitHandleCannotBeOpenedException)
			{
				doesNotExist = true;
			}

			// There are two cases: (1) The mutex does not exist. (2) The mutex exists and the user has access. 
			if (doesNotExist)
			{
				// The mutex does not exist, so create it. 
				_Mutex = new System.Threading.Mutex(true, Name, out mutexWasCreated);

				// If the named system mutex was created, it can be 
				// used by the current instance of this program, even  
				// though the current user is denied access. The current 
				// program owns the mutex. Otherwise, exit the program. 
				//  
				if (!mutexWasCreated)
				{
					throw new Exception(string.Format("Unable to create the mutex, {0}", this.Name));
				}
			}

			try
			{
				// If this program created the mutex, it already owns the mutex. 
				if (!mutexWasCreated)
				{
					// Enter the mutex, and hold it until the program exits. 
					_Mutex.WaitOne();
				}

				// Do critical section
				criticalSection();
			}
			catch 
			{
				throw;
			}
			finally
			{
				// Release the mutex
				_Mutex.ReleaseMutex();
			}
		}
	}
}
