using Memo;
class Program
{
    static void Main()
    {
        var pID = Memory.GetProcessIDbyName("Terraria.exe");
        if (pID != -1)
        {
            var pHandle = Memory.OpenProcess(pID);
            if (pHandle > 0)
            {
                //0x408
                Memory Mem = new Memory(pHandle); // 初始化内存读写类
                //地址 0x363A96DC
                //偏移 - 
                //      人物id 0
                //      生命 0x408
                var life = Mem[0x363A96DC] //地址
                                    .Next(0, 0x408) //偏移
                                    .ReadInt32();//取值 int32 整数型

                var flag = Mem[0x363A96DC]
                                    .Next(0, 0x408)
                                    .WriteInt32(100);
                var life2 = Mem[0x363A96DC]
                                    .Next(0, 0x408)
                                    .ReadInt32();
                Console.WriteLine($"当前生命值:{life}");
                if (flag)
                {
                    Console.WriteLine("修改成功");
                }
                Console.WriteLine($"当前生命值:{life2}");
                // var speed = Mem[0x111]//地址
                //                 .ReadDouble();// Double 双浮数
            }

        }

    }
}