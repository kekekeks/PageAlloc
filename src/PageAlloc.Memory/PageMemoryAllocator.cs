using System;
using System.Buffers;

namespace PageAlloc
{
    /// <summary>
    /// Allocates IMemoryOwner&lt;byte&gt; using <see cref="PageAllocator"/>
    /// </summary>
    public static class PageMemoryAllocator
    {
        /// <summary>
        /// Allocates unmanaged memory
        /// </summary>
        /// <param name="minimumSize">The minimum size to allocate</param>
        /// <param name="truncateReportedSize">If set to true truncates reported Memory&lt;byte&gt;.Length to minimumSize</param>
        /// <returns>Allocated memory</returns>
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