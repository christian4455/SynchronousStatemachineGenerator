#ifndef ICONDITIONHANDLER_HPP
#define ICONDITIONHANDLER_HPP

class IConditionHandler
{

public:

IConditionHandler() {}
virtual ~IConditionHandler() {}

virtual bool IsTrue() = 0;

private:
    /// @brief Automatically generated forbidden copy-constructor
    IConditionHandler(const IConditionHandler&)
    {
    }

    /// @brief Automatically generated forbidden assignment operator
    IConditionHandler& operator =(const IConditionHandler&)
    {
        return *this;
    }
};

#endif // ICONDITIONHANDLER_HPP

