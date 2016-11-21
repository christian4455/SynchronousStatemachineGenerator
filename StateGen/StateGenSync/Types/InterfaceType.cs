using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.StateGenSync.Types
{
    public class InterfaceType
    {
        private string m_InterfaceName;
        private List<Method> m_Methods = new List<Method>();

        public InterfaceType()
        {
            /* Intentionally left blank */
        }

        public void SetInterfaceName(string interfaceName)
        {
            m_InterfaceName = interfaceName;
        }

        public string GetInferfaceName()
        {
            return m_InterfaceName;
        }

        public void AddMethod(Method method)
        {
            m_Methods.Add(method);
        }

        public List<Method> GetMethods()
        {
            return m_Methods;
        }
    }
}
