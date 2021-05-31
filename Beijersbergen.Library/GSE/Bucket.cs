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
        public Bucket(int bucketNumber, int bucketIndex, DateTime date, string formulaCode, string formulaName, string orderCode, List<BucketBaseColor> baseColors)
        {
            BucketNumber = bucketNumber;
            BucketIndex = bucketIndex;
            Date = date;
            FormulaCode = formulaCode;
            FormulaName = formulaName;
            OrderCode = orderCode;
            BaseColors = baseColors;

            NumberOfBatchBaseColors = BaseColors.Count;
            TotalRequiredWeightKg = BaseColors.Sum(x => x.RequiredWeightKg);
            TotalDispensedWeightKg = BaseColors.Sum(x => x.DispensedWeightKg);
        }

        public int BucketNumber { get; set; } // TODO: Rename to Batch nummer?

        public int BucketIndex { get; set; }

        public DateTime Date { get; set; }

        public string FormulaCode { get; set; }

        public string FormulaName { get; set; }

        public string OrderCode { get; set; }

        public List<BucketBaseColor> BaseColors { get; set; } = new List<BucketBaseColor>();

        [Category("Kleuren")]
        [DisplayName("Aantal")]
        public int NumberOfBatchBaseColors { get; set; }

        public decimal TotalRequiredWeightKg { get; set; }

        public decimal TotalDispensedWeightKg { get; set; }
    }
}
