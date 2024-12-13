namespace ArtificialCassie.Utils
{
    using System.Threading.Tasks;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using Exiled.Events.EventArgs;
    using Exiled.Events.EventArgs.Player;
    using AudioPlayer.API;
    using AudioPlayer.Other;
    using static AudioPlayer.Other.Extensions;
    using static AudioPlayer.Plugin;
    using PlayerRoles;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using VoiceChat;
    using UnityEngine;

    public static class AudioPlayerWrapper
    {
        // Static Random instance for better performance and correct random number generation
        private static readonly System.Random random = new System.Random();

        public static async Task PlayAudioFromFile(string filePath)
        {
            int randomDummyId = random.Next(1, 100);  // Generate random number between 1 and 99


            FakeConnectionList fakeConnectionList = SpawnDummy(name: "C.A.S.S.I.E.", id: randomDummyId);
            Player player = GetAudioBot(fakeConnectionList.audioplayer);
            if (player != null)
            {
                player.RoleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.Respawn);
                player.Teleport(new Vector3(-9999f, -9999f, -9999f));
                player.SyncEffect(new Effect(EffectType.Invisible, 2147483647));
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
