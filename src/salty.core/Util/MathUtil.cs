using System;

namespace salty.core.Util
{
    public static class MathUtil
    {
        public static int RoundToNearest(this float input, float roundTo)
        {
            if (roundTo == 0)
                throw new DivideByZeroException();
            var rounded = input / roundTo;
            return (int) (Math.Round(rounded) * roundTo);
        }
    }
}