namespace BulletHoleInspect.Utils
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Cassie;
    using AudioPlayer.API;

    internal sealed class ElevenlabsWrapper
    {
        public async void GenerateVoiceline(string text)
        {
            Log.Info(text);
        }
    }
}
