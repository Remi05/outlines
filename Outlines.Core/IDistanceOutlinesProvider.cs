using System.Collections.Generic;

namespace Outlines.Core
{
    public interface IDistanceOutlinesProvider
    {
        List<DistanceOutline> GetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement);
    }
}