using NWN.Xenomech.Core.Plugins;

namespace NWN.Xenomech.Plugin.Test
{
    public class Class1: IPlugin
    {
        public void OnLoad()
        {
            Console.WriteLine($"Loading plugin Test");
        }

        public void OnUnload()
        {
            Console.WriteLine($"Unloading plugin Test");
        }
    }
}
