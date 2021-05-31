using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Library.GSE
{
    public class BucketBaseColor
    {
        public string LotCode { get; set; }

        public string ComponentCode { get; set; }

        public string ComponentName { get; set; }

        public decimal RequiredWeightKg { get; set; }

        public decimal DispensedWeightKg { get; set; }
    }
}
