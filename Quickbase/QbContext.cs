using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickbase
{
    public class QbContext : IQbContext
    {
        public string Realm { get; init; }
        public string Usertoken { get; init; }

    }
}
