using AudioPlayer.API.Container;

namespace ArtificialCassie.Utils
{
    using System.Threading.Tasks;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using AudioPlayer.API;
    using AudioPlayer.Other;
    using PlayerRoles;
    using VoiceChat;
    using UnityEngine;

    public static class AudioPlayerWrapper
    {
        // Static Random instance for better performance and correct random number generation
        private static readonly System.Random random = new System.Random();

        public static async Task PlayAudioFromFile(string filePath, int audioDuration)
        {
            int randomDummyId = random.Next(200, 300);  // Generate random number between 200 and 300


            AudioPlayerBot bot = Extensions.SpawnDummy(name: "C.A.S.S.I.E.", id: randomDummyId);
            Player player = bot.Player;

            await Task.Delay(500);

            player.RoleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.Respawn);
            player.Teleport(new Vector3(-9999f, -9999f, -9999f));
            player.SyncEffect(new Effect(EffectType.Invisible, 2147483647));

            await Task.Delay(500);

            // Play the audio from the provided file path
            bot.PlayAudioFromFile(filePath, false, 75, VoiceChatChannel.Intercom, false, false, true);

            await Task.Delay(audioDuration + 500);

            try
            {
                // Remove the dummy after 5 seconds
                bot.Destroy();
            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to remove dummy audio player: {ex.Message}");
            }
        }
    }
}
