using Anvil.API;

namespace XM.UI
{
    public interface IViewModel
    {
        public NuiRect Geometry { get; protected set; }

        internal void Bind(
            uint player, 
            int windowToken,
            uint tetherObject = OBJECT_INVALID);

        public void OnOpen();

        public void OnClose();
    }
}
