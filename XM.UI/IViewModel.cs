using Anvil.API;

namespace XM.UI
{
    public interface IViewModel
    {
        public NuiRect Geometry { get; protected set; }

        internal void Bind(
            uint player, 
            int windowToken,
            NuiRect geometry,
            uint tetherObject = OBJECT_INVALID);

        internal void Unbind();

        public void OnOpen();

        public void OnClose();
    }
}
