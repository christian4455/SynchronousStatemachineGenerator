using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;
using StateGen.Utils.Logger;

namespace StateGen.StateGenSync.Types
{   
    public class TransitionTable
    {
        private List<Row> m_Rows = new List<Row>();

        public TransitionTable()
        {
            /* Intentionally left blank */
        }

        public void AddRow(Row row)
        {
            m_Rows.Add(row);
        }

        public List<Row> GetRows()
        {
            return m_Rows;
        }

        public void PrintTable()
        {
            foreach (Row r in m_Rows)
            {
                Log.Info("currentActivity=" + r.GetCurrentActivity().GetName() + " currentActivity type=" + r.GetCurrentActivity().GetElementType() + " event=" + r.GetEvent() + " action=" + r.GetAction() + " nextActivity=" + r.GetNextActivity().GetName() + " nextActivity type=" + r.GetNextActivity().GetElementType() + " guard=" + r.GetGuard() + " id=" + r.GetID());
            }
        }
    }
}
