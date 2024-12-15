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
            string ffprobePath = "/home/container/.config/EXILED/Plugins/dependencies/ffmpeg/ffprobe"; // Path to ffprobe binary
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
                                Log.Debug($"Successfully converted the file to: {oggFilePath}");
                            }
                            else
                            {
                                Log.Error($"FFmpeg conversion failed: {error}");
                                return; // Exit early if conversion failed
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the conversion
                    Log.Error($"An error occurred while converting the file: {ex.Message}");
                    return; // Exit early if an exception occurs
                }
            }
            else
            {
                Log.Debug($"The file {oggFilePath} already exists. Skipping conversion.");
            }

            // Extract audio duration using ffprobe
            double audioDuration = await GetAudioDuration(ffprobePath, oggFilePath);

            // Play the converted audio file
            Log.Debug($"Playing audioFile for: {audioDuration}");
            AudioPlayerWrapper.PlayAudioFromFile(oggFilePath, audioDuration);
        }

        private static async Task<double> GetAudioDuration(string ffprobePath, string filePath)
        {
            try
            {
                string arguments = $"-i \"{filePath}\" -show_entries format=duration -v quiet -of csv=\"p=0\"";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    if (process != null)
                    {
                        string output = await process.StandardOutput.ReadToEndAsync();
                        process.WaitForExit();

                        if (double.TryParse(output, out double duration))
                        {
                            Log.Debug($"Audio duration extracted: {duration} seconds");
                            return duration;
                        }
                        else
                        {
                            Log.Error("Failed to parse audio duration.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while extracting audio duration: {ex.Message}");
            }

            // Return 0 as a fallback
            return 0;
        }
    }
}
