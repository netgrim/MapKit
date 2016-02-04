using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyrez.SqliteUtil
{
    public class ColumnInfo
    {
        public int Cid { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool NotNull { get; set; }
    }
}
