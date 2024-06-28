namespace BulletHoleInspect.Events
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Map;
    using Exiled.Events.Patches.Events.Map;
    /// <summary>
    /// Events bruh
    /// </summary>
    internal sealed class BulletHoleHandler
    {
        public void OnPlacingBulletHole(PlacingBulletHoleEventArgs ev)
        {
            Log.Info("Bullet hole placed at " + ev.Position);
        }
    }
}