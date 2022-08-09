using System;
using DemoAnalyzer.Rules;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;


namespace DemoAnalyzer.Helpers
{
	public static class DiagnosticDescriptorBuilder
    {
        private static readonly RuleMetadata metadata = new RuleMetadata();
        public static DiagnosticDescriptor GetDescriptor(string diagnosticId, string messageFormat, bool showInIde = true)
        {
			RuleDescription ruleDescription = GetRuleDescription(diagnosticId);

			return new DiagnosticDescriptor(
				diagnosticId,
				ruleDescription.Title,
                messageFormat,
				ruleDescription.Category,
				ParseSeverity(ruleDescription.DefaultSeverity).ToDiagnosticSeverity(showInIde),
				ruleDescription.DefaultActive,
				helpLinkUri: GetHelpLink(diagnosticId),
				description: GetHtmlDescription(diagnosticId),
				customTags: showInIde ? ruleDescription.Tags : new string[] { });

		}

		public static RuleDescription GetRuleDescription(string diagnosticId)
		{
			var data= JsonConvert.DeserializeObject<RuleDescription>(metadata[$"{diagnosticId}json"]);
            return data;
		}

		public static string GetHtmlDescription(string diagnosticId)
        {
            var data= metadata[$"{diagnosticId}html"];
            return data;
        }

        private static Severity ParseSeverity(string severity)
        {
			if (Enum.TryParse(severity, out Severity result))
			{
				return result;
			}

			throw new NotSupportedException($"Not supported severity");
        }

        private static DiagnosticSeverity ToDiagnosticSeverity(this Severity severity,
            bool showInIde)
        {
            switch (severity)
            {
                case Severity.Info:
                    return !showInIde ? DiagnosticSeverity.Hidden : DiagnosticSeverity.Info;
                case Severity.Minor:
                    return !showInIde ? DiagnosticSeverity.Hidden : DiagnosticSeverity.Warning;
                case Severity.Major:
                    return !showInIde ? DiagnosticSeverity.Hidden : DiagnosticSeverity.Error;
                case Severity.Critical:
                case Severity.Blocker:
                    return DiagnosticSeverity.Warning;
                default:
                    throw new NotSupportedException();
            }
        }

        private static string GetHelpLink(string diagnosticId)
        {
            var data= new Uri($"/RuleMetadata/Metadata/{diagnosticId}.html", UriKind.Relative).ToString();
            return data;    
        }
    }
}
