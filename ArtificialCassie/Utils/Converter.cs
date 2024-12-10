namespace ArtificialCassie.Utils
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using System.Diagnostics;

    internal sealed class Converter
    {
        public static async Task Convert(string filePath)
        {
            string ffmpegPath = "/home/container/.config/EXILED/Plugins/dependencies/ffmpeg/ffmpeg"; // Path to ffmpeg binary
            string tempFolderPath = "/home/container/.config/EXILED/Plugins/dependencies/tmp";

            // Define the path for the .ogg file, removing the extension from the original file
            string oggFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".ogg");

            if (!File.Exists(oggFilePath))  // Check if the .ogg file already exists
            {
                try
                {
                    // Build the ffmpeg command arguments
                    string arguments = $"-i \"{filePath}\" -c:a libvorbis -ar 48000 -filter_complex \"pan=mono|c0=0.9*c0+0.1*c1\" \"{oggFilePath}\"";

                    // Create the process to run the ffmpeg command
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = Path.GetDirectoryName(ffmpegPath) // Ensure valid working directory
                    };

                    using (Process process = Process.Start(startInfo))
                    {
                        if (process != null)
                        {
                            // Read the output and error streams asynchronously
                            string output = await process.StandardOutput.ReadToEndAsync();
                            string error = await process.StandardError.ReadToEndAsync();

                            process.WaitForExit();  // Wait for the conversion process to finish

                            if (process.ExitCode == 0)
                            {
                                Log.Info($"Successfully converted the file to: {oggFilePath}");
                            }
                            else
                            {
                                Log.Error($"FFmpeg conversion failed: {error}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the conversion
                    Log.Error($"An error occurred while converting the file: {ex.Message}");
                }
            }
            else
            {
                Log.Info($"The file {oggFilePath} already exists. Skipping conversion.");
            }

            // Play the converted audio file
            Log.Info("Playing file now...");
            AudioPlayerWrapper.PlayAudioFromFile(oggFilePath);
        }
    }
}
