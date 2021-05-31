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
        private readonly Bucket _bucket = new Bucket(
            bucketNumber: 1,
            bucketIndex: 1,
            date: DateTime.Now,
            formulaCode: "forumla code",
            formulaName: "formula name",
            orderCode: "OrderCode",
            baseColors: new List<BucketBaseColor>()
                {
                    new BucketBaseColor { ComponentCode = "123", ComponentName = "Zwart", DispensedWeightKg = 5, RequiredWeightKg = 5, LotCode = "efg" }
                }
            );

        public IEnumerable<Bucket> FindByBucketNumber(int bucketNumber) =>
            new List<Bucket>(1) { _bucket };

        public IEnumerable<Bucket> SelectLatest(int numberOfBuckets) =>
            new List<Bucket>(1) { _bucket };
    }
}
