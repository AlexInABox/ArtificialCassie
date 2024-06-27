namespace io.alexinabox.bulletholeinspect
{
    using System.Collections.Generic;

    using Exiled.API.Enums;
    using Exiled.API.Features;

    /// <summary>
    /// A fake rank plugin
    /// </summary>
    public class BulletHoleInspect : Plugin<Config>
    {
        private static BulletHoleInspect Singleton;
        public static BulletHoleInspect Instance => Singleton;
        private PlayerHandler playerHandler;
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override string Author { get; } = "radston12";


        public override void OnEnabled()
        {
            Singleton = this;

            playerHandler = new PlayerHandler();
            Exiled.Events.Handlers.Player.Verified += playerHandler.OnVerified;

            Timing.CallDelayed(
                5f,
                () =>
                {
                    Extensions.BulletHoleInspectStorage.Create();
                    Extensions.BulletHoleInspectStorage.Reload();
                });

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= playerHandler.OnVerified;

            Extensions.BulletHoleInspectStorage.Storage.Clear();

            base.OnDisabled();
        }
    }
}