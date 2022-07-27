using System;

namespace PageAlloc
{
    interface IAllocator
    {
        IntPtr Alloc(IntPtr size);
        void Free(IntPtr address, IntPtr size);
        uint PageSize { get; }
    }
}