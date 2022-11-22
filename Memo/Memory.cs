using System.Diagnostics;
using Win32API;
namespace Memo;
public class MemoryBody
{
    /// <summary>
    /// 当前地址
    /// </summary>
    /// <value>Address</value>
    public Address mAddress { get; private set; }
    /// <summary>
    /// 进程句柄
    /// </summary>
    /// <value>Int32</value>
    public Int32 mHandle { get; }
    public MemoryBody(Address _address)
    {
        mAddress = _address;
        mHandle = GlobalConfig.mHandle;
    }
    /// <summary>
    /// 多重指针快速读写, 遇到无效地址停止
    /// </summary>
    /// <param name="offsets">偏移列表</param>
    /// <returns>内存读写类</returns>
    public MemoryBody Next(params int[] offsets)
    {
        var mb = new MemoryBody(mAddress);
        var len = offsets.Count();
        for (int i = 0; i < len; i++)
        {
            mb.mAddress += offsets[i];
            if (i == len - 1) break;
            var address = mb.ReadInt32();
            if (IsValid(address))
            {
                mb.mAddress = address;
            }
            else break;
        }
        return mb;
    }
    private bool IsValid(Address _address)
    {
        return ((_address >= 0x10000) && (_address < 0x7fffffff));
    }
    /// <summary>
    /// 读Int32值
    /// </summary>
    /// <param name="offset">偏移, 默认为0, 可空</param>
    /// <returns>Int32值</returns>
    public Int32 ReadInt32(Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return -1;
        var buffer = new byte[4];
        if (Kernel32.ReadProcessMemory(mHandle, mAddress + offset, buffer, 4, 0))
        {
            return BitConverter.ToInt32(buffer);
        }
        return -1;
    }
    /// <summary>
    /// 写Int32值
    /// </summary>
    /// <param name="value">写入的值</param>
    /// <param name="offset">偏移</param>
    /// <returns>返回是否成功</returns>
    public bool WriteInt32(Int32 value, Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return false;
        var buffer = BitConverter.GetBytes(value);
        if (Kernel32.WriteProcessMemory(mHandle, mAddress + offset, buffer, 4, 0))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 读Int64值
    /// </summary>
    /// <param name="offset">偏移, 默认为0, 可空</param>
    /// <returns>Int64值</returns>
    public Int64 ReadInt64(Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return -1;
        var buffer = new byte[8];
        if (Kernel32.ReadProcessMemory(mHandle, mAddress + offset, buffer, 8, 0))
        {
            return BitConverter.ToInt64(buffer);
        }
        return -1;
    }
    /// <summary>
    /// 写Int64值
    /// </summary>
    /// <param name="value">写入的值</param>
    /// <param name="offset">偏移</param>
    /// <returns>返回是否成功</returns>
    public bool WriteInt64(Int64 vlaue, Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return false;
        var buffer = BitConverter.GetBytes(vlaue);
        if (Kernel32.WriteProcessMemory(mHandle, mAddress + offset, buffer, 8, 0))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 读Float值
    /// </summary>
    /// <param name="offset">偏移, 默认为0, 可空</param>
    /// <returns>Float值</returns>
    public float ReadFloat(Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return -1;
        var buffer = new byte[4];
        if (Kernel32.ReadProcessMemory(mHandle, mAddress + offset, buffer, 4, 0))
        {
            return BitConverter.ToSingle(buffer);
        }
        return -1;
    }
    /// <summary>
    /// 写Float值
    /// </summary>
    /// <param name="value">写入的值</param>
    /// <param name="offset">偏移</param>
    /// <returns>返回是否成功</returns>
    public bool WriteFloat(float value, Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return false;
        var buffer = BitConverter.GetBytes(value);
        if (Kernel32.WriteProcessMemory(mHandle, mAddress + offset, buffer, 4, 0))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 读Double值
    /// </summary>
    /// <param name="offset">偏移, 默认为0, 可空</param>
    /// <returns>Double值</returns>
    public double ReadDouble(Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return -1;
        var buffer = new byte[8];
        if (Kernel32.ReadProcessMemory(mHandle, mAddress + offset, buffer, 8, 0))
        {
            return BitConverter.ToDouble(buffer);
        }
        return -1;
    }
    /// <summary>
    /// 写Double值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="offset"></param>
    /// <returns>返回是否成功</returns>
    public bool WriteDouble(double value, Int32 offset = 0)
    {
        if (!IsValid(mAddress)) return false;
        var buffer = BitConverter.GetBytes(value);
        if (Kernel32.WriteProcessMemory(mHandle, mAddress + offset, buffer, 8, 0))
        {
            return true;
        }
        return false;
    }

}
public class Memory
{
    /// <summary>
    /// 创建内存类
    /// </summary>
    /// <param name="mHandle">进程句柄</param>
    /// <param name="Is64Bit">是否为64位进程, 默认为32位, 可空</param>
    public Memory(Int32 mHandle, bool Is64Bit = false)
    {
        if (Is64Bit)
            GlobalConfig.Enable64Bit();
        else
            GlobalConfig.Enable32Bit();
        GlobalConfig.mHandle = mHandle;
    }
    public MemoryBody this[Address _address] => new MemoryBody(_address);
    /// <summary>
    /// 打开进程
    /// </summary>
    /// <param name="ProcessID">进程ID</param>
    /// <returns>进程句柄</returns>
    public static Int32 OpenProcess(Int32 processID) =>
         Kernel32.OpenProcess(Kernel32.PROCESS_ALL_ACCESS, false, processID);
    /// <summary>
    /// 根据游戏名称获取进程ID
    /// </summary>
    /// <param name="gameName">游戏名称</param>
    /// <returns>进程ID, 失败返回 -1</returns>
    public static Int32 GetProcessIDbyName(string gameName)
    {
        var processName = "";
        if (gameName.EndsWith(".exe"))
        {
            processName = gameName.Substring(0, gameName.IndexOf(".exe"));
        }
        else
        {
            processName = gameName;
        }
        Process[] ps = Process.GetProcesses();
        foreach (var p in ps)
        {
            if (p.ProcessName == processName)
                return p.Id;
        }
        return -1;
    }
}
