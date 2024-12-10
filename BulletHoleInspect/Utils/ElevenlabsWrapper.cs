namespace BulletHoleInspect.Utils
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json; // Add this package to handle JSON serialization/deserialization
    using Exiled.API.Features;

    internal sealed class ElevenlabsWrapper
    {
        public static async void GenerateVoiceline(string text)
        {
            // Access the config from the singleton instance
            var config = BulletHoleInspect.Instance.Config;
            Log.Warn("Starting to do stuff!");
            if (string.IsNullOrWhiteSpace(config.elevenlabs_api_key))
            {
                Log.Warn("Elevenlabs API key is missing. Please configure it in your settings.");
                return;
            }

            if (string.IsNullOrWhiteSpace(config.voice_id) || string.IsNullOrWhiteSpace(config.model_id))
            {
                Log.Warn("Voice ID or Model ID is missing in the configuration.");
                return;
            }

            string url = $"https://api.elevenlabs.io/v1/text-to-speech/{config.voice_id}";
            var payload = new
            {
                text = text,
                model_id = config.model_id
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("xi-api-key", config.elevenlabs_api_key);
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                try
                {
                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Log.Info("Voiceline generated successfully.");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Log.Info($"Response: {responseContent}");
                    }
                    else
                    {
                        Log.Warn($"Failed to generate voiceline. Status Code: {response.StatusCode}");
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Log.Warn($"Error: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Exception while generating voiceline: {ex.Message}");
                }
            }
        }
    }
}