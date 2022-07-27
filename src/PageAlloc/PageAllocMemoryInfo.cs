using System;

namespace PageAlloc
{
    public struct PageAllocMemoryHandle
    {
        public IntPtr Address { get; }
        public IntPtr Size { get; }
        internal PageAllocMemoryHandle(IntPtr address, IntPtr size)
        {
            Address = address;
            Size = size;
        }
    }
}