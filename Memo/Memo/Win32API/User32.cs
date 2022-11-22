using System.Runtime.InteropServices;

namespace Win32API
{
    class User32
    {
        [DllImport("User32")]
        public extern static int GetSystemMetrics(int index);

        [DllImport("User32")]
        public extern static short GetAsyncKeyState(int vk_key);
        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyBoardInput
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Input
        {
            [FieldOffset(0)] public int type;
            [FieldOffset(4)] public MouseInput mouseInput;
            [FieldOffset(4)] public KeyBoardInput keyboardInput;
            [FieldOffset(4)] public HardwareInput hardwardInput;
        }
        [DllImport("User32")]
        public static extern uint SendInput(uint numberOfInputs,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Input[] input, int structSize);
    }
}