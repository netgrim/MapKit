using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConnectivityLite
{
    [DebuggerDisplay("{PntId}, {Code}")]
    class DevIntInfo
    {
        public DevIntInfo()
        {
            DevIntConnPntAs = new List<DevIntConn>();
            DevIntConnPntBs = new List<DevIntConn>();
            DevIntConnSwitches = new List<DevIntConn>();
        }

        public long RowId { get; set; }

        public long DefId { get; set; }
        
        public string PntId { get; set; }

        public DevIntInfoCode Code { get; set; }

        public virtual ICollection<DevIntConn> DevIntConnPntAs { get; set; }
        public virtual ICollection<DevIntConn> DevIntConnPntBs { get; set; }
        public virtual ICollection<DevIntConn> DevIntConnSwitches { get; set; }
    }

    enum DevIntInfoCode
    {
        ConnectionPoint = 1,
        Switch = 2,
        Intermediary = 9,
        VLN = 14
    }
}
