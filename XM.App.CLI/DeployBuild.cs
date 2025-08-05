using System.Text.Json;
using XM.AI;
using XM.Shared.API.Constants;

namespace XM.App.CLI
{
    internal class DeployBuild
    {
        private const string ServerPath = "../server/";
        private const string ModulePath = "../Module/";
        private const string HakPath = ServerPath + "hak";
        private const string ModulesPath = ServerPath + "modules";
        private const string TlkPath = ServerPath + "tlk";
        private const string ExternalPluginsPath = "../ExternalPlugins";
        private const string AnvilPath = ServerPath + "anvil/";
        private const string AnvilPluginsPath = AnvilPath + "Plugins/";
        private const string ResourcesPath = AnvilPluginsPath + "resources/";
        private const string DataPath = "../Data/";

        private readonly HakBuilder _hakBuilder = new();

        public void Process()
        {
            CreateServerDirectory();
            BuildHaks();
            BuildModule();
            BuildCachedCreatureFeatsFile();
            CopyExternalPlugins();
            CopyDataFiles();
        }

        private void CreateServerDirectory()
        {
            Directory.CreateDirectory(AnvilPath);
            Directory.CreateDirectory(ServerPath);
            Directory.CreateDirectory(HakPath);
            Directory.CreateDirectory(ModulesPath);
            Directory.CreateDirectory(TlkPath);
            Directory.CreateDirectory(ResourcesPath);

            var source = new DirectoryInfo("../XM.App.Runner/Docker");
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

        private void BuildCachedCreatureFeatsFile()
        {
            const string CreaturePath = ModulePath + "utc";
            const string OutputPath = ResourcesPath + "CachedCreatures.json";
            var creatureFeatsList = new List<CreatureFeatsFile>();

            foreach (var filePath in Directory.GetFiles(CreaturePath, "*.json"))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(filePath));
                string jsonContent = File.ReadAllText(filePath);

                try
                {
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonContent);
                    var featList = new CreatureFeatsFile { Resref = fileNameWithoutExtension, Feats = new List<FeatType>() };

                    if (jsonData != null && jsonData.TryGetValue("FeatList", out object featListObj))
                    {
                        var featData = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(featListObj));
                        if (featData.TryGetValue("value", out object featValues))
                        {
                            var featEntries = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(JsonSerializer.Serialize(featValues));

                            foreach (var entry in featEntries)
                            {
                                if (entry.TryGetValue("Feat", out object featObj))
                                {
                                    var featInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(featObj));
                                    if (featInfo.TryGetValue("value", out object featValue) && int.TryParse(featValue.ToString(), out int featId))
                                    {
                                        featList.Feats.Add((FeatType)featId);
                                    }
                                }
                            }
                        }
                    }

                    creatureFeatsList.Add(featList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                }
            }

            // Write as a list instead of a dictionary
            File.WriteAllText(OutputPath, JsonSerializer.Serialize(creatureFeatsList, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine($"Processed {creatureFeatsList.Count} files and saved output to {OutputPath}");
        }

        private void CopyDataFiles()
        {
            var source = new DirectoryInfo(DataPath);
            var target = new DirectoryInfo(ResourcesPath);

            Console.WriteLine($"Copying Data files to: {target.FullName}");
            CopyAll(source, target, string.Empty);
            Console.WriteLine("Data files copied successfully.");
        }

    }
}
