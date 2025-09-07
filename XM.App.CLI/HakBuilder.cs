using System.Diagnostics;
using XM.App.CLI.Model;
using XM.Shared.Core.Json;

namespace XM.App.CLI
{
    public class HakBuilder
    {
        private const string ConfigFilePath = "./hakbuilder.json";
        private HakBuilderConfig _config;
        private List<HakBuilderHakpak> _haksToProcess;
        private readonly Dictionary<string, string> _checksumDictionary = new();

        public void Process()
        {
            try
            {
                // Read the config file.
                _config = GetConfig();
                _haksToProcess = _config.HakList.ToList();
                
                // Debug: Show current directory and TLK path
                Console.WriteLine($"Current working directory: {Directory.GetCurrentDirectory()}");
                Console.WriteLine($"TLK path from config: {_config.TlkPath}");
                Console.WriteLine($"TLK path resolved: {Path.GetFullPath(_config.TlkPath)}");
                
                // Clean the output folder.
                CleanOutputFolder();

                // Check if TLK file is locked before attempting to copy
                CheckTlkFileLock();

                // Copy the TLK to the output folder.
                Console.WriteLine($"Copying TLK: {_config.TlkPath}");

                if (File.Exists(_config.TlkPath))
                {
                    var destination = $"{_config.OutputPath}tlk/{Path.GetFileName(_config.TlkPath)}";
                    CopyFileWithRetry(_config.TlkPath, destination);
                }
                else
                {
                    Console.WriteLine("Error: TLK does not exist");
                }

                // Iterate over every configured hakpak folder and build the hak file.
                Parallel.ForEach(_haksToProcess, hak =>
                {
                    CompileHakpak(hak.Name, hak.Path);
                });

                Console.WriteLine("Hak building completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hak building failed with error: {ex.Message}");
                Console.WriteLine("This may be due to the NWN server running and locking files.");
                Console.WriteLine("Please try stopping the NWN server and running the build again.");
                // Don't rethrow the exception to allow the build to continue
                // The build process should not fail completely due to hak building issues
            }
        }

        /// <summary>
        /// Checks if an IOException is due to file sharing or lock violations.
        /// </summary>
        /// <param name="ex">The IOException to check</param>
        /// <returns>True if the exception is due to sharing/lock violations</returns>
        private static bool IsSharingOrLockViolation(IOException ex)
        {
            // HRESULT codes for sharing/lock violations
            // ERROR_SHARING_VIOLATION: 0x20 (32) -> 0x80070020
            // ERROR_LOCK_VIOLATION: 0x21 (33) -> 0x80070021
            const int HR_ERROR_SHARING_VIOLATION = unchecked((int)0x80070020);
            const int HR_ERROR_LOCK_VIOLATION = unchecked((int)0x80070021);
            return ex.HResult == HR_ERROR_SHARING_VIOLATION || ex.HResult == HR_ERROR_LOCK_VIOLATION;
        }

        /// <summary>
        /// Retrieves the configuration file for the hak builder.
        /// Throws an exception if the file is missing.
        /// </summary>
        /// <returns>The hak builder config settings.</returns>
        private HakBuilderConfig GetConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                throw new Exception($"Unable to locate config file. Ensure file '{ConfigFilePath}' exists in the same folder as this application.");
            }

