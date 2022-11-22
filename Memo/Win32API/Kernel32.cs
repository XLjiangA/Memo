using System.Runtime.InteropServices;

namespace Win32API;
class Kernel32
{
    [DllImport("Kernel32")]
    public extern static bool CloseHandle(Int32 handle);

    [DllImport("kernel32")]
    public static extern Int32 OpenProcess(uint dwDesiredAccess, bool bInheritHandle, Int32 dwProcessId);

    [DllImport("kernel32")]
    public static extern bool ReadProcessMemory(Int32 hProcess, Int32 lpBaseAddress, byte[] buffer, Int32 size, Int32 lpNumberOfBytesRead);

    [DllImport("kernel32")]
    public static extern bool WriteProcessMemory(Int32 hProcess, Int32 lpBaseAddress, byte[] buffer, Int32 size,Int32 lpNumberOfBytesWritten);

    public const Int32 PROCESS_ALL_ACCESS = 0x1f0fff;

    [DllImport("kernel32")]
    public static extern Int32 OpenThread(Int32 dwDesiredAccess, bool bInheritHandle, Int32 dwThreadId);
}
