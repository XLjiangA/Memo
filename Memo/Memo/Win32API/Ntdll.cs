using System.Runtime.InteropServices;

namespace Win32API;
class Ntdll
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CLIENT_ID
    {
        public int UniqueProcess;
        public int UniqueThread;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct THREAD_BASIC_INFORMATION
    {
        public int ExitStatus;
        public IntPtr TebBaseAdress;
        public CLIENT_ID ClientId;
        public IntPtr AffinityMask;
        public IntPtr Priority;
        public int BasePriority;
    }
    [DllImport("ntdll.dll")]
    public static extern UInt32 NtQueryInformationThread(
        int handle,
        uint infclass,
        ref THREAD_BASIC_INFORMATION info,
        uint length,
        UInt32 bytesread
    );

}