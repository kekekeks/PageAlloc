using PageAlloc;
using Xunit;

namespace PageAllocTests;

public class Tests
{
    [Fact]
    public unsafe void AllocatedMemoryIsUsable()
    {
        var mem = PageAllocator.AllocateHandle(new IntPtr(123));
        Assert.NotNull(mem);
        Assert.True(mem.Value.Size.ToInt64() > 123);
        *(byte*)mem.Value.Address = 123;
        PageAllocator.FreeHandle(mem.Value);
    }
}