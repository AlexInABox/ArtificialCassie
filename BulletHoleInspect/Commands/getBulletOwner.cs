namespace BulletHoleInspect
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    using UnityEngine;

    [CommandHandler(typeof(ClientCommandHandler))]
    public class getBulletOwner : ICommand
    {
        public string Command { get; } = "getbulletowner";
        public string Description { get; } = "Get the owner of the bullet hole you are looking at.";
        public string[] Aliases { get; } = new string[] { "gbo" };

        public bool SanitizeResponse => false;
        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            Ray ray = new(player.Transform.position, player.Transform.forward);
            RaycastHit raycastHit = new();

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.collider.TryGetComponent(out BulletHole bulletHole))
                {
                    response = $"Owner: {bulletHole.Owner}";
                    return true;
                }
                else
                {
                    response = "You are not looking at a bullet hole.";
                    return false;
                }
            }
            else
            {
                response = "You are not looking at a bullet hole.";
                return false;
            }
        }
    }
}