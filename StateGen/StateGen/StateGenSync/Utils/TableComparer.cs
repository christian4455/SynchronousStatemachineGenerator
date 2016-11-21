using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    class TableComparer : IComparer<Row>
    {
        private string ELSE_BRANCH = "else";

        public int Compare(Row x, Row y)
        {
            int result = 0;

            if (x.GetCurrentActivity() == null)
            {
                if (y.GetCurrentActivity() == null)
                {
                    // If x is null and y is null, they are equal.
                    result = 0;
                }
                else
                {
                    // If ix is null and y is not null, y is greater.
                    result = -1;
                }
            }
            else
            {
                // if x is not null..
                if (y.GetCurrentActivity() == null) // and y is null
                {
                    // x is greater
                    result = 1;
                }
                else
                {
                    // ...and y is not null, compare the lenghts of the two strings.
                    int retVal = x.GetCurrentActivity().GetName().Length.CompareTo(y.GetCurrentActivity().GetName().Length);

                    if (retVal != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater
                        result = retVal;
                    }
                    else
                    {
                        if (string.Equals(x.GetCurrentActivity().GetName(), y.GetCurrentActivity().GetName()))
                        {
                            if (x.GetCurrentActivity().GetName() == y.GetCurrentActivity().GetName())
                            {
                                result = 0;
                            }
                            else if(x.GetCurrentActivity().GetName() == ELSE_BRANCH)
                            {
                                result = -1;
                            }
                            else
                            {
                                result = 1;
                            }
                        }
                        else
                        {
                            // If the strings are of equal length,
                            // sort them with ordinary string comparison.
                            result = x.GetCurrentActivity().GetName().CompareTo(y.GetCurrentActivity().GetName());
                        }
                    }
                }
            }

            return result;
        }
    }
}
