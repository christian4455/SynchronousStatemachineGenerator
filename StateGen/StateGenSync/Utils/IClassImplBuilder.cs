using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public interface IClassImplBuilder
    {
        Product CreateProduct(List<Method> actions, string filename);
    }
}
