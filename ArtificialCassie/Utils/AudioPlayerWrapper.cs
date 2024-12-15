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
            int randomDummyId = random.Next(200, 300);  // Generate random number between 200 and 300


            FakeConnectionList fakeConnectionList = Extensions.SpawnDummy(name: "C.A.S.S.I.E.", id: randomDummyId);
            Player player = Extensions.GetAudioBot(fakeConnectionList.audioplayer);

            await Task.Delay(500);

            player.RoleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.Respawn);
            player.Teleport(new Vector3(-9999f, -9999f, -9999f));
            player.SyncEffect(new Effect(EffectType.Invisible, 2147483647));

            await Task.Delay(500);

            // Play the audio from the provided file path
            AudioController.PlayAudioFromFile(filePath, false, 75, VoiceChatChannel.Intercom, false, false, true, randomDummyId);

            await Task.Delay(500);

            while (fakeConnectionList.audioplayer.CurrentPlay != null)
            {
                await Task.Delay(500); //Wait for the track to stop
            }

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
