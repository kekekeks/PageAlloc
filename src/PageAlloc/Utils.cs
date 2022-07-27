using System;

namespace PageAlloc
{
    static class Utils
    {
        public static IntPtr RoundToPageSize(IntPtr size, uint pageSize)
        {
            var s = (ulong)size.ToInt64();
            var pages = s / pageSize;
            var rem = s % pageSize;
            if (rem == 0)
                return size;
            return new IntPtr((long)((pages + 1) * pageSize));


        }
    }
}