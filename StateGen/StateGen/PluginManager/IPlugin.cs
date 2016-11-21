using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.PluginManager
{
    public interface IPlugin
    {
        void ProcessRepository(EA.Repository repository);
    }
}
