
using System;
using System.Collections.Generic;

using StateGen.Utils.Logger;

namespace StateGen.PluginManager
{
    class PluginManager
    {
        private Dictionary<Types.PluginType.Enum, IPlugin> m_PluginCollection = new Dictionary<Types.PluginType.Enum, IPlugin>();

        public void RegisterPlugin(IPlugin plugin, Types.PluginType.Enum type)
        {
            Log.Info("type=" + type.ToString());
            m_PluginCollection.Add(type, plugin);
        }

        public bool ProcessRepository(EA.Repository repository, Types.PluginType.Enum type)
        {
            bool result = false;

            if (m_PluginCollection.ContainsKey(type))
            {
                m_PluginCollection[type].ProcessRepository(repository);
            }

            return result;
        }
    }
}
