using Anvil.API;

namespace XM.UI.Builder
{
    internal class NuiWindowBuildResult
    {
        public NuiWindow Window { get; }
        public NuiRect DefaultGeometry { get; }

        public NuiWindowBuildResult(NuiWindow window, NuiRect defaultGeometry)
        {
            Window = window;
            DefaultGeometry = defaultGeometry;
        }
    }
}
