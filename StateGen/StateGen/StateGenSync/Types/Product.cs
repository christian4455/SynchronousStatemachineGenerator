using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.StateGenSync.Types
{
    public class Product
    {
        StringBuilder m_Product = new StringBuilder();
        string m_Filename;

        public Product()
        {
            /* Intentionally left blank */
        }

        public StringBuilder Append(string part)
        {
            m_Product.Append(part);

            return m_Product;
        }

        public StringBuilder GetProduct()
        {
            return m_Product;
        }

        public string GetFilename()
        {
            return m_Filename;
        }

        public void SetFilename(string filename)
        {
            m_Filename = filename;
        }

        public void Clear()
        {
            m_Product.Clear();
            m_Filename = "";
        }
    }
}