            var json = File.ReadAllText(ConfigFilePath);
            return XMJsonUtility.Deserialize<HakBuilderConfig>(json);
        }

        /// <summary>
        /// Cleans the output folder.
        /// </summary>
        private void CleanOutputFolder()
        {
            {
                if (Directory.Exists(_config.OutputPath))
                {
                    // Delete .tlk with error handling for file locks
                    var tlkPath = $"{_config.OutputPath}tlk/{Path.GetFileName(_config.TlkPath)}";
                    if (File.Exists(tlkPath))
                    {
                        try
                        {
                            File.Delete(tlkPath);
                        }
                        catch (IOException ex) when (IsSharingOrLockViolation(ex))
                        {
                            Console.WriteLine($"Warning: Could not delete TLK file {Path.GetFileName(tlkPath)} - it may be locked by the NWN server.");
                            Console.WriteLine("The file will be overwritten during the copy operation.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Warning: Could not delete TLK file {Path.GetFileName(tlkPath)}: {ex.Message}");
                        }
                    }

                    Parallel.ForEach(_config.HakList, hak =>
                    {
                        // Check whether .hak file exists
                        if (!File.Exists(_config.OutputPath + "hak/" + hak.Name + ".hak"))
                        {
                            Console.WriteLine(hak.Name + " needs to be built");
                            return;
                        }

                        var checksumFolder = ChecksumUtil.ChecksumFolder(hak.Path);
                        _checksumDictionary.Add(hak.Name, checksumFolder);

                        // Check whether .sha checksum file exists
                        if (!File.Exists(_config.OutputPath + "hak/" + hak.Name + ".md5"))
                        {
                            Console.WriteLine(hak.Name + " needs to be built");
                            return;
                        }

                        // When checksums are equal or hak folder doesn't exist -> remove hak from the list
                        var checksumFile = ChecksumUtil.ReadChecksumFile(_config.OutputPath + "hak/" + hak.Name + ".md5");
                        if (checksumFolder == checksumFile)
                        {
                            _haksToProcess.Remove(hak);
                            Console.WriteLine(hak.Name + " is up to date");
                        }
                    });

                    // Delete outdated haks and checksums
                    Parallel.ForEach(_haksToProcess, hak =>
                    {
                        var filePath = _config.OutputPath + "hak/" + hak.Name;
                        if (File.Exists(filePath + ".hak"))
                        {
                            try
                            {
                                File.Delete(filePath + ".hak");
                            }
                            catch (IOException ex) when (IsSharingOrLockViolation(ex))
                            {
                                Console.WriteLine($"Warning: Could not delete HAK file {hak.Name}.hak - it may be locked by the NWN server.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Warning: Could not delete HAK file {hak.Name}.hak: {ex.Message}");
                            }
                        }

                        if (File.Exists(filePath + ".md5"))
                        {
                            try
                            {
                                File.Delete(filePath + ".md5");
                            }
                            catch (IOException ex) when (IsSharingOrLockViolation(ex))
                            {
                                Console.WriteLine($"Warning: Could not delete checksum file {hak.Name}.md5 - it may be locked by the NWN server.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Warning: Could not delete checksum file {hak.Name}.md5: {ex.Message}");
                            }
                        }
                    });
                }
                else
                {
                    Directory.CreateDirectory(_config.OutputPath);
                }
            }
        }

        /// <summary>
        /// Copies a file with retry logic to handle file locking issues.
        /// </summary>
        /// <param name="sourcePath">Source file path</param>
        /// <param name="destinationPath">Destination file path</param>
        /// <param name="maxRetries">Maximum number of retry attempts</param>
        /// <param name="retryDelayMs">Delay between retries in milliseconds</param>
        private void CopyFileWithRetry(string sourcePath, string destinationPath, int maxRetries = 3, int retryDelayMs = 1000)
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
                    Console.WriteLine($"Successfully copied TLK file on attempt {attempt}");
                    return;
                }
                catch (IOException ex) when (IsSharingOrLockViolation(ex))
                {
                    if (attempt < maxRetries)
                    {
                        Console.WriteLine($"TLK file is locked by another process (likely NWN server). Retrying in {retryDelayMs}ms... (Attempt {attempt}/{maxRetries})");
                        Thread.Sleep(retryDelayMs);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to copy TLK file after {maxRetries} attempts. The file may be locked by the NWN server.");
                        Console.WriteLine("Please ensure the NWN server is not running, or try again later.");
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error copying TLK file: {ex.Message}");
                    return;
                }
            }
        }

        /// <summary>
        /// Checks if the TLK file is locked and warns the user.
        /// </summary>
        private void CheckTlkFileLock()
        {
            if (File.Exists(_config.TlkPath) && IsFileLocked(_config.TlkPath))
            {
                Console.WriteLine("Warning: The TLK file appears to be locked by another process.");
                Console.WriteLine("This may be due to the NWN server running. The hak builder will attempt to retry the file operation.");
                Console.WriteLine("If the operation fails, please stop the NWN server and try again.");
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

        /// <summary>
        /// Creates a new background process used for running external programs.
        /// </summary>
        /// <param name="command">The command to pass into the cmd instance.</param>
        /// <returns>A new process</returns>
        private Process CreateProcess(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("cmd.exe", "/K " + command)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = false
                },
                EnableRaisingEvents = true
            };

            return process;
        }

        /// <summary>
        /// Compiles files contained in a folder into a hakpak.
        /// </summary>
        /// <param name="hakName">The name of the hak without the .hak extension</param>
        /// <param name="folderPath">The folder where the assets are.</param>
        private void CompileHakpak(string hakName, string folderPath)
        {
            var command = $"nwn_erf -f \"{_config.OutputPath}hak/{hakName}.hak\" -e HAK -c ./{folderPath}";
            Console.WriteLine($"Building hak: {hakName}.hak");

            using (var process = CreateProcess(command))
            {
                process.Start();

                process.StandardInput.Flush();
                process.StandardInput.Close();

                process.StandardOutput.ReadToEnd();

                process.WaitForExit();
            }

            if (!_checksumDictionary.TryGetValue(hakName, out var checksum))
            {
                checksum = ChecksumUtil.ChecksumFolder(folderPath);
            }

            ChecksumUtil.WriteChecksumFile(_config.OutputPath + "hak/" + hakName + ".md5", checksum);
        }
    }
}
