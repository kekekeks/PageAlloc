using System;

namespace PageAlloc
{
    abstract class UnixAllocator : IAllocator
    {
        protected abstract uint sysconf(int name);
        protected abstract IntPtr mmap(IntPtr addr, IntPtr length, int prot, int flags, int fd, IntPtr offset);
        protected abstract int munmap(IntPtr addr, IntPtr length);
        protected abstract int GetFlags();
        
        public UnixAllocator()
        {
            PageSize = sysconf(29);
        }
        
        public IntPtr Alloc(IntPtr size)
        {
            var res = mmap(IntPtr.Zero, size, 0x3, GetFlags(), -1, IntPtr.Zero);
            if(res == new IntPtr(-1))
                return IntPtr.Zero;
            return res;
        }

        public void Free(IntPtr address, IntPtr size) => munmap(address, size);

        public uint PageSize { get; }
    }
}