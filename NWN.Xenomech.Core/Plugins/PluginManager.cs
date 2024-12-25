using System.Runtime.Loader;

namespace NWN.Xenomech.Core.Plugins
{
    internal class PluginManager
    {
        private readonly List<AssemblyLoadContext> _pluginContexts = new();
        private readonly List<IPlugin> _loadedPlugins = new();

        public void LoadPlugins(string pluginDirectory)
        {
            foreach (var pluginPath in Directory.GetFiles(pluginDirectory, "*.dll"))
            {
                var context = new AssemblyLoadContext(pluginPath, isCollectible: true);
                context.Resolving += (context, name) =>
                {
                    // Resolving dependencies for plugin, if needed
                    return null;
                };

                var assembly = context.LoadFromAssemblyPath(pluginPath);
                var pluginType = assembly.GetTypes().FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t));

                if (pluginType != null)
                {
                    var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                    plugin.OnLoad();
                    _loadedPlugins.Add(plugin);
                    _pluginContexts.Add(context);
                }
            }
        }

        public void ReloadPlugins(string pluginDirectory)
        {
            // Unload current plugins
            UnloadPlugins();

            // Reload plugins
            LoadPlugins(pluginDirectory);
        }

        private void UnloadPlugins()
        {
            foreach (var context in _pluginContexts)
            {
                context.Unload();
            }

            _pluginContexts.Clear();
            _loadedPlugins.Clear();
        }

    }
}