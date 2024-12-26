using System;
using NWN.Xenomech.Core.Plugins;

namespace NWN.Xenomech.Plugin.Test
{
    public class Class1: IPlugin
    {
        public string Name => "Test Plugin";

        public void Load()
        {
            Console.WriteLine($"does this change okokok");
        }

        ~Class1()
        {
            Console.WriteLine($"Finalizer running");
        }

        public void Unload()
        {
            Console.WriteLine($"Unloading plugin Test");
        }

        public void Dispose()
        {
            
        }
    }
}
