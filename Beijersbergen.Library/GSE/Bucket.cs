using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Library.GSE
{
    public class Bucket
    {
        public int BucketNumber { get; set; } // TODO: Rename to Batch nummer?

        public int BucketIndex { get; set; }

        public DateTime Date { get; set; }

        public string FormulaCode { get; set; }

        public string FormulaName { get; set; }

        public string OrderCode { get; set; }

        public List<BucketBaseColor> BaseColors { get; set; } = new List<BucketBaseColor>();

        [Category("Kleuren")]
        [DisplayName("Aantal")]
        public int NumberOfBatchBaseColors => BaseColors.Count;

        public decimal TotalRequiredWeightKg
            => BaseColors.Sum(x => x.RequiredWeightKg);

        public decimal TotalDispensedWeightKg
            => BaseColors.Sum(x => x.DispensedWeightKg);
    }
}
