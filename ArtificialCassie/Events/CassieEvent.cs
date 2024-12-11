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

            if (ArtificialCassie.Instance.Config.ReplaceEverything || !ev.isAllowed)
            {
                Log.Debug("Trying to replace announcement!");

                // Use the async Normalize method
                string normalizedWords = await NormalizeCassie.NormalizeAsync(ev.Words);
                Log.Debug($"Normalized Words: {normalizedWords}");

                // Generate the voiceline asynchronously
                Timing.RunCoroutine(ElevenlabsWrapper.GenerateVoiceline(normalizedWords));

                // Clear constantly for 2 seconds
                const int MAX_DELAY = 2000;
                int waited_for = 0;
                while (waited_for < MAX_DELAY)
                {
                    Cassie.Clear();
                    waited_for++;
                    await Task.Delay(1);
                }
            }
        }
    }
}
