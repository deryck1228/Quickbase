using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickbase
{
    public class QbData : IQbData
    {
        public List<IQbField> Data { get; set; } = new List<IQbField>();
    }


    public class QbTextField : IQbField
    {
        IQbFieldValue IQbField.Value 
        {
            get { return Value; }
            set { Value = (QbTextValue)value; } 
        }
        public string FieldId { get; set; } = "";
        public QbTextValue? Value { get; set; } = new QbTextValue();
    }

    public class QbTextValue : IQbFieldValue
    {
        object IQbFieldValue.Value 
        {
            get { return Value; }
            set { Value = (string)value; }
        }
        public string Value { get; set; }

        public void SetFieldValue(object val)
        {
            Value = (string)val;
        }
    }


}
