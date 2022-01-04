using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickbase
{
    [Dbid("bqkhiqi9y")]
    public abstract class QbDataset<T> : IQbDataset<T> where T : IQbRecord
    {
        private List<Dictionary<string, object>> _data { get; set; } = new List<Dictionary<string, object>>();
        public List<QbRecord> QbRecords { get; } = new List<QbRecord>();

        public void AddRecord(QbRecord record)
        {
            var fieldData = record.GetFieldData();
            _data.Add(fieldData);
            QbRecords.Add(record);
        }

        public List<Dictionary<string, object>> GetRecordsAsDictionary()
        {
            return _data;
        }

        public List<QbRecord> GetQbRecords()
        {
            return QbRecords;
        }
    }
}
