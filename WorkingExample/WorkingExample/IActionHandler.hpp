#ifndef IACTIONHANDLER_HPP
#define IACTIONHANDLER_HPP

class IActionHandler
{

public:

IActionHandler() {}
virtual ~IActionHandler() {}

virtual void Activity2() = 0;

virtual void Activity3() = 0;

virtual void ActivityFinal() = 0;

virtual void Activity1() = 0;

virtual void FsmError() = 0;

private:
    /// @brief Automatically generated forbidden copy-constructor
    IActionHandler(const IActionHandler&)
    {
    }

    /// @brief Automatically generated forbidden assignment operator
    IActionHandler& operator =(const IActionHandler&)
    {
        return *this;
    }
};

#endif // IACTIONHANDLER_HPP

