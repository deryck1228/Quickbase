using System.Text.Json.Serialization;

namespace Quickbase
{
    internal class QbPostRecordsBody
    {
        public string  to { get; set; }
        public object data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<int> fieldsToReturn { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int mergeFieldId { get; set; }
    }
}
