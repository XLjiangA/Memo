namespace Memo;

enum GameOS
{
    X32,
    X64
}
class GlobalConfig
{
    private static GameOS _OS = GameOS.X32;
    public static bool Is32Bit => _OS == GameOS.X32;
    public static bool Is64Bit => _OS == GameOS.X64;
    public static void Enable32Bit() =>
        _OS = GameOS.X32;
    public static void Enable64Bit() =>
        _OS = GameOS.X64;
    public static Int32 mHandle;
}