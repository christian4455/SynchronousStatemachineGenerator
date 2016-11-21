// WorkingExample.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "ActionHandler.hpp"
#include "ConditionHandler.hpp"

#include "FsmData.hpp"
#include "FsmHandler.hpp"

int main()
{
    ActionHandler a;
    ConditionHandler c;
    FsmData f(a, c);
    FsmHandler h(f);

    h.Start();

    return 0;
}

