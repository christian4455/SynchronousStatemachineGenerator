using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.StateGenSync.Types
{
    public class Method
    {
        private string m_FunctionName;
        private string m_ReturnType;

        public Method(string functionName, string returnType)
        {
            m_FunctionName = functionName;
            m_ReturnType = returnType;
        }

        public string GetFunctionName()
        {
            return m_FunctionName;
        }

        public string GetReturnType()
        {
            return m_ReturnType;
        }
    }
}
