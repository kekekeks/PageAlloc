using System;
using System.Runtime.InteropServices;

namespace PageAlloc
{
    class WindowsAllocator : IAllocator
    {
        [DllImport("kernel32")]
        private static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32")]
        private static extern int VirtualFree(IntPtr lpAddress, IntPtr dwSize, uint dwFreeType);
        
        [DllImport("kernel32.dll", SetLastError=true)]
        internal static extern void GetSystemInfo(out SYSTEM_INFO Info);

        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            internal ushort wProcessorArchitecture;
            internal ushort wReserved;
            internal uint dwPageSize;
            internal IntPtr lpMinimumApplicationAddress;
            internal IntPtr lpMaximumApplicationAddress;
            internal IntPtr dwActiveProcessorMask;
            internal uint dwNumberOfProcessors;
            internal uint dwProcessorType;
            internal uint dwAllocationGranularity;
            internal ushort wProcessorLevel;
            internal ushort wProcessorRevision;
        }

        public WindowsAllocator()
        {
            GetSystemInfo(out var info);
            PageSize = info.dwPageSize;
        }

        public IntPtr Alloc(IntPtr size) =>
            VirtualAlloc(IntPtr.Zero, (uint)size.ToInt64(), 0x3000, 0x04);

        public void Free(IntPtr address, IntPtr size)
        {
            VirtualFree(address, IntPtr.Zero, 0x8000);
        }

        public uint PageSize { get; }
    }
}