using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Automation;
using Outlines.Core;

namespace Outlines.Inspection.NetFramework
{
    public class ElementPropertiesProvider : IElementPropertiesProvider
    {
        public ElementProperties GetElementProperties(AutomationElement element)
        {
            if (element?.Current == null)
            {
                return null;
            }

            try
            {
                string name = element.Current.Name;
                ControlType controlType = element.Current.ControlType;
                string controlTypeName = controlType == null ? "" : controlType.ProgrammaticName.Replace("ControlType.", "").Trim();
                string automationId = element.Current.AutomationId ?? "";
                string className = element.Current.ClassName ?? "";
                int nativeWindowHandle = element.Current.NativeWindowHandle;
                Rectangle boundingRect = element.Current.BoundingRectangle.ToDrawingRectangle();

                TextProperties textProperties = GetTextProperties(element);
                ProcessProperties processProperties = GetProcessProperties(element);

                return new AutomationElementProperties() {
                    Name = name,
                    ControlType = controlTypeName,
                    BoundingRect = boundingRect,
                    AutomationId = automationId,
                    ClassName = className,
                    NativeWindowHandle = nativeWindowHandle,
                    ProcessProperties = processProperties,
                    TextProperties = textProperties,
                    Element = element,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private TextProperties GetTextProperties(AutomationElement element)
        {
            try
            {
                if (element == null)
                {
                    return null;
                }

                object textPatternObject;
                if (!element.TryGetCurrentPattern(TextPattern.Pattern, out textPatternObject))
                {
                    return null;
                }

                TextPattern textPattern = (TextPattern)textPatternObject;
                var textProperties = new TextProperties()
                {
                    FontName = textPattern.DocumentRange.GetAttributeValue(TextPattern.FontNameAttribute).ToString(),
                    FontSize = textPattern.DocumentRange.GetAttributeValue(TextPattern.FontSizeAttribute).ToString(),
                    FontWeight = textPattern.DocumentRange.GetAttributeValue(TextPattern.FontWeightAttribute).ToString(),
                    ForegroundColor = textPattern.DocumentRange.GetAttributeValue(TextPattern.ForegroundColorAttribute).ToString(),
                };
                return textProperties;
            }
            catch (Exception)
            {
                return null;
            }
}

        private ProcessProperties GetProcessProperties(AutomationElement element)
        {
            try
            {
                int processId = element.Current.ProcessId;
                var process = Process.GetProcessById(processId);
                string processName = process.ProcessName;
                string mainModuleName = process.MainModule.ModuleName;

                var processProperties = new ProcessProperties()
                {
                    ProcessId = processId,
                    ProcessName = processName
                };

                return processProperties;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
