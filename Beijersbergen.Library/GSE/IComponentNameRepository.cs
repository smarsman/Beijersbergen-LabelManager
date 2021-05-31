using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Library.GSE
{
    public interface IComponentNameRepository
    {
        string GetComponentName(string componentCode);
    }
}
