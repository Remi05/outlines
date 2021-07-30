namespace Outlines
{
    public interface ISnapshotService
    {
        Snapshot TakeSnapshot(ElementProperties elementProperties);
        void SaveSnapshot(Snapshot snapshot);
    }
}