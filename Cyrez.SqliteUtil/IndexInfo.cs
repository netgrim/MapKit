using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyrez.SqliteUtil
{
    public class IndexInfo
    {
        public IndexInfo(string name, bool unique)
        {
            Name = name;
            Unique = unique;
        }

        public string Name { get; private set; }

        public bool Unique { get; private set; }
    }
}
