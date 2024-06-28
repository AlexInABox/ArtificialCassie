namespace BulletHoleInspect
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events;

    public class BulletHoleInspect : Plugin<Config>
    {
        private static BulletHoleInspect Singleton;
        public static BulletHoleInspect Instance => Singleton;
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override string Author { get; } = "AlexInABox";

        public override void OnEnabled()
        {
            Singleton = this;
            Log.Info("BulletHoleInspect has been enabled!");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("BulletHoleInspect has been disabled!");

            base.OnDisabled();
        }
    }
}