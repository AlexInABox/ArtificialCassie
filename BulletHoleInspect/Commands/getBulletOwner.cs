namespace BulletHoleInspect.Commands
{
    using System;
    using Exiled.API.Features;

    using CommandSystem;

    using UnityEngine;


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class toggleBulletHoleOwners : ParentCommand
    {
        public toggleBulletHoleOwners()
        {
            LoadGeneratedCommands();

            // Use this to load commands for the parent command
        }
        public override string Command { get; } = "toggleBulletHoleOwners";
        public override string Description { get; } = "Toggle the markers next to bullet holes to show the owner of the bullet hole.";
        public override string[] Aliases { get; } = new string[] { "tbo" };

        public override void LoadGeneratedCommands() // Put here your commands (the other commands dont need the [CommandHandler(typeof())]
        {
        }
        /// <inheritdoc/>
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // just enable bullet hole markers for now
            response = "Done!";
            return true;
        }
    }
}