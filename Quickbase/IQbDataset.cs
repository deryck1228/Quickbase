
namespace Quickbase
{
    public interface IQbDataset<T> where T : IQbRecord
    {
        void AddRecord(QbRecord record);
        List<Dictionary<string, object>> GetRecordsAsDictionary();
    }
}