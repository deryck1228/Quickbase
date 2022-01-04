using Quickbase;

namespace QuickbaseConsoleTest
{
    [Dbid("bqkhiqi9y")]
    public class Contact : QbRecord
    {
        [FieldId("6", "QbTextField")]
        public string? FirstName { get; set; }
        [FieldId("7", "QbTextField")]
        public string? LastName { get; set; }
        public string? Title { get; set; }
    }
}
