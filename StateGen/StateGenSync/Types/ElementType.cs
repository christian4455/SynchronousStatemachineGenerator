using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateGen.StateGenSync.Types
{
    public enum ElementType
    {
        Unknown      = 0,
        Activity     = 1,
        Class        = 2,
        Interface    = 3,
        Trigger      = 4,
        StateNode    = 5,
        State        = 6,
        StateMachine = 7,
        MergeNode    = 8,
        Decision     = 9,
        Max
    }
}
