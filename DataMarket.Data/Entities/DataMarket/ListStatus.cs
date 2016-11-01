using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public enum ListStatus
    {
        Queued = 2,
        Waiting = 3,
        Ready = 4,
        Failed = 8
    }
}
