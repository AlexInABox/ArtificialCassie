namespace ArtificialCassie.Utils
{
    using System.Threading.Tasks;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using Exiled.Events.EventArgs;
    using Exiled.Events.EventArgs.Player;
    using AudioPlayer.API;
    using PlayerRoles;
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
            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to spawn dummy audio player: {ex.Message}");
            }

            // Get bot by userid (randomDummyId@audioplayer), and teleport them far away to use the intercom
            Player audioBot = Player.Get(FakeConnectionsIds.Values.FirstOrDefault(x => x.BotID == randomDummyId).hubPlayer);

            try
            {
                // Set the new role for the player
                audioBot.Role.Set(RoleTypeId.Tutorial);
            }
            catch (System.Exception ex)
            {
                Log.Error($"Couldnt set role: {ex.Message}");
            }

            audioBot.Position = new Vector3(-9999f, -9999f, -9999f);




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
