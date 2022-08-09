
$sb = New-Object System.Text.StringBuilder;

$sb.AppendLine("using System.Collections.Generic;");
$sb.AppendLine("");
$sb.AppendLine("namespace DemoAnalyzer.Rules");
$sb.AppendLine("{");
$sb.AppendLine("`tpublic class RuleMetadata");
$sb.AppendLine("`t{");
$sb.AppendLine("`t`tpublic string this[string key] => data[key];");
$sb.AppendLine("`t`tprivate readonly Dictionary<string, string> data = new Dictionary<string, string>();");
$sb.AppendLine("");
$sb.AppendLine("`t`tpublic RuleMetadata()");
$sb.AppendLine("`t`t{");

gci .\MetaData | foreach { "`t`t`tdata.Add(`"" + $_.ToString().Replace('.', '') + "`", `"" + (((Get-Content $_.FullName).Replace('\', '\\') -join '\n').Replace('"', '\"')) + "`");`n" } | foreach { $sb.Append($_) }

$sb.AppendLine("`t`t}");
$sb.AppendLine("`t}");
$sb.AppendLine('}');

$sb.ToString() | Out-File .\RuleMetadata.cs -Encoding utf8
