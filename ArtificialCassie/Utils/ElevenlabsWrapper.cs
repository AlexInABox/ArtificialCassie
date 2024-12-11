namespace ArtificialCassie.Utils
{
    using System;
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;
    using UnityEngine.Networking;
    using Newtonsoft.Json;
    using Exiled.API.Features;
    using MEC;
    using System.Collections.Generic;

    internal sealed class ElevenlabsWrapper
    {
        public static IEnumerator<float> GenerateVoiceline(string text)
        {
            string savePath = "/home/container/.config/EXILED/Plugins/audio/ArtificialCassie/";
            string fileName = $"{ComputeMD5(text)}.wav";
            string fullFilePath = Path.Combine(savePath, fileName);
            Directory.CreateDirectory(savePath);

            if (!File.Exists(fullFilePath) || !ArtificialCassie.Instance.Config.ReuseVoicelines)  // Check if file already exists
            {
                var payload = new
                {
                    text = text,
                    model_id = ArtificialCassie.Instance.Config.model_id
                };

                UnityWebRequest request = UnityWebRequest.Put(
                    $"https://api.elevenlabs.io/v1/text-to-speech/{ArtificialCassie.Instance.Config.voice_id}",
                    JsonConvert.SerializeObject(payload)
                );
                request.method = "POST";
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("xi-api-key", ArtificialCassie.Instance.Config.elevenlabs_api_key);

                yield return Timing.WaitUntilDone(request.SendWebRequest());

                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        // Save the received data to the file
                        File.WriteAllBytes(fullFilePath, request.downloadHandler.data);
                        Log.Debug($"Voiceline saved to: {fullFilePath}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Failed to save voiceline: {ex.Message}");
                    }
                }
                else
                {
                    Log.Error($"Failed to generate voiceline. Error: {request.error}");
                }
            }
            // If file already exists, play it directly
            Log.Debug("Converting to .ogg");
            Converter.Convert(fullFilePath);

        }

        private static string ComputeMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
