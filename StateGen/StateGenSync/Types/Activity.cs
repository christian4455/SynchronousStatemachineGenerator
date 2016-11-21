using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.StateGenSync.Types
{
    public class Activity
    {
        private string m_Name;
        private ElementType m_ElementType;
        private Int32 m_ID;

        public Activity()
        {
            m_Name = "";
            m_ElementType = ElementType.Unknown;
            m_ID = -1;
        }

        public Activity(string name, ElementType type, Int32 id)
        {
            m_Name = name;
            m_ElementType = type;
            m_ID = id;
        }

        public string GetName()
        {
            return m_Name;
        }

        public ElementType GetElementType()
        {
            return m_ElementType;
        }

        public Int32 GetID()
        {
            return m_ID;
        }
    }
}
