using Quickbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbaseConsoleTest
{
    [Dbid("asdix2a")]
    public class QbInvoice : QbRecord
    {
        [FieldId("3", "QbTextField")]
        public string RecordId { get; set; }

        [FieldId("6", "QbTextField") ]
        public string Name { get; set; }

        public string Title { get; set; } = "Mr.";
    }
}
