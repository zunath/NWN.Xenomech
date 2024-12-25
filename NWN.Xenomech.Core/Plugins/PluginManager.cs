using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.Extensions.Primitives;
using System.Runtime.Loader;

namespace NWN.Xenomech.Core.Plugins
{
    internal class PluginManager : IDisposable
    {
        private readonly List<AssemblyLoadContext> _pluginContexts = new();
        private readonly List<IPlugin> _loadedPlugins = new();
        private readonly List<IChangeToken> _changeTokens = new(); // List to hold change tokens
        private readonly List<PhysicalFileProvider> _fileProviders = new(); // List to hold PhysicalFileProvider instances

        private const string DotNetRoot = "/nwn/home/dotnet/";
        private const string PluginRoot = "/nwn/home/dotnet/plugins";
        private const string CoreLibrary = "NWN.Xenomech.Core.dll";

        public PluginManager()
        {
            // Set up the PhysicalFileProvider for each plugin folder (subdirectory)
            SetupPluginDirectoryWatchers();
        }

        // Set up PhysicalFileProvider for each plugin folder
        private void SetupPluginDirectoryWatchers()
        {
            var pluginDirectories = Directory.GetDirectories(PluginRoot);

            foreach (var pluginDirectory in pluginDirectories)
            {
                // Create a PhysicalFileProvider for each plugin folder
                var fileProvider = new PhysicalFileProvider(pluginDirectory)
                {
                    UsePollingFileWatcher = true, // Enable polling to handle file system monitoring
                    UseActivePolling = true
                };

                // Watch for changes in all files (DLLs) in the plugin folder
                var changeToken = fileProvider.Watch("*.dll");

                // Register the callback to be triggered on changes
                changeToken.RegisterChangeCallback(OnPluginChanged, pluginDirectory);

                _changeTokens.Add(changeToken); // Store the change token to manage them
                _fileProviders.Add(fileProvider); // Track the PhysicalFileProvider

                Console.WriteLine($"Watching plugin folder: {pluginDirectory}");
            }
        }

        // This is triggered when a plugin DLL is changed or created in any subdirectory
        private void OnPluginChanged(object state)
        {
            var pluginDirectory = state as string;
            if (pluginDirectory != null)
            {
                Console.WriteLine($"Plugin in directory {pluginDirectory} changed, reloading...");
                ReloadPlugins();

                // Re-register the watcher for further changes to continue monitoring the DLLs
                ReRegisterPluginWatcher(pluginDirectory);
            }
        }

        // Re-register the plugin watcher after a change to continue watching for further changes
        private void ReRegisterPluginWatcher(string pluginDirectory)
        {
            var fileProvider = new PhysicalFileProvider(pluginDirectory)
            {
                UsePollingFileWatcher = true, // Re-enable polling for further changes
                UseActivePolling = true
            };

            var changeToken = fileProvider.Watch("*.dll");
            changeToken.RegisterChangeCallback(OnPluginChanged, pluginDirectory);

            _fileProviders.Add(fileProvider); // Track the new PhysicalFileProvider
            Console.WriteLine($"Re-registered watch for plugin directory: {pluginDirectory}");
        }

        public void LoadPlugins()
        {
            // Check if NWN.Xenomech.Core.dll is already loaded in the default AppDomain
            if (!IsAssemblyLoaded("NWN.Xenomech.Core"))
            {
                var coreAssemblyPath = Path.Combine(DotNetRoot, CoreLibrary);
                if (File.Exists(coreAssemblyPath))
                {
                    // Load the core assembly into the default AppDomain
                    try
                    {
                        var coreAssembly = Assembly.LoadFrom(coreAssemblyPath); // Load from default context
                        Console.WriteLine($"Loaded {CoreLibrary} from {coreAssemblyPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to load {CoreLibrary}: {ex.Message}");
                        return; // Exit if core assembly can't be loaded
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {CoreLibrary} not found in DotNet root directory: {DotNetRoot}");
                    return;
                }
            }
            else
            {
                Console.WriteLine($"{CoreLibrary} is already loaded in the default context.");
            }

            // Load plugins from the specified plugin root directory
            foreach (var pluginDirectory in Directory.GetDirectories(PluginRoot))
            {
                foreach (var pluginPath in Directory.GetFiles(pluginDirectory, "*.dll"))
                {
                    if (pluginPath.Contains(CoreLibrary))
                        continue;

                    var context = new AssemblyLoadContext(pluginPath, isCollectible: true);

                    // Resolving dependencies for the plugin
                    context.Resolving += (context, name) =>
                    {
                        // Check if the assembly is already loaded in the default context
                        if (name.Name == "NWN.Xenomech.Core")
                        {
                            // Allow the plugin context to use the already-loaded assembly from the default context
                            return Assembly.Load(name);
                        }

                        // Try to resolve the assembly in the plugin root directory
                        var pluginAssemblyPath = Path.Combine(PluginRoot, name.Name + ".dll");

                        // If the assembly is found in the directory, load it
                        if (File.Exists(pluginAssemblyPath))
                        {
                            return context.LoadFromAssemblyPath(pluginAssemblyPath);
                        }

                        // Try to resolve the assembly in the individual plugin's directory
                        pluginAssemblyPath = Path.Combine(pluginDirectory, name.Name + ".dll");

                        if (File.Exists(pluginAssemblyPath))
                        {
                            return context.LoadFromAssemblyPath(pluginAssemblyPath);
                        }

                        // Resolving dependencies for plugin, if needed
                        return null;
                    };

                    var assembly = context.LoadFromAssemblyPath(pluginPath);
                    try
                    {
                        var pluginType = assembly
                            .GetTypes()
                            .FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t));

                        if (pluginType != null)
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                            plugin.OnLoad();
                            _loadedPlugins.Add(plugin);
                            _pluginContexts.Add(context);

                            Console.WriteLine($"Loaded plugin: {pluginPath}");
                        }
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        Console.WriteLine($"ReflectionTypeLoadException: {ex.Message}");
                        foreach (var loaderEx in ex.LoaderExceptions)
                        {
                            Console.WriteLine($"LoaderException: {loaderEx.Message}");
                        }
                    }
                }
            }
        }

        // Method to check if an assembly is already loaded
        private bool IsAssemblyLoaded(string assemblyName)
        {
            return
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Any(assembly => assembly.GetName().Name == assemblyName);
        }

        public void ReloadPlugins()
        {
            // Unload current plugins
            UnloadPlugins();

            // Trigger garbage collection to ensure the old context is unloaded
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Reload plugins
            LoadPlugins();
        }

        public void UnloadPlugins()
        {
            foreach (var plugin in _loadedPlugins)
            {
                plugin.OnUnload();
            }

            foreach (var context in _pluginContexts)
            {
                context.Unload();
            }

            _pluginContexts.Clear();
            _loadedPlugins.Clear();
        }

        // Clean up file watchers when done
        public void Dispose()
        {
            // Dispose of each PhysicalFileProvider to release the file watchers
            foreach (var fileProvider in _fileProviders)
            {
                fileProvider.Dispose(); // Dispose of the PhysicalFileProvider (which cleans up the file watchers)
            }
        }
    }
}
