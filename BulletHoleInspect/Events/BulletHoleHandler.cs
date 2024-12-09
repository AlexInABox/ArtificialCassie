namespace BulletHoleInspect.Events
{
    using System;
    using Exiled.Events.EventArgs.Cassie;
    using Exiled.Events.Handlers;
    using Exiled.API.Features;

    internal sealed class CassieAnnouncementHandler
    {
        public CassieAnnouncementHandler()
        {
            Cassie.SendingCassieMessage += OnSendingCassieMessage;
        }

        private void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            Console.WriteLine("CASSIE Announcement:");
            Console.WriteLine($"Words: {ev.Words}");
            Console.WriteLine($"MakeHold: {ev.MakeHold}");
            Console.WriteLine($"MakeNoise: {ev.MakeNoise}");
            Console.WriteLine($"IsAllowed: {ev.IsAllowed}");
        }
    }
}