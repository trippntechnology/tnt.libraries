using System.IO;
using System.Net;
using System.Xml.Linq;

namespace TNT.WebRelay
{
	public class WebRelay
	{
		static int OFF = 0;
		static int ON = 1;
		static int PULSE = 2;

		public string URI { get; set; }

		public string Parameter { get; set; }

		public int PulseTime { get; set; }

		public WebRelay()
			:this(string.Empty, string.Empty, 0)
		{

		}

		public WebRelay(string uri, string parameter, int pulseTime = 0)
		{
			this.URI = uri;
			this.Parameter = parameter;
			this.PulseTime = 0;
		}

		public virtual XDocument On()
		{
			return SendRequest(string.Concat(this.URI, "?", this.Parameter, "=", ON));
		}

		public virtual XDocument Off()
		{
			return SendRequest(string.Concat(this.URI, "?", this.Parameter, "=", OFF));
		}

		public virtual XDocument Pulse()
		{
			if (this.PulseTime > 0)
			{
				return SendRequest(string.Concat(this.URI, "?", this.Parameter, "=", PULSE, "&pulseTime=", this.PulseTime));
			}
			else
			{
				return SendRequest(string.Concat(this.URI, "?", this.Parameter, "=", PULSE));
			}
		}

		public virtual XDocument GetState()
		{
			return SendRequest(this.URI);
		}

		protected virtual XDocument SendRequest(string request)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(request);

			Stream responseStream = webRequest.GetResponse().GetResponseStream();
			XDocument responseXML = XDocument.Load(responseStream);

			return responseXML;
		}

	}
}
