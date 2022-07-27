using System;
using System.Buffers;

namespace PageAlloc
{
    unsafe class PageAllocMemoryOwner : MemoryManager<byte>
    {
        private PageAllocMemoryHandle? _handle;
        private readonly int _reportedSize;

        public PageAllocMemoryOwner(PageAllocMemoryHandle handle, int reportedSize)
        {
            _handle = handle;
            _reportedSize = reportedSize;
        }

        public override Span<byte> GetSpan()
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(PageAllocMemoryOwner));
            return new Span<byte>(_handle.Value.Address.ToPointer(), _reportedSize);
        }

        public override MemoryHandle Pin(int elementIndex = 0)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(PageAllocMemoryOwner));
            return new MemoryHandle(_handle.Value.Address.ToPointer());
        }

        public override void Unpin()
        {
            // No-op
        }
        
        protected override void Dispose(bool disposing)
        {
            if (_handle != null)
                PageAllocator.FreeHandle(_handle.Value);
            _handle = null;
        }
    }
}