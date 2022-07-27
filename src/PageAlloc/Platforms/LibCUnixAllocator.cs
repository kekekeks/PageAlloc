using System;
using System.Runtime.InteropServices;

namespace PageAlloc
{
    class LibCUnixAllocator : UnixAllocator
    {
        private readonly int _flags;

        public LibCUnixAllocator(int flags)
        {
            _flags = flags;
        }
        
        [DllImport("c", EntryPoint = "sysconf")]
        static extern uint sysconf_c(int name);

        [DllImport("c", EntryPoint = "mmap")]
        static extern IntPtr mmap_c(IntPtr addr, IntPtr length, int prot, int flags, int fd, IntPtr offset);

        [DllImport("c", EntryPoint = "munmap")]
        static extern int munmap_c(IntPtr addr, IntPtr length);

        protected override uint sysconf(int name) => sysconf_c(name);

        protected override IntPtr mmap(IntPtr addr, IntPtr length, int prot, int flags, int fd, IntPtr offset)
            => mmap_c(addr, length, prot, flags, fd, offset);

        protected override int munmap(IntPtr addr, IntPtr length) => munmap_c(addr, length);
        protected override int GetFlags() => _flags;
    }
}