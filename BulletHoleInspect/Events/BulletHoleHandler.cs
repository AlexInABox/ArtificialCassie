namespace BulletHoleInspect.Events
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Map;
    using Exiled.Events.Patches.Events.Map;

    internal sealed class BulletHoleHandler
    {
        public void OnPlacingBulletHole(PlacingBulletHoleEventArgs ev)
        {
            Exiled.API.Features.Log.Info("Bullet hole placed at " + ev.Position);
        }

        public void OnSendingCassieMessage(Exiled.Events.EventArgs.Cassie.SendingCassieMessageEventArgs ev)
        {
            Exiled.API.Features.Log.Info("CASSIE Announcement:");
            Exiled.API.Features.Log.Info($"Words: {ev.Words}");
            Exiled.API.Features.Log.Info($"MakeHold: {ev.MakeHold}");
            Exiled.API.Features.Log.Info($"MakeNoise: {ev.MakeNoise}");
            Exiled.API.Features.Log.Info($"IsAllowed: {ev.IsAllowed}");
        }
    }
}