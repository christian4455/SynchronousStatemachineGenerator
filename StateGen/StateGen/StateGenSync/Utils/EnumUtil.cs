using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateGen.StateGenSync.Utils
{
    class EnumUtil
    {
        private EnumUtil()
        { }

        public static T ParseEnum<T>(string value, T defaultValue) where T : struct
        {
            try
            {
                T enumValue;
                if (!Enum.TryParse(value, true, out enumValue))
                {
                    return defaultValue;
                }

                return enumValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
