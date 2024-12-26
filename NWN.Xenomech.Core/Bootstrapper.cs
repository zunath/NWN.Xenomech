using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anvil.API;
using Anvil.Services;

namespace NWN.Xenomech.Core
{
    [ServiceBinding(typeof(Bootstrapper))]
    internal class Bootstrapper
    {
        public Bootstrapper()
        {
            
        }

        [ScriptHandler("mod_load")]
        public void OnModuleLoad()
        {
            DB.Instance.Load();
        }
    }
}
