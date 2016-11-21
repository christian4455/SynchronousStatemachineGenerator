#ifndef IFSMHANDLER_HPP
#define IFSMHANDLER_HPP

class IFsmHandler
{

public:

IFsmHandler() {}
virtual ~IFsmHandler() {}

virtual void Start() = 0;

private:
    /// @brief Automatically generated forbidden copy-constructor
    IFsmHandler(const IFsmHandler&)
    {
    }

    /// @brief Automatically generated forbidden assignment operator
    IFsmHandler& operator =(const IFsmHandler&)
    {
        return *this;
    }
};

#endif // IFSMHANDLER_HPP

