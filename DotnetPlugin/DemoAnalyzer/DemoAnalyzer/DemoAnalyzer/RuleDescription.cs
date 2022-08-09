namespace DemoAnalyzer
{
	public class RuleDescription
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string DefaultSeverity { get; set; }
        public bool DefaultActive { get; set; }
    }
}
