namespace ArtificialCassie.Utils
{
    using System.Threading.Tasks;
    using Exiled.API.Enums;
    using Exiled.API.Features.Roles;
    using Exiled.Events.EventArgs;
    using AudioPlayer.API;
    using System;
    using VoiceChat;
    using UnityEngine;

    public static class AudioPlayerWrapper
    {
        // Static Random instance for better performance and correct random number generation
        private static readonly System.Random random = new System.Random();

        public static async Task PlayAudioFromFile(string filePath)
        {
            int randomDummyId = random.Next(1, 100);  // Generate random number between 1 and 99

            try
            {
                // Spawn dummy audio player with the random ID
                AudioController.SpawnDummy(randomDummyId, "AudioPlayer BOT", "orange", "C.A.S.S.I.E");

                // Get bot by userid (randomDummyId@audioplayer), and teleport them far away to use the intercom
                Player audioBot = Player.Get(randomDummyId + "@audioplayer");

                // Set the new role for the player
                audioBot.Role = RoleTypeId.Tutorial;

                // Teleport the player
                var teleportingEventArgs = new TeleportingEventArgs(audioBot, new Vector3(-9999f, -9999f, -9999f), true);
                Exiled.Events.Handlers.Scp106.OnTeleporting(teleportingEventArgs);

            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to spawn dummy audio player: {ex.Message}");
            }

            // Play the audio from the provided file path
            AudioController.PlayAudioFromFile(filePath, false, 75, VoiceChatChannel.Intercom, false, false, true, randomDummyId);

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
