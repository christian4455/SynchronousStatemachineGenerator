using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public interface IFsmDataBuilder
    {
        Product CreateProduct(StateMachineData fsmData, string filename);
    }
}
