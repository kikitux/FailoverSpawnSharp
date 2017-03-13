using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailoverSpawnSharp
{
    public class FailoverSpawnConnectionInfo
    {
        public string ConnectionDateTime { get; set; }
        public string DatabaseName { get; set; }
        public ConnectionState ConnectionState { get; set; }
    }
}
