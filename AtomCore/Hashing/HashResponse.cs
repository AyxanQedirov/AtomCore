using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.Hashing
{
    public class HashResponse
    {
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
    }
}
