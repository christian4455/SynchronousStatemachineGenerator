#ifndef EVENTS_HPP
#define EVENTS_HPP

#include <string>

class Events
{

public:

    enum Enum
    {
        Any = 0
    };

    static ::std::string ToString(const Enum e)
    {
        ::std::string ret;

        switch (e)
        {
        case Any : ret = "Any"; break;
            // no default case since we want to get a compiler warning in case enum value is added
        }

        return ret;
    }
private:
    Events();
    Events(const Events&);
    Events & operator= (const Events&);
};

#endif // EVENTS_HPP

