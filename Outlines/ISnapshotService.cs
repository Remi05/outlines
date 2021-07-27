namespace Outlines
{
    public interface ISnapshotService
    {
        Snapshot TakeSnapshot(ElementProperties elementProperties);
        Snapshot LoadSnapshot(string snapshotFilePath);
        void SaveSnapshot(Snapshot snapshot);
    }
}