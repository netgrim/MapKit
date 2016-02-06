using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConnectivityLite
{
    [DebuggerDisplay("{PntA} - {PntB}")]
    class DevIntConn
    {
        public long RowId { get; set; }

        public long DefId { get; set; }

        public string PntIdA { get; set; }

        public string PntIdB { get; set; }
        
        public string SwitchId { get; set; }

        public virtual DevIntInfo PntA { get; set; }

        public virtual DevIntInfo PntB { get; set; }

        public virtual DevIntInfo Switch { get; set; }

        //[System.Data.Entity.Infrastructure.Annotations.i
        public DevIntInfo GetOther(DevIntInfo devIntInfo)
        {
            if (PntA == devIntInfo)
                return PntB;
            else if (PntB == devIntInfo)
                return PntA;
            else
                throw new ArgumentException();
        }

        public void Replace(DevIntInfo devIntInfo, DevIntInfo newValue)
        {
            if (PntA == devIntInfo)
                PntA = newValue;
            else if (PntB == devIntInfo)
                PntB = newValue;
            else
                throw new ArgumentException();
        }


    }
}
