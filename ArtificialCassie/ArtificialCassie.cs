namespace ArtificialCassie
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events;
    using Events;
    using Utils;
    using AudioPlayer.API;

    public class ArtificialCassie : Plugin<Config>
    {
        public override string Prefix => "ArtificialCassie";
        public override string Name => "ArtificialCassie";
        public override string Author => "AlexInABox";
        public override Version Version => new Version(1, 0, 0);

        private static ArtificialCassie Singleton;
        public static ArtificialCassie Instance => Singleton;
        private CassieHandler cassieHandler;
        public override PluginPriority Priority { get; } = PluginPriority.Last;
        public override void OnEnabled()
        {
            if (string.IsNullOrWhiteSpace(Config.elevenlabs_api_key))
            {
                Log.Warn($"No Elevenlabs API key detected! Please generate one first at: https://elevenlabs.io/app/settings/api-keys");
                return;
            }

            Singleton = this;
            Log.Info("ArtificialCassie has been enabled!");
            cassieHandler = new CassieHandler();
            Exiled.Events.Handlers.Cassie.SendingCassieMessage += cassieHandler.OnSendingCassieMessage;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("ArtificialCassie has been disabled!");

            Exiled.Events.Handlers.Cassie.SendingCassieMessage -= cassieHandler.OnSendingCassieMessage;
            base.OnDisabled();
        }
    }
}