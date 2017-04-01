using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNT.Plugin.Manager;

namespace Plugins
{
	public class ApplicationData : IApplicationData
	{
		public ApplicationData(string content)
		{
			this.Content = content;
		}
		public string Content { get; set; }
	}
}
