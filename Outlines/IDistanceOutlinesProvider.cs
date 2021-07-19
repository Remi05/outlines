using System.Collections.Generic;

namespace Outlines
{
    public interface IDistanceOutlinesProvider
    {
        List<DistanceOutline> GetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement);
    }
}