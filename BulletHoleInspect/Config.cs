namespace BulletHoleInspect
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
    }
}