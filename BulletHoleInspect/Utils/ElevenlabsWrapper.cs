namespace BulletHoleInspect.Utils
{
    using System.Threading.Tasks;
    using Exiled.API.Features;

    internal sealed class ElevenlabsWrapper
    {
        public static async void GenerateVoiceline(string text)
        {
            await Task.Delay(1000);
            Log.Info(text);
            // Add your implementation here
        }
    }
}