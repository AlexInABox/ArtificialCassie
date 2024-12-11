namespace ArtificialCassie
{
    using System.ComponentModel;
    using System.IO;
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Interfaces;


    /// <inheritdoc cref="IConfig"/>
    public sealed class Config : IConfig
    {
        public Config()
        {
        }
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc />
        public bool Debug { get; set; }

        [Description("Generate an API Key at: https://elevenlabs.io/app/settings/api-keys")]
        public string elevenlabs_api_key { get; set; }
        public string voice_id { get; set; } = "tTZ0TVc9Q1bbWngiduLK";
        public string model_id { get; set; } = "eleven_turbo_v2_5";

        [Description("Should every C.A.S.S.I.E. voiceline be replaced, or only custom messages that you prepend with a '#'?")]
        public bool ReplaceEverything { get; set; } = false;

        [Description("I recommend to leave this on. This will reuse previously generated voicelines to save YOU money and tokens!")]
        public bool ReuseVoicelines { get; set; } = true;
    }
}