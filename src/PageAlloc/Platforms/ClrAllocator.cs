using System;
using System.Runtime.InteropServices;

namespace PageAlloc
{
    /// <summary>
    /// Fallback allocator in case we don't have a platform-specific one
    /// </summary>
    class ClrAllocator : IAllocator
    {
        public IntPtr Alloc(IntPtr size) => Marshal.AllocHGlobal(size);

        public void Free(IntPtr address, IntPtr size) => Marshal.FreeHGlobal(address);

        public uint PageSize => 4096;
    }
}