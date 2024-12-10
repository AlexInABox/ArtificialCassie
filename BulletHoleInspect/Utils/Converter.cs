namespace BulletHoleInspect.Utils
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using FFMpegCore;
    using Exiled.API.Features;

    internal sealed class Converter
    {
        public static async Task Convert(string filePath)
        {
            // Define the path for the .ogg file, removing the extension from the original file
            string oggFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".ogg");

            if (!File.Exists(oggFilePath))  // Check if the .ogg file already exists
            {
                try
                {
                    // Perform the conversion with FFMpegCore
                    await FFMpegArguments
                        .FromFileInput(filePath)  // Input file
                        .OutputToFile(oggFilePath, true, options => options
                            .WithAudioSampleRate(48000)  // Set the sample rate to 48000 Hz
                            .WithAudioChannels(1)        // Set mono audio channel
                            .WithAudioCodec("libopus")   // Set the audio codec to Opus for .ogg
                            .WithFormat(AudioFormat.Ogg)) // Set the output format to .ogg
                        .ProcessAsynchronously();  // Perform the conversion asynchronously

                    Log.Info($"Successfully converted the file to: {oggFilePath}");
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
