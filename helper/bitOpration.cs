using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2R.helper
{
    public class bitOpration
    {
        public static byte SetBit(byte b, int bitPosition, int value)
        {
            if (value != 0 && value != 1)
            {
                throw new ArgumentException("Value can only be 0 or 1");
            }

            if (bitPosition < 0 || bitPosition > 7)
            {
                throw new ArgumentException("Bit position must be between 0 and 7");
            }

            if (value == 1)
            {
                return (byte)(b | (1 << bitPosition));
            }
            else
            {
                return (byte)(b & ~(1 << bitPosition));
            }
        }
    }
}
