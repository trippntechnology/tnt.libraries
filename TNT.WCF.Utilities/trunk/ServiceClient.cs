using System;
using System.ServiceModel;

namespace TNT.WCF.Utilities
{
	/// <summary>
	/// Represents a client for a TNT WCF service
	/// </summary>
	/// <typeparam name="T">Service contract interface</typeparam>
	public class ServiceClient<T>
	{
		private ChannelFactory<T> m_factory;

		#region Constructors

		/// <summary>
		/// Creates a new service client using an existing channel factory
		/// </summary>
		/// <param name="channelFactory">Channel factory</param>
		public ServiceClient(ChannelFactory<T> channelFactory)
		{
			m_factory = channelFactory;
		}
	
		/// <summary>
		/// Initializes a service client with the endpoint specified
		/// </summary>
		/// <param name="endpointConfigurationName">Endpoint configuration name</param>
		public ServiceClient(string endpointConfigurationName)
			: this(new ChannelFactory<T>(endpointConfigurationName))
		{
		}
		
		/// <summary>
		/// Initializes a service client with the endpoint and remote address
		/// </summary>
		/// <param name="endpointConfigurationName">Endpoint configuration name</param>
		/// <param name="remoteAddress">Remote address</param>
		public ServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: this(new ChannelFactory<T>(endpointConfigurationName, remoteAddress))
		{
		}

		#endregion

		#region Execute 

		/// <summary>
		/// Executes a delegate using a new channel and returns the result
		/// </summary>
		/// <typeparam name="R">Result type</typeparam>
		/// <param name="deleg">Delegate that takes a channel as a parameter and returns a value</param>
		/// <returns>The result of the execution of the delegate</returns>
		public R Execute<R>(Func<T, R> deleg)
		{
			var chan = m_factory.CreateChannel();
			var ico = chan as ICommunicationObject;
			R ret;
			try
			{
				ico.Open();
				ret = deleg(chan);
			}
			finally
			{
				if (ico.State == CommunicationState.Opened)
				{
					ico.Close();
				}
				else if (ico.State == CommunicationState.Faulted)
				{
					ico.Abort();
				}
			}
			return ret;
		}
		
		/// <summary>
		/// Executes a delegate using a new channel
		/// </summary>
		/// <param name="deleg">Delegate that takes a channel as a parameter</param>
		public void Execute(Action<T> deleg)
		{
			Execute(c =>
			{
				deleg(c);
				return true;
			});
		}

		#endregion
	}
}