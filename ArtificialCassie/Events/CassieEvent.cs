namespace ArtificialCassie.Events
{
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Cassie;
    using AudioPlayer.API;
    using MEC;
    using Utils;

    internal sealed class CassieHandler
    {
        public async void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            Log.Info("Intercepted a C.A.S.S.I.E announcement");
            Log.Debug($"Words: {ev.Words}");
            Log.Debug($"IsAllowed: {ev.IsAllowed}");
            Log.Debug($"MakeHold: {ev.MakeHold}");
            Log.Debug($"MakeNoise: {ev.MakeNoise}");
            Log.Debug($"ArtificialCassie.Instance.Config.ReplaceEverything: {ArtificialCassie.Instance.Config.ReplaceEverything}");

            if (ArtificialCassie.Instance.Config.ReplaceEverything || ev.Words.StartsWith("#"))
            {
                ev.IsAllowed = false;
                Log.Debug("Trying to replace announcement!");

                // Use the async Normalize method
                string normalizedWords = await NormalizeCassie.NormalizeAsync(ev.Words);
                Log.Debug($"Normalized Words: {normalizedWords}");

                // Generate the voiceline asynchronously
                Timing.RunCoroutine(ElevenlabsWrapper.GenerateVoiceline(normalizedWords));
            }
        }
    }
}
