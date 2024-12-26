namespace NWN.Xenomech.Core.Plugins
{
    public interface IPlugin: IDisposable
    {
        string Name { get; }
        void Load();
        void Unload();
    }
}
