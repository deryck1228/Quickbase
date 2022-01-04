namespace Quickbase
{
    internal class QbGetRecordsBody
    {
        public string from { get; set; }
        //TODO Add in validation that allows only for 'a' and no other elements, or all elements must be int
        public List<object> select { get; set; } = new List<object>();
        public string where { get; set; }  
        public List<SortDefinitions> sortBy { get; set; }
        public List<GroupDefinitions> groupBy { get; set; }
        public Options options { get; set; }

    }
}
