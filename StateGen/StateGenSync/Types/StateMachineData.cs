using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Types
{
    public class StateMachineData
    {
        private TransitionTable m_TransitionTable = new TransitionTable();
        private string m_StatemachineName;
        private List<Method> m_Actions = new List<Method>();
        private List<Method> m_Guards = new List<Method>();
        private InterfaceType m_ConditionHandler = new InterfaceType();
        private InterfaceType m_ActionHandler = new InterfaceType();
        private List<Method> m_PseudoActions = new List<Method>();
        private List<string> m_EnumEvents = new List<string>();
        private List<string> m_EnumActivities = new List<string>();
        private InterfaceType m_Fsm = new InterfaceType();

        public StateMachineData()
        {
            m_EnumEvents.Add("Any");
            m_EnumActivities.Add("Any");

            Method start = new Method("Start", "void");
            m_Fsm.AddMethod(start);
            m_Fsm.SetInterfaceName("IFsmHandler");

            //Activity errorCurrentActivity = new Activity("Any", ElementType.Activity, -1);

            //string errorTransitionGuard = "";

            //Activity errorNextActivity = new Activity("ActivityFinal", ElementType.Activity, -1);

            //Row errorRow = new Row(errorCurrentActivity, "", "FsmError", errorNextActivity, errorTransitionGuard, -1);

            //GetTransitionTable().AddRow(errorRow);
        }

        public void AddRow(Row row)
        {
            m_TransitionTable.AddRow(row);
        }

        public bool Containes(Row row)
        {
            bool result = false;

            foreach (Row r in m_TransitionTable.GetRows())
            {
                if (r.GetID() == row.GetID())
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public TransitionTable GetTransitionTable()
        {
            return m_TransitionTable;
        }

        public void SetStatemachineName(string statemachineName)
        {
            m_StatemachineName = statemachineName;
        }

        public string GetStatemachineName()
        {
            return m_StatemachineName;
        }

        public void AddAction(Method action)
        {
            m_Actions.Add(action);
            m_ActionHandler.AddMethod(action);
        }

        public List<Method> GetActions()
        {
            return m_Actions;
        }

        public void AddGuard(Method guard)
        {
            m_Guards.Add(guard);
            m_ConditionHandler.AddMethod(guard);
        }

        public List<Method> GetGuards()
        {
            return m_Guards;
        }

        public InterfaceType GetActionHandler()
        {
            return m_ActionHandler;
        }

        public InterfaceType GetConditionHandler()
        {
            return m_ConditionHandler;
        }

        public InterfaceType GetFsm()
        {
            return m_Fsm;
        }

        public void AddPseudoAction(Method pseudoAction)
        {
            m_PseudoActions.Add(pseudoAction);
        }

        public List<Method> GetPseudoActions()
        {
            return m_PseudoActions;
        }

        public List<string> GetEnumEvents()
        {
            return m_EnumEvents;
        }

        public List<string> GetEnumActivities()
        {
            return m_EnumActivities;
        }
    }
}
