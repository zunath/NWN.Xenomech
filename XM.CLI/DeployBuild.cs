namespace XM.CLI
{
    internal class DeployBuild
    {
        private const string ServerPath = "../server/";
        private const string HakPath = ServerPath + "hak";
        private const string ModulesPath = ServerPath + "modules";
        private const string TlkPath = ServerPath + "tlk";
        private const string ExternalPluginsPath = "../ExternalPlugins";
        private const string AnvilPath = ServerPath + "anvil/";
        private const string AnvilPluginsPath = AnvilPath + "Plugins/";

        private readonly HakBuilder _hakBuilder = new();

        public void Process()
        {
            CreateServerDirectory();
            BuildHaks();
            BuildModule();
            CopyExternalPlugins();
        }

        private void CreateServerDirectory()
        {
            Directory.CreateDirectory(AnvilPath);
            Directory.CreateDirectory(ServerPath);
            Directory.CreateDirectory(HakPath);
            Directory.CreateDirectory(ModulesPath);
            Directory.CreateDirectory(TlkPath);

            var source = new DirectoryInfo("../XM.Runner/Docker");
            var target = new DirectoryInfo(ServerPath);

            CopyAll(source, target, "nwserver.env");

        }

        private void CopyExternalPlugins()
        {
            var source = new DirectoryInfo(ExternalPluginsPath);
            var target = new DirectoryInfo(AnvilPluginsPath);
            CopyAll(source, target, string.Empty);
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
