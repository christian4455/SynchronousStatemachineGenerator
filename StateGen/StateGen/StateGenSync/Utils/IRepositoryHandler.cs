using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public interface IRepositoryHandler
    {
        StateGenSync.Types.StateMachineData HandleRepository(EA.Repository activityDiagram);
    }
}
