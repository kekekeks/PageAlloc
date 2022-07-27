using System;

namespace PageAlloc
{
    /// <summary>
    /// An unmanaged memory region descriptor
    /// </summary>
    public struct PageAllocMemoryHandle
    {
        /// <summary>
        /// Memory address
        /// </summary>
        public IntPtr Address { get; }
        /// <summary>
        /// Memory size
        /// </summary>
        public IntPtr Size { get; }
        internal PageAllocMemoryHandle(IntPtr address, IntPtr size)
        {
            Address = address;
            Size = size;
        }
    }
}