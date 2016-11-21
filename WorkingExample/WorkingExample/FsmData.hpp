#ifndef FSMDATA_HPP
#define FSMDATA_HPP

#include "Activity.hpp"
#include "Events.hpp"
#include "IActionHandler.hpp"
#include "IConditionHandler.hpp"
#include "TransitionRow.hpp"

class FsmData
{

public:
    FsmData(IActionHandler& actionHandler, IConditionHandler& conditionHandler) :
        m_IActionHandler(actionHandler),
        m_IConditionHandler(conditionHandler)
{
    /* Intentionally left blank */
}

virtual ~FsmData()
{
    /* Intentionally left blank */
}

static void Activity2(FsmData& fsmData)
{
}

static void Activity3(FsmData& fsmData)
{
}

static void ActivityFinal(FsmData& fsmData)
{
}

static void Activity1(FsmData& fsmData)
{
}

static void FsmError(FsmData& fsmData)
{
}

// pseudoactions
static void Question(FsmData& fsmData)
{
    /* Intentionally left blank */
}

static bool IsTrue(FsmData& fsmData)
{
    return fsmData.GetConditionHandler().IsTrue();
}

inline IActionHandler& GetActionHandler() { return m_IActionHandler; }
inline IConditionHandler& GetConditionHandler() { return m_IConditionHandler; }

private:

IActionHandler& m_IActionHandler;
IConditionHandler& m_IConditionHandler;
};

//******************************************************
// Transition table
//******************************************************
const TransitionRow trans[] = {
// CURRENT ACTIVITY            EVENT          ACTION                   NEXT ACTIVITY              GUARD
{ ::Activity::Question       , ::Events::Any, &FsmData::Activity2    , ::Activity::Activity2    , &FsmData::IsTrue },
{ ::Activity::Question       , ::Events::Any, &FsmData::Activity3    , ::Activity::Activity3    , NULL             },
{ ::Activity::Activity1      , ::Events::Any, &FsmData::Question     , ::Activity::Question     , NULL             },
{ ::Activity::Activity2      , ::Events::Any, &FsmData::ActivityFinal, ::Activity::ActivityFinal, NULL             },
{ ::Activity::Activity3      , ::Events::Any, &FsmData::Question     , ::Activity::Question     , NULL             },
{ ::Activity::ActivityInitial, ::Events::Any, &FsmData::Activity1    , ::Activity::Activity1    , NULL             },
{ ::Activity::Any            , ::Events::Any, &FsmData::FsmError     , ::Activity::ActivityFinal, NULL             }
};


#endif // FSMDATA_HPP

