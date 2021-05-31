using Beijersbergen.Library.GSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Gse
{
    public class StubBucketRepository : IBucketRepository
    {
        private readonly Bucket _bucket = new Bucket
        {
            FormulaCode = "ABCD",
            Date = DateTime.Now,
            BucketNumber = 1,
            OrderCode = "OrderCode",
            BaseColors = new List<BucketBaseColor>()
                {
                    new BucketBaseColor { ComponentCode = "123", ComponentName = "Zwart", DispensedWeightKg = 5, RequiredWeightKg = 5, LotCode = "efg" }
                }

        };
        public IEnumerable<Bucket> FindByBucketNumber(int bucketNumber) =>
            new List<Bucket>(1) { _bucket };

        public IEnumerable<Bucket> SelectLatest(int numberOfBuckets) =>
            new List<Bucket>(1) { _bucket };
    }
}
