using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickbase
{
    public class QbFieldMetadata
    {
        public string FieldId { get; set; }
        public string Name { get; set; }
        public IQbField FieldData { get; set; }
    }
}
