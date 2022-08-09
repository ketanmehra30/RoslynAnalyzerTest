using System.Collections.Generic;

namespace DemoAnalyzer.Rules
{
	public class RuleMetadata
	{
		public string this[string key] => data[key];
		private readonly Dictionary<string, string> data = new Dictionary<string, string>();

		public RuleMetadata()
		{
			data.Add("SF001html", "<h1>Custom Dummy Rule</h1>\n<p>This is a Test HTML File</p>");
			data.Add("SF001json", "{\n	\"title\": \"Custom Dummy Rule Invoked!\",\n	\"category\": \"C#-General\",\n	\"tags\": [ \"TestTag\" ],\n	\"defaultSeverity\": \"Major\",\n	\"defaultActive\": \"true\"\n}");
		}
	}
}

