using System.Windows;

namespace Outlines
{
    public interface ISnapshotService
    {
        Snapshot TakeSnapshot(Rect bounds);
        Snapshot TakeSnapshot(ElementProperties elementProperties);
        void SaveSnapshot(Snapshot snapshot);
    }
}