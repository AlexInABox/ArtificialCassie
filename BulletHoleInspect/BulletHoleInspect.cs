namespace BulletHoleInspect
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events;
    using Events;
    using Utils;
    using AudioPlayer.API;

    public class BulletHoleInspect : Plugin<Config>
    {
        private static BulletHoleInspect Singleton;
        public static BulletHoleInspect Instance => Singleton;
        private CassieHandler cassieHandler;
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override string Author { get; } = "AlexInABox";

        public override void OnEnabled()
        {
            if (Config.elevenlabs_api_key == '')
            {
                Log.Warn($"No Elevenlabs API key detected! Please generate one first at: https://elevenlabs.io/app/settings/api-keys");
                return;
            }

            Singleton = this;
            Log.Info("AI-CASSIE has been enabled!");
            cassieHandler = new CassieHandler();
            Exiled.Events.Handlers.Cassie.SendingCassieMessage += cassieHandler.OnSendingCassieMessage;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("AI-CASSIE has been disabled!");

            Exiled.Events.Handlers.Cassie.SendingCassieMessage -= cassieHandler.OnSendingCassieMessage;
            base.OnDisabled();
        }
    }
}