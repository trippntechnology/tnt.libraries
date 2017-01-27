using ServiceStack.Text;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace TNT.Web
{
	public enum Method
	{
		GET,
		POST,
		PUT,
		DELETE
	}

	/// <summary>
	/// Taken from http://www.codeproject.com/Tips/497123/How-to-make-REST-requests-with-Csharp
	/// </summary>
	public class RESTClient
	{
		public string BaseEndPoint { get; set; }
		public string ContentType { get; set; }

		public RESTClient()
		{
			this.ContentType = "text/json";
		}

		public T Post<T>(string endpoint, object obj)
		{
			string postData = JsonSerializer.SerializeToString(obj);
			string response = Request(GetEndpoint(endpoint), Method.POST.ToString(), postData);
			return JsonSerializer.DeserializeFromString<T>(response);
		}

		public T Get<T>(string endpoint)
		{
			string response = Request(GetEndpoint(endpoint), Method.GET.ToString());
			return JsonSerializer.DeserializeFromString<T>(response);
		}

		protected string Request(string endpoint, string method, string postData = "")
		{
			string postResponse = string.Empty;
			var request = (HttpWebRequest)WebRequest.Create(endpoint);

			request.Method = method;
			request.ContentLength = 0;
			request.ContentType = this.ContentType;

			if (!string.IsNullOrEmpty(postData))
			{
				var encoding = new UTF8Encoding();
				var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(postData);
				request.ContentLength = bytes.Length;

				using (var writeStream = request.GetRequestStream())
				{
					writeStream.Write(bytes, 0, bytes.Length);
				}
			}

			try
			{
				using (var response = (HttpWebResponse)request.GetResponse())
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
						throw new ApplicationException(message);
					}

					// grab the response
					using (var responseStream = response.GetResponseStream())
					{
						if (responseStream != null)
						{
							using (var reader = new StreamReader(responseStream))
							{
								postResponse = reader.ReadToEnd();
							}
						}
					}
				}

			}
			catch (WebException wex)
			{
				throw;
			}

			return postResponse;
		}

		protected string GetEndpoint(params string[] strings)
		{
			return string.Concat(BaseEndPoint, "/", string.Join("/", strings));
		}
	}
}
