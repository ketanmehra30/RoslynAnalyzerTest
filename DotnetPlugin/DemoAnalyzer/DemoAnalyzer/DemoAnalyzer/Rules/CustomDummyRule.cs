using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using DemoAnalyzer.Helpers;
using System.Collections.Immutable;

namespace DemoAnalyzer.Rules
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class CustomDummyRule : DiagnosticAnalyzer
	{

		public const string DiagnosticId = "SF001";
		private static readonly LocalizableString MessageFormat =
		   new LocalizableResourceString(nameof(Resources.CustomDummyRuleMessage),
			   Resources.ResourceManager, typeof(Resources));
		private static readonly DiagnosticDescriptor descriptor = DiagnosticDescriptorBuilder.GetDescriptor(DiagnosticId, MessageFormat.ToString(), true); 

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(descriptor);

		public override void Initialize(AnalysisContext context)
		{
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
			context.EnableConcurrentExecution();

			context.RegisterSymbolAction(
				Analyze,
				SymbolKind.NamedType);
		}

		private void Analyze(SymbolAnalysisContext context)
		{
			var namedtypedsymbol = (INamedTypeSymbol)context.Symbol;
			if (namedtypedsymbol.Name.Contains("Ketan"))
			{
				var diagnostic = Diagnostic.Create(descriptor, namedtypedsymbol.Locations[0], namedtypedsymbol.Name);
				context.ReportDiagnostic(diagnostic);
			}

		}
	}
}
