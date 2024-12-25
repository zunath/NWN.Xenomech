namespace NWN.Xenomech.CLI
{
    internal class DeployBuild
    {
        private const string DebugServerPath = "../debugserver/";
        private const string DotnetPath = DebugServerPath + "dotnet";
        private const string HakPath = DebugServerPath + "hak";
        private const string ModulesPath = DebugServerPath + "modules";
        private const string TlkPath = DebugServerPath + "tlk";
        private const string PluginPath = DebugServerPath + DotnetPath + "/plugins/";

        private readonly HakBuilder _hakBuilder = new();

        public void Process()
        {
            CreateDebugServerDirectory();
            CopyCoreBinaries();
            CopyPluginBinaries();
            BuildHaks();
            BuildModule();
        }

        private void CreateDebugServerDirectory()
        {
            Directory.CreateDirectory(DebugServerPath);
            Directory.CreateDirectory(DotnetPath);
            Directory.CreateDirectory(HakPath);
            Directory.CreateDirectory(ModulesPath);
            Directory.CreateDirectory(TlkPath);
            Directory.CreateDirectory(PluginPath);

            var source = new DirectoryInfo("../NWN.Xenomech.Core/Docker");
            var target = new DirectoryInfo(DebugServerPath);

            CopyAll(source, target, "xenomech.env");
        }

        private void CopyCoreBinaries()
        {
            var binPath = "../NWN.Xenomech.Core/bin/Debug/net8.0/";

            var source = new DirectoryInfo(binPath);
            var target = new DirectoryInfo(DotnetPath);

            CopyAll(source, target, string.Empty);
        }

        private void CopyPluginBinaries()
        {
            var rootPath = "../";

            var pluginPaths = Directory.GetDirectories(rootPath, $"*.Plugin.*");
            foreach (var path in pluginPaths)
            {
                var splitName = path.Split(".");
                var folderName = splitName[^1];

                var binPath = new DirectoryInfo($"{path}/bin/Debug/net8.0/");
                var target = new DirectoryInfo(PluginPath + folderName);

                if (target.Exists)
                {
                    target.Delete(true);
                }
                target.Create();

                CopyAll(binPath, target, string.Empty, "NWN.Xenomech.Core.dll", "NWN.Xenomech.Core.pdb");
            }
        }

        private void BuildHaks()
        {
            _hakBuilder.Process();
        }

        private void BuildModule()
        {
            var modulePath = "../Module/Xenomech.mod";
            File.Copy(modulePath, ModulesPath + "/Xenomech.mod", true);
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target, string preventOverwriteFile, params string[] excludeFiles)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (var fi in source.GetFiles())
            {
                if (excludeFiles.Contains(fi.Name))
                    continue;

                var targetPath = Path.Combine(target.FullName, fi.Name);

                if (File.Exists(targetPath) && fi.Name == preventOverwriteFile)
                    continue;

                fi.CopyTo(targetPath, true);
            }
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, preventOverwriteFile, excludeFiles);
            }
        }
    }
}
