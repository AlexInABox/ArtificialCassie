namespace BulletHoleInspect.Events
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Cassie;


    internal sealed class CassieHandler
    {

        public void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            Log.Info("CASSIE Announcement:");
            Log.Info($"Words: {ev.Words}");
            Log.Info($"MakeHold: {ev.MakeHold}");
            Log.Info($"MakeNoise: {ev.MakeNoise}");
            Log.Info($"IsAllowed: {ev.IsAllowed}");

            Cassie.Clear();
        }
    }
}