using System.Collections.Generic;

namespace Beijersbergen.Library.Fluid
{
    public interface IBatchRepository
    {
        IEnumerable<Batch> FindByBatchNumber(string batchNumber);
        IEnumerable<Batch> SelectLatest(int numberOfBatches);
    }
}
