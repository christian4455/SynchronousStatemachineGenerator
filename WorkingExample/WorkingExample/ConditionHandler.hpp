#ifndef CONDITIONHANDLER_HPP
#define CONDITIONHANDLER_HPP

#include "IConditionHandler.hpp"

class ConditionHandler : public IConditionHandler
{

public:

ConditionHandler();
virtual ~ConditionHandler();

virtual bool IsTrue();

};

#endif // CONDITIONHANDLER_HPP

