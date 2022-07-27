# Portable VirtualAlloc/mmap wrapper for .NET

Allocates in pages using OS syscall instead of going through the heap. That is usable for allocating large chunks of memory without fragmenting said heap.

API:

```cs

public static class PageAllocator
{
    public static PageAllocMemoryHandle? AllocateHandle(IntPtr minimumSize);
    public static void FreeHandle(PageAllocMemoryHandle handle);
}

public struct PageAllocMemoryHandle
{
    public IntPtr Address { get; }
    public IntPtr Size { get; }
}

// PageAlloc.Memory package to avoid System.Memory dependency in the main one
public static class PageMemoryAllocator
{
    public static IMemoryOwner<byte>? AllocateMemory(int minimumSize, bool truncateReportedSize = false)
}gi


```