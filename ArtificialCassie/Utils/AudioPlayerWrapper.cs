namespace ArtificialCassie.Utils
{
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using AudioPlayer.API;
    using System;
    using VoiceChat;

    public static class AudioPlayerWrapper
    {
        // Static Random instance for better performance and correct random number generation
        private static readonly Random random = new Random();

        public static async Task PlayAudioFromFile(string filePath)
        {
            int randomDummyId = random.Next(1, 100);  // Generate random number between 1 and 99

            try
            {
                // Spawn dummy audio player with the random ID
                AudioController.SpawnDummy(randomDummyId, "AudioPlayer BOT", "orange", "C.A.S.S.I.E");
            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to spawn dummy audio player: {ex.Message}");
            }

            // Play the audio from the provided file path
            AudioController.PlayAudioFromFile(filePath, false, 100, VoiceChatChannel.Proximity, false, false, true, randomDummyId);

            // Wait for 5 seconds before removing the dummy
            await Task.Delay(60000);  // 10000ms = 10 seconds

            try
            {
                // Remove the dummy after 5 seconds
                AudioController.DisconnectDummy(randomDummyId);
            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to remove dummy audio player: {ex.Message}");
            }
        }
    }
}
