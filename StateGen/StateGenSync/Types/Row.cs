using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.StateGenSync.Types
{
    public class Row
    {
        private Activity m_CurrentActivity;
        private string m_Event;
        private string m_Action;
        private Activity m_NextActivity;
        private string m_Guard;
        private Int32 m_ID;

        public Row(Activity currentActivity, string _event, string action, Activity nextActivity, string guard, Int32 id)
        {
            m_CurrentActivity = currentActivity;
            m_Event = _event;
            m_Action = action;
            m_NextActivity = nextActivity;
            m_Guard = guard;
            m_ID = id;
        }

        public Activity GetCurrentActivity()
        {
            return m_CurrentActivity;
        }

        public string GetEvent()
        {
            return m_Event;
        }
        
        public string GetAction()
        {
            return m_Action;
        }

        public void SetAction(string action)
        {
            m_Action = action;
        }

        public string GetGuard()
        {
            return m_Guard;
        }

        public Int32 GetID()
        {
            return m_ID;
        }

        public Activity GetNextActivity()
        {
            return m_NextActivity;
        }

        public void SetNextActivity(Activity nextActivity)
        {
            m_NextActivity = nextActivity;
        }
    }
}
