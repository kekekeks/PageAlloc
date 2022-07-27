using System;
using System.Runtime.InteropServices;

namespace PageAlloc
{
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

        public static PageAllocMemoryHandle? AllocateHandle(IntPtr minimumSize)
        {
            var size = Utils.RoundToPageSize(minimumSize, Allocator.PageSize);
            var mem = Allocator.Alloc(size);
            if (mem == IntPtr.Zero)
                return null;
            return new PageAllocMemoryHandle(mem, size);
        }

        public static void FreeHandle(PageAllocMemoryHandle handle)
            => Allocator.Free(handle.Address, handle.Size);
    }

}