using System;
using System.Runtime.InteropServices;

namespace PageAlloc
{
    /// <summary>
    /// Wraps VirtualAlloc/mmap with a fallback to Marshal.AllocHGlobal
    /// </summary>
    public static class PageAllocator
    {
        static IAllocator Allocator { get; }

        static PageAllocator()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Allocator = new WindowsAllocator();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                     || RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID")))
                Allocator = new LibCUnixAllocator(0x22);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                     || RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"))
                     || RuntimeInformation.IsOSPlatform(OSPlatform.Create("TVOS"))
                    )
                Allocator = new LibCUnixAllocator(0x1002);
            else
                Allocator = new ClrAllocator();
        }

        /// <summary>
        /// Allocates unmanaged memory
        /// </summary>
        /// <param name="minimumSize">The minimum required memory size</param>
        /// <returns>A memory handle</returns>
        public static PageAllocMemoryHandle? AllocateHandle(IntPtr minimumSize)
        {
            var size = Utils.RoundToPageSize(minimumSize, Allocator.PageSize);
            var mem = Allocator.Alloc(size);
            if (mem == IntPtr.Zero)
                return null;
            return new PageAllocMemoryHandle(mem, size);
        }

        /// <summary>
        /// Frees previously allocated memory
        /// </summary>
        public static void FreeHandle(PageAllocMemoryHandle handle)
            => Allocator.Free(handle.Address, handle.Size);
    }

}