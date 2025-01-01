using Anvil.API;

namespace XM.UI
{
    public interface IViewModel
    {
        internal void Bind(
            uint player, 
            int windowToken,
            uint tetherObject = OBJECT_INVALID);

        public void OnOpen();

        public void OnClose();
    }
}
