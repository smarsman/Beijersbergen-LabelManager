using System.Collections.Generic;

namespace Beijersbergen.Library.GSE
{
    public interface IBucketRepository
    {
        IEnumerable<Bucket> FindByBucketNumber(int bucketNumber);

        IEnumerable<Bucket> SelectLatest(int numberOfBuckets);
    }
}
