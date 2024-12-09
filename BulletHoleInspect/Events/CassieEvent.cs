namespace BulletHoleInspect.Events
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Cassie;

    internal sealed class CassieHandler
    {
        public async void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            Log.Info("CASSIE Announcement:");
            Log.Info($"Words: {ev.Words}");
            Log.Info($"MakeHold: {ev.MakeHold}");
            Log.Info($"MakeNoise: {ev.MakeNoise}");
            Log.Info($"IsAllowed: {ev.IsAllowed}");

            // Wait before clearing
            const int MAX_DELAY = 1000;
            int waited_for = 0;
            while (!Cassie.IsSpeaking && waited_for < MAX_DELAY)
            {
                await Task.Delay(1);
                waited_for++;
            }
            waited_for = 0;

            Cassie.Clear();
        }
    }
}
