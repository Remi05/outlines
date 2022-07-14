using System.Drawing;
using Outlines.Core;

namespace Outlines.Inspection
{
    public interface ISnapshotService
    {
        Snapshot TakeSnapshot(Rectangle bounds);
        Snapshot TakeSnapshot(ElementProperties elementProperties);
        string SaveSnapshot(Snapshot snapshot);
        void ShowSnapshotNotitication(Snapshot snapshot, string snapshotFilePath);
    }
}