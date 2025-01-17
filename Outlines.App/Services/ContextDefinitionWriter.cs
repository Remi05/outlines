using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Outlines.Core;

namespace Outlines.App.Services;

internal class ContextDefinitionWriter
{
    private const string ContextIdentifier = "context";
    private static readonly HashSet<string> IgnoredControls = new() { "text", "image", "header" };

    private uint UniqueCounter = 0;

    public void ExportContextDefinition(Snapshot snapshot)
    {
        string contextDefinition = ConvertToContextDefinition(snapshot);

        var dialog = new Microsoft.Win32.SaveFileDialog();
        dialog.FileName = $"{snapshot.UITree.ElementProperties.Name}-ContextDefinition";
        dialog.DefaultExt = ".uial";

        if (dialog.ShowDialog().Value)
        {
            File.WriteAllText(dialog.FileName, contextDefinition);
        }
    }

    public string ConvertToContextDefinition(Snapshot snapshot)
    {
        StringBuilder sb = new();
        BuildContextDefinition(snapshot.UITree, sb, 0);
        return sb.ToString();
    }

    private void BuildContextDefinition(IUITreeNode uiTreeNode, StringBuilder sb, int identationLevel)
    {
        sb.Append(new string(' ', 4 * identationLevel));
        sb.AppendLine(GetContextHeader(uiTreeNode));

        foreach (IUITreeNode child in uiTreeNode.GetAndMonitorChildren())
        {
            if (!IgnoredControls.Contains(child.ElementProperties.ControlType))
            {
                BuildContextDefinition(child, sb, identationLevel + 1);
                sb.AppendLine();
            }
        }
    }

    private string GetContextHeader(IUITreeNode uiTreeNode)
    {
        return $"{ContextIdentifier} {GetContextName(uiTreeNode)} {GetConditionString(uiTreeNode)}:";
    }

    private string GetContextName(IUITreeNode uiTreeNode)
    {
        if (!string.IsNullOrEmpty(uiTreeNode.ElementProperties.AutomationId))
        {
            return uiTreeNode.ElementProperties.AutomationId;
        }
        if (!string.IsNullOrEmpty(uiTreeNode.ElementProperties.Name))
        {
            return MergeAndCapitalize(uiTreeNode.ElementProperties.Name);
        }
        return $"{uiTreeNode.ElementProperties.ControlType}{UniqueCounter++}";
    }

    private string GetConditionString(IUITreeNode uiTreeNode)
    {
        List<string> conditionParts = new(); 

        if (!string.IsNullOrWhiteSpace(uiTreeNode.ElementProperties.AutomationId))
        {
            conditionParts.Add($"AutomationId=\"{uiTreeNode.ElementProperties.AutomationId}\"");
        }
        else if (!string.IsNullOrWhiteSpace(uiTreeNode.ElementProperties.Name))
        {
            // We only add the Name property if AutomationId is not provided since Name can vary a lot.
            conditionParts.Add($"Name=\"{uiTreeNode.ElementProperties.Name}\"");
        }

        if (!string.IsNullOrWhiteSpace(uiTreeNode.ElementProperties.ControlType))
        {
            string controlType = MergeAndCapitalize(uiTreeNode.ElementProperties.ControlType);
            conditionParts.Add($"ControlType=\"{controlType}\"");
        }

        if (!string.IsNullOrWhiteSpace(uiTreeNode.ElementProperties.ClassName))
        {
            conditionParts.Add($"ClassName=\"{uiTreeNode.ElementProperties.ClassName}\"");
        }
        return $"[{string.Join(", ", conditionParts)}]";
    }

    private string MergeAndCapitalize(string originalString)
    {
        // Merge the words and capitalize the first letter of each word.
        string[] parts = originalString.Trim().Split(" ");
        return string.Join("", parts.Select(part => part.Substring(0, 1).ToUpper() + part.Substring(1)));
    }
}
