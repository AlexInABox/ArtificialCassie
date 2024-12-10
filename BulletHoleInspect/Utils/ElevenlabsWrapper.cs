namespace BulletHoleInspect.Utils
{
    using System;
    using System.IO;
    using System.Text;
    using UnityEngine.Networking;
    using Newtonsoft.Json;
    using Exiled.API.Features;
    using MEC;

    internal sealed class ElevenlabsWrapper
    {
        public static IEnumerator<float> GenerateVoiceline(string text)
        {
            string savePath = "/home/container/.config/EXILED/Plugins/audio/AI-CASSIE/";
            string fileName = $"{Guid.NewGuid()}.wav";
            Directory.CreateDirectory(savePath);

            var payload = new
            {
                text = text,
                model_id = BulletHoleInspect.Instance.Config.model_id
            };

            UnityWebRequest request = UnityWebRequest.Put(
                $"https://api.elevenlabs.io/v1/text-to-speech/{BulletHoleInspect.Instance.Config.voice_id}",
                JsonConvert.SerializeObject(payload)
            );
            request.method = "POST";
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("xi-api-key", config.elevenlabs_api_key);

            yield return Timing.WaitUntilDone(request.SendWebRequest());

            if (request.result is UnityWebRequest.Result.Success)
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(savePath, fileName), request.downloadHandler.data);
                    Log.Info($"Voiceline saved to: {Path.Combine(savePath, fileName)}");
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
    }
}
