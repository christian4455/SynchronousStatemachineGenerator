#ifndef FSMHANDLER_HPP
#define FSMHANDLER_HPP

#include "Activity.hpp"
#include "FsmData.hpp"
#include "IFsmHandler.hpp"
#include "TransitionRow.hpp"

// FsmHandler data structure
class FsmHandler : public IFsmHandler
{

static const int TRANS_COUNT = sizeof(trans) / sizeof(*trans);

public:

explicit FsmHandler(FsmData& fsmData);
virtual ~FsmHandler();

virtual void Start();

private:

int GetNextEvent(FsmHandler* fsm);

void RunStateMachine(FsmHandler* fsm, const Activity::Enum initActivity);

void RunEvent(FsmHandler* fsm, int event);

FsmData& m_FsmData;                      // pointer to a structure carrying context
Activity::Enum m_CurrentActivity;        // Current activity
int m_NumActivity;                       // Number of entries in the table
const TransitionRow* m_pTransitionTable; // FSM table
};

#endif // FSMHANDLER_HPP

