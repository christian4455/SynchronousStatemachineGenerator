#include "FsmHandler.hpp"

#include <cstdio>

#include "Events.hpp"

FsmHandler::FsmHandler(FsmData & fsmData) :
    m_FsmData(fsmData),
    m_CurrentActivity(::Activity::ActivityInitial),
    m_NumActivity(TRANS_COUNT),
    m_pTransitionTable(trans)
{
    /* Intentionally left blank */
}

FsmHandler::~FsmHandler()
{
    /* Intentionally left blank */
}

//******************************************************
// This function starts the state machine. 
//******************************************************
void FsmHandler::Start()
{
    // Todo decleare init activity in a special member
    RunStateMachine(this, ::Activity::ActivityInitial);
}

//******************************************************
// This function determines what the next state is. 
//******************************************************
int FsmHandler::GetNextEvent(FsmHandler* fsm)
{
    // We have a synchronous state machine, so we fire some Event.
    return ::Events::Any;
}

//******************************************************
// Run the state machine while there is something to be read.
//******************************************************
void FsmHandler::RunStateMachine(FsmHandler* fsm, const Activity::Enum initActivity)
{
    FsmData & fsmData = fsm->m_FsmData;

    // set the init state
    fsm->m_CurrentActivity = initActivity;

    // cycle through the state machine
    // Todo declare final activity in a special member like start activity
    while (fsm->m_CurrentActivity != ::Activity::ActivityFinal)
    {
        int event = GetNextEvent(fsm);

        const Activity::Enum prevActivity = fsm->m_CurrentActivity;

        RunEvent(fsm, event);

        printf("transition %s -> %s \n", ::Activity::ToString(prevActivity).c_str(), ::Activity::ToString(fsm->m_CurrentActivity).c_str());
    }
}

//******************************************************
// Performs an event handling on the FSM.
// Make sure there is a wildcard activity at the end of
// your table, otherwise; the event will be ignored.
//******************************************************
void FsmHandler::RunEvent(FsmHandler* fsm, int event)
{
    int i;

    // walk over the transition table
    // to find a relevant entry for this activity and event
    //
    for (i = 0; i < fsm->m_NumActivity; i++)
    {
        // see if this is the Activity we are looking for
        if ((fsm->m_CurrentActivity == fsm->m_pTransitionTable[i].m_StartActivity) || (::Activity::Any == fsm->m_pTransitionTable[i].m_StartActivity))
        {
            // is this the event we are looking for
            if ((event == fsm->m_pTransitionTable[i].m_Event) || (::Events::Any == fsm->m_pTransitionTable[i].m_Event))
            {
                int iHasConditionSatisfied = (fsm->m_pTransitionTable[i].conditionfn && 
                fsm->m_pTransitionTable[i].conditionfn(fsm->m_FsmData));

                // See if there is a condition associated
                // or we are not looking for any condition
                //
                if (iHasConditionSatisfied || (fsm->m_pTransitionTable[i].conditionfn == NULL))
                {
                    // call the activity callback function
                    fsm->m_pTransitionTable[i].fn(fsm->m_FsmData);

                    // set the next activity
                    fsm->m_CurrentActivity = fsm->m_pTransitionTable[i].m_NextActivity;

                    break;
                }
            }
        }
    }
}

