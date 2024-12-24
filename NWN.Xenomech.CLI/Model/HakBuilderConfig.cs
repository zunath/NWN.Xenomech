namespace NWN.Xenomech.CLI.Model
{
    public class HakBuilderConfig
    {
        public HakBuilderConfig()
        {
            HakList = new List<HakBuilderHakpak>();
        }

        public string TlkPath { get; set; }
        public string OutputPath { get; set; }
        public List<HakBuilderHakpak> HakList { get; set; }
    }
}
