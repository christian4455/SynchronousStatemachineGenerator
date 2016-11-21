using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    
    class BoostStateMachineBuilder
    {
        Product m_Product;
        StateMachineData m_Data;

        public BoostStateMachineBuilder(Product product, StateMachineData data)
        {
            m_Product = product;
            m_Data = data;
        }

        public void Init()
        {
            m_Product.Clear();
        }

        public Product CreateProduct()
        {
            m_Product.Append("ccc");
            return m_Product;
        }
    }
}
