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
        private Int32 ADDITIONAL_COLONE_SIZE = 12;
        private Int32 ADDITIONAL_COLTWO_SIZE = 13;
        private Int32 ADDITIONAL_COLTHREE_SIZE = 10;
        private Int32 ADDITIONAL_COLFOUR_SIZE = 12;
        private Int32 ADDITIONAL_COLFIVE_SIZE = 10;

        private List<Row> m_Rows = new List<Row>();

        private Int32 m_ColOneLen = 0;
        private Int32 m_ColTwoLen = 0;
        private Int32 m_ColThreeLen = 0;
        private Int32 m_ColFourLen = 0;
        private Int32 m_ColFiveLen = 0;

        public Int32 GetColOneLen()
        {
            return m_ColOneLen;
        }

        public Int32 GetColTwoLen()
        {
            return m_ColTwoLen;
        }
        public Int32 GetColThreeLen()
        {
            return m_ColThreeLen;
        }
        public Int32 GetColFourLen()
        {
            return m_ColFourLen;
        }
        public Int32 GetColFiveLen()
        {
            return m_ColFiveLen;
        }

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

        public void CalculateColumnSizes()
        {
            bool GotRealGuard = false;

            m_Rows.ForEach(oneRow =>
            {
                m_ColOneLen = (oneRow.GetCurrentActivity().GetName().Length > m_ColOneLen) ? oneRow.GetCurrentActivity().GetName().Length : m_ColOneLen;
                m_ColTwoLen = (oneRow.GetEvent().Length > m_ColTwoLen) ? oneRow.GetEvent().Length : m_ColTwoLen;
                m_ColThreeLen = (oneRow.GetNextActivity().GetName().Length > m_ColThreeLen) ? oneRow.GetNextActivity().GetName().Length : m_ColThreeLen;
                m_ColFourLen = (oneRow.GetAction().Length > m_ColFourLen) ? oneRow.GetAction().Length : m_ColFourLen;
                m_ColFiveLen = (oneRow.GetGuard().Length > m_ColFiveLen) ? oneRow.GetGuard().Length : m_ColFiveLen;

                if (!GotRealGuard)
                {
                    GotRealGuard = oneRow.GetGuard().Contains("(") ? true : false;
                }
            });

            m_ColOneLen += ADDITIONAL_COLONE_SIZE;
            m_ColTwoLen += ADDITIONAL_COLTWO_SIZE;
            m_ColThreeLen += ADDITIONAL_COLTHREE_SIZE;
            m_ColFourLen += ADDITIONAL_COLFOUR_SIZE;
            m_ColFiveLen += ADDITIONAL_COLFIVE_SIZE;

            if (GotRealGuard)
            {
                m_ColFiveLen -= 2;
            }
        }
    }
}
