using System.Collections.Generic;
using System.Reflection;
using Anvil.API;

namespace XM.UI
{
    public interface IViewModel
    {
        internal void Bind(
            uint player, 
            int windowToken,
            uint tetherObject = OBJECT_INVALID);

        internal void OnCloseInternal();

        public void OnOpen();

        public void OnClose();

        //protected Dictionary<string, MethodInfo> GetBinds();
    }
}
