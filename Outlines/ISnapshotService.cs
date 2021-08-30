using System.Drawing;

namespace Outlines
{
    public interface ISnapshotService
    {
        Snapshot TakeSnapshot(Rectangle bounds);
        Snapshot TakeSnapshot(ElementProperties elementProperties);
        void SaveSnapshot(Snapshot snapshot);
    }
}