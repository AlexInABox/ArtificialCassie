namespace BulletHoleInspect
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events;
    using Events;

    public class BulletHoleInspect : Plugin<Config>
    {
        private static BulletHoleInspect Singleton;
        public static BulletHoleInspect Instance => Singleton;
        private CassieHandler cassieHandler;
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override string Author { get; } = "AlexInABox";

        public override void OnEnabled()
        {
            Singleton = this;
            Log.Info("BulletHoleInspect has been enabled!");
            cassieHandler = new CassieHandler();
            Exiled.Events.Handlers.Cassie.SendingCassieMessage += cassieHandler.OnSendingCassieMessage;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("BulletHoleInspect has been disabled!");

            Exiled.Events.Handlers.Cassie.SendingCassieMessage -= cassieHandler.OnSendingCassieMessage;
            base.OnDisabled();
        }
    }
}