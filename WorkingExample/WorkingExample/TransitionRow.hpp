#ifndef TRANSITIONROW_HPP
#define TRANSITIONROW_HPP

#include "Activity.hpp"
#include "Events.hpp"
#include "FsmData.hpp"

class FsmData;
// Each row of the transition table
struct TransitionRow
{
Activity::Enum m_StartActivity;             // start activity
Events::Enum m_Event;                       // event identifier
// TODO check if we can use datapool parameter
void(*fn)(FsmData &);                       // function to be called for action
Activity::Enum m_NextActivity;              // next activity
bool(*conditionfn)(FsmData &);              // condition to be evaluated
};

#endif // TRANSITIONROW_HPP

