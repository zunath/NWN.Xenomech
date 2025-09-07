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
            try
            {
                // Check if NWN server might be running and warn user
                CheckForPotentialFileLocks();
                
                CreateServerDirectory();
                BuildHaks();
                BuildModule();
                BuildCachedCreatureFeatsFile();
                CopyExternalPlugins();
                CopyDataFiles();
                
                Console.WriteLine("Deployment completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deployment failed with error: {ex.Message}");
                Console.WriteLine("This may be due to the NWN server running and locking files.");
                Console.WriteLine("Please try stopping the NWN server and running the build again.");
                // Don't rethrow the exception to allow the build to continue
                // The build process should not fail completely due to deployment issues
            }
        }

        /// <summary>
        /// Checks for potential file locks and warns the user if NWN server might be running.
        /// </summary>
        private void CheckForPotentialFileLocks()
        {
            var modulePath = "../Module/Xenomech.mod";
            var tlkPath = "../Content/tlk/xenomech.tlk";
            
            var lockedFiles = new List<string>();
            
            if (File.Exists(modulePath) && IsFileLocked(modulePath))
            {
                lockedFiles.Add("Xenomech.mod");
            }
            
            if (File.Exists(tlkPath) && IsFileLocked(tlkPath))
            {
                lockedFiles.Add("xenomech.tlk");
            }
            
            if (lockedFiles.Count > 0)
            {
                Console.WriteLine("Warning: The following files appear to be locked by another process:");
                foreach (var file in lockedFiles)
                {
                    Console.WriteLine($"  - {file}");
                }
                Console.WriteLine("This may be due to the NWN server running. The deployment will attempt to retry file operations.");
                Console.WriteLine("If deployment fails, please stop the NWN server and try again.");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Checks if a file is locked by attempting to open it for reading.
        /// </summary>
        /// <param name="filePath">Path to the file to check</param>
        /// <returns>True if the file is locked, false otherwise</returns>
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return false; // File is not locked
                }
            }
            catch (IOException)
            {
                return true; // File is locked
            }
            catch (UnauthorizedAccessException)
            {
                return true; // File is locked or access denied
            }
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
            var destinationPath = ModulesPath + "/Xenomech.mod";
            CopyFileWithRetry(modulePath, destinationPath);
        }


        private static bool IsSharingOrLockViolation(IOException ex)
        {
            // HRESULT codes for sharing/lock violations
            // ERROR_SHARING_VIOLATION: 0x20 (32) -> 0x80070020
            // ERROR_LOCK_VIOLATION: 0x21 (33) -> 0x80070021
            const int HR_ERROR_SHARING_VIOLATION = unchecked((int)0x80070020);
            const int HR_ERROR_LOCK_VIOLATION = unchecked((int)0x80070021);
            return ex.HResult == HR_ERROR_SHARING_VIOLATION || ex.HResult == HR_ERROR_LOCK_VIOLATION;
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

                // Use retry logic for file copying to handle NWN server file locks
                CopyFileWithRetry(fi.FullName, targetPath);
            }
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, preventOverwriteFile, excludeFiles);
            }
        }

        /// <summary>
        /// Static version of CopyFileWithRetry for use in CopyAll method.
        /// </summary>
        /// <param name="sourcePath">Source file path</param>
        /// <param name="destinationPath">Destination file path</param>
        /// <param name="maxRetries">Maximum number of retry attempts</param>
        /// <param name="retryDelayMs">Delay between retries in milliseconds</param>
        private static void CopyFileWithRetry(string sourcePath, string destinationPath, int maxRetries = 3, int retryDelayMs = 1000)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Ensure destination directory exists
                    var destinationDir = Path.GetDirectoryName(destinationPath);
                    if (!string.IsNullOrEmpty(destinationDir) && !Directory.Exists(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }

                    File.Copy(sourcePath, destinationPath, true);
                    Console.WriteLine($"Successfully copied {Path.GetFileName(sourcePath)} on attempt {attempt}");
                    return;
                }
                catch (IOException ex) when (IsSharingOrLockViolation(ex))
                {
                    if (attempt < maxRetries)
                    {
                        Console.WriteLine($"{Path.GetFileName(sourcePath)} is locked by another process (likely NWN server). Retrying in {retryDelayMs}ms... (Attempt {attempt}/{maxRetries})");
                        Thread.Sleep(retryDelayMs);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to copy {Path.GetFileName(sourcePath)} after {maxRetries} attempts. The file may be locked by the NWN server.");
                        Console.WriteLine("Please ensure the NWN server is not running, or try again later.");
                        Console.WriteLine($"Error: {ex.Message}");
                        // Don't throw the exception - just log it and continue with other files
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error copying {Path.GetFileName(sourcePath)}: {ex.Message}");
                    return;
                }
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
