namespace BulletHoleInspect.Events
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Cassie;
    using AudioPlayer.API;
    using BulletHoleInspect.Utils;

    internal sealed class CassieHandler
    {
        public async void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            Log.Info("CASSIE Announcement:");
            Log.Info($"Words: {ev.Words}");
            Log.Info($"MakeHold: {ev.MakeHold}");
            Log.Info($"MakeNoise: {ev.MakeNoise}");
            Log.Info($"IsAllowed: {ev.IsAllowed}");

            ElevenlabsWrapper.GenerateVoiceline(ev.Words);

            try
            {
                AudioController.SpawnDummy(99, "AudioPlayer BOT", "orange", "C.A.S.S.I.E");
            }
            catch (System.Exception)
            {
                // Handle exception silently or leave empty
            }
            AudioController.PlayAudioFromFile("/home/container/.config/EXILED/Plugins/welcome.ogg");



            // Clear constantly for 2 seconds
            const int MAX_DELAY = 2000;
            int waited_for = 0;
            while (waited_for < MAX_DELAY)
            {
                Cassie.Clear();
                waited_for++;
                await Task.Delay(1);
            }
        }
    }
}
