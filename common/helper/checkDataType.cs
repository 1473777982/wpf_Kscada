using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.helper
{
    public class checkDataType
    {
        public static bool CheckDouble(string str)
        {
            try
            {
                double temp;
                temp = double.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckSingle(string str)
        {
            try
            {
                Single temp;
                temp = Single.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckInt32(string str)
        {
            try
            {
                Int32 temp;
                temp = Int32.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckInt16(string str)
        {
            try
            {
                Int16 temp;
                temp = Int16.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckUInt16(string str)
        {
            try
            {
                UInt16 temp;
                temp = UInt16.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckBoolean(string str)
        {
            try
            {
                if (str == "1" || str == "0")
                {
                    return true;
                }
                else
                {
                    Boolean temp;
                    temp = Boolean.Parse(str);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckByte(string str)
        {
            try
            {
                Byte temp;
                temp = Byte.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
