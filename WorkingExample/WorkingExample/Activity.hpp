#ifndef ACTIVITY_HPP
#define ACTIVITY_HPP

#include <string>

class Activity
{

public:

    enum Enum
    {
        Any = 0,
        Question = 1,
        Activity2 = 2,
        Activity3 = 3,
        Activity1 = 4,
        ActivityFinal = 5,
        ActivityInitial = 6
    };

    static ::std::string ToString(const Enum e)
    {
        ::std::string ret;

        switch (e)
        {
        case Any : ret = "Any"; break;
        case Question : ret = "Question"; break;
        case Activity2 : ret = "Activity2"; break;
        case Activity3 : ret = "Activity3"; break;
        case Activity1 : ret = "Activity1"; break;
        case ActivityFinal : ret = "ActivityFinal"; break;
        case ActivityInitial : ret = "ActivityInitial"; break;
            // no default case since we want to get a compiler warning in case enum value is added
        }

        return ret;
    }
private:
    Activity();
    Activity(const Activity&);
    Activity & operator= (const Activity&);
};

#endif // ACTIVITY_HPP

