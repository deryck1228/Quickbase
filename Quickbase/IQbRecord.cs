namespace Quickbase
{
    public interface IQbRecord
    {
        string RecordID { get; init; }

        void SetFieldData(Dictionary<string, object> data);
    }
}