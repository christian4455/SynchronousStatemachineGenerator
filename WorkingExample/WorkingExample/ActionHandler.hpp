#ifndef ACTIONHANDLER_HPP
#define ACTIONHANDLER_HPP

#include "IActionHandler.hpp"

class ActionHandler : public IActionHandler
{

public:

ActionHandler();
virtual ~ActionHandler();

virtual void Activity2();

virtual void Activity3();

virtual void ActivityFinal();

virtual void Activity1();

virtual void FsmError();

};

#endif // ACTIONHANDLER_HPP

