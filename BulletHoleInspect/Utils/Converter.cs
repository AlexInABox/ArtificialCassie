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
            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "./dependencies", TemporaryFilesFolder = "./dependencies/tmp" });

            // Define the path for the .ogg file, removing the extension from the original file
            string oggFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".ogg");

            if (!File.Exists(oggFilePath))  // Check if the .ogg file already exists
            {
                try
                {

                    await FFMpegArguments
                        .FromFileInput(filePath)  // Input file
                        .OutputToFile(oggFilePath, overwrite: true, options => options
                            .WithAudioCodec("libvorbis")
                            .WithAudioSamplingRate(48000)
                            .WithAudioFilters(filter => filter.Pan("mono", "c0 < 0.9 * c0 + 0.1 * c1")))
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
