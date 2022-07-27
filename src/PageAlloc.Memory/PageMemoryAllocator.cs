using System;
using System.Buffers;

namespace PageAlloc
{
    public static class PageMemoryAllocator
    {
        public static IMemoryOwner<byte>? AllocateMemory(int minimumSize, bool truncateReportedSize = false)
        {
            var handle = PageAllocator.AllocateHandle(new IntPtr(minimumSize));
            if (handle == null)
                return null;
            
            return new PageAllocMemoryOwner(handle.Value,
                truncateReportedSize ? minimumSize : handle.Value.Size.ToInt32());
        }
    }
}